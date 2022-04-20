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
    public static string currentState;
    bool running, jumping, attacking;
    public enum State
    {
        Idling,
        Running,
        Jumping,
        Falling,
        Attacking,
        Count
    }

    private void Awake()
    {   
        captainInput = new CaptainInput();
        MovementInput();
        captainInput.Player.Jump.performed += Jump_performed;
        captainInput.Player.Jump.canceled += Jump_canceled;
        currentState = State.Idling.ToString();
    }
    #region Jump Input
    private void Jump_canceled(InputAction.CallbackContext obj)
    {
        jumpButtonHold = false;
        jumping = false;
    }

    private void Jump_performed(InputAction.CallbackContext obj)
    {
        jumpTimes++;
        if (jumpTimes <= numberOfJumps)
        {
            jumping = true;
            jumpButtonHold = true;            
            timeHoldJump = 0f;
        }
    }
    #endregion

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
        ChangeState();
    }

    

    private void Jump()
    {
        if (jumpButtonHold && timeHoldJump < maxHoldTime)
        {
            timeHoldJump = Mathf.Clamp(timeHoldJump, 0, maxHoldTime);
            timeHoldJump += Time.fixedDeltaTime;
            Vector2 jumper = new Vector2(playerRB.velocity.x, minJump + varJump * timeHoldJump);
            playerRB.velocity = jumper;
        }
        if (playerFeet.IsTouchingLayers(LayerMask.GetMask("Ground")) && playerRB.velocity.y <= 0)
        {
            jumpTimes = 0;
        }
    }
    #region Move Methods
    private void MovementInput()
    {
        captainInput.Player.Move.performed += context => moveInput = context.ReadValue<Vector2>();
        captainInput.Player.Move.canceled += context => moveInput = context.ReadValue<Vector2>();
    }

    void Move()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * moveSpeed, playerRB.velocity.y);
        playerRB.velocity = playerVelocity;
        running = Mathf.Abs(playerRB.velocity.x) > Mathf.Epsilon;                
        if (running)
        {
            transform.localScale = new Vector3(Mathf.Sign(playerRB.velocity.x), transform.localScale.y, transform.localScale.z);
            
        }
        else
        {
            if (playerFeet.IsTouchingLayers(LayerMask.GetMask("Ground")))
            {
                currentState = State.Idling.ToString();
            }
        }
    }
    #endregion
    void ChangeState()
    {
        if (running)
        {
            currentState = State.Running.ToString();
            CameraMovement.cameraMovement.CameraOnRunning();
        }
        if (jumping)
        {
            currentState = State.Jumping.ToString();
        }
        if (playerRB.velocity.y < 0 && !jumping)
        {
            currentState = State.Falling.ToString();
        }
        if (!running && playerFeet.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            currentState = State.Idling.ToString();
            CameraMovement.cameraMovement.CameraOnIdling();
        }
        if (attacking)
        {
            currentState = State.Attacking.ToString();
        }
    }

}
