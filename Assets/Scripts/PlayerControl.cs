using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEditor;

public class PlayerControl : MonoBehaviour
{
    [Header ("Move and Jump")]
    [SerializeField] float moveSpeed;
    [SerializeField] float minJump, varJump;
    [SerializeField] int numberOfJumps = 1;
    [SerializeField] State state = State.Idling;
    CaptainInput captainInput;
    Vector2 moveInput;
    bool jumpButtonHold;
    float timeHoldJump = 0;
    float maxHoldTime = 0.5f;
    [SerializeField] int jumpTimes = 0;
    public static bool onAir;
    [HideInInspector]
    [SerializeField] Rigidbody2D playerRB;
    [HideInInspector]
    [SerializeField] BoxCollider2D playerFeet;
    [HideInInspector]
    [SerializeField] Animator playerAnim;

    private void Awake()
    {   
        captainInput = new CaptainInput();
        MovementInput();
        captainInput.Player.Jump.performed += Jump_performed;
        captainInput.Player.Jump.canceled += Jump_canceled;
        
    }

    private void Jump_canceled(InputAction.CallbackContext obj)
    {
        jumpButtonHold = false;
    }

    private void Jump_performed(InputAction.CallbackContext obj)
    {
        jumpTimes++;
        if (jumpTimes <= numberOfJumps)
        {
            jumpButtonHold = true;
            playerAnim.SetBool(State.Jumping.ToString(), true);
            timeHoldJump = 0f;
        }
    }

    
    #region Enable and Disable input
    private void OnEnable()
    {
        captainInput.Player.Enable();
    }
    private void OnDisable()
    {
        captainInput.Player.Disable();
    }
    #endregion

    void FixedUpdate()
    {
        Move();
        Jump();
        AnimationControl();
    }

    private void AnimationControl()
    {
        if (playerFeet.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            playerAnim.ResetTrigger(State.Falling.ToString());
            playerAnim.SetBool("onGround", true);
        }
        if (playerRB.velocity.y < 0)
        {
            playerAnim.SetTrigger(State.Falling.ToString());
            playerAnim.SetBool(State.Jumping.ToString(), false);
        }
    }

    private void Jump()
    {
        if (jumpButtonHold && timeHoldJump < maxHoldTime)
        {
            playerAnim.ResetTrigger(State.Falling.ToString());
            timeHoldJump = Mathf.Clamp(timeHoldJump, 0, maxHoldTime);
            timeHoldJump += Time.fixedDeltaTime;
            Vector2 jumper = new Vector2(playerRB.velocity.x, minJump + varJump * timeHoldJump);
            playerRB.velocity = jumper;
            playerAnim.SetBool("onGround", false);
        }
        if (playerFeet.IsTouchingLayers(LayerMask.GetMask("Ground")) && playerRB.velocity.y <= 0)
        {
            Debug.Log("on ground");
            jumpTimes = 0;
        }
    }
    #region Move
    private void MovementInput()
    {
        captainInput.Player.Move.performed += context => moveInput = context.ReadValue<Vector2>();
        captainInput.Player.Move.canceled += context => moveInput = context.ReadValue<Vector2>();
    }

    void Move()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * moveSpeed, playerRB.velocity.y);
        playerRB.velocity = playerVelocity;
        bool playerHasHorizontalMovement = Mathf.Abs(playerRB.velocity.x) > Mathf.Epsilon;
        playerAnim.SetBool(State.Running.ToString(), playerHasHorizontalMovement);
        if (playerHasHorizontalMovement)
        {
            transform.localScale = new Vector3(Mathf.Sign(playerRB.velocity.x), transform.localScale.y, transform.localScale.z);
        }
    }
    #endregion

    public enum State
    {
        Idling,
        Running,
        Jumping,
        Falling,
        Attacking,
    }
}
