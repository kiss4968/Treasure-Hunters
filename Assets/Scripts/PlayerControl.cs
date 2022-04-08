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
    CaptainInput captainInput;
    Rigidbody2D playerRB;
    BoxCollider2D playerFeet;
    Vector2 moveInput;
    Animator playerAnim;
    bool jumpButtonHold;
    float timeHoldJump = 0;
    float maxHoldTime = 0.5f;
    int jumpTimes = 0;

    private void Awake()
    {
        playerRB = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<Animator>();
        playerFeet = GetComponent<BoxCollider2D>();
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
            playerAnim.ResetTrigger("isFalling");
            playerAnim.SetBool("onGround", true);
            jumpTimes = 0;
        }
        if (playerRB.velocity.y < 0)
        {
            playerAnim.SetTrigger("isFalling");
            playerAnim.ResetTrigger("isJumping");
        }
    }

    private void Jump()
    {
        if (jumpButtonHold && timeHoldJump < maxHoldTime)
        {
            playerAnim.ResetTrigger("isFalling");
            timeHoldJump = Mathf.Clamp(timeHoldJump, 0, maxHoldTime);
            timeHoldJump += Time.fixedDeltaTime;
            Vector2 jumper = new Vector2(playerRB.velocity.x, minJump + varJump * timeHoldJump);
            playerRB.velocity = jumper;
            playerAnim.SetTrigger("isJumping");
            playerAnim.SetBool("onGround", false);
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
        playerAnim.SetBool("isRunning", playerHasHorizontalMovement);
        if (playerHasHorizontalMovement)
        {
            transform.localScale = new Vector3(Mathf.Sign(playerRB.velocity.x), transform.localScale.y, transform.localScale.z);
        }
    }
    #endregion

    
}
