using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float holdForce;
    [SerializeField] float minJump, varJump;
    [SerializeField] int numberOfJumps;
    CaptainInput captainInput;
    Rigidbody2D playerRB;
    Vector2 moveInput;
    Animator playerAnim;
    bool jumpButtonHold;
    bool onAir;
    float timeHoldJump = 0;
    float maxHoldTime = 0.5f;
    int jumpTimes = 0;

    private void Awake()
    {
        playerRB = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<Animator>();
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
        if(jumpTimes < numberOfJumps)
        {
            jumpButtonHold = true;
            timeHoldJump = 0f;
        }        
        jumpTimes++;
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
        FlipSprite();
    }

    private void Jump()
    {
        if (jumpButtonHold && timeHoldJump < maxHoldTime && Mathf.Abs(playerRB.velocity.y) > 0)
        {
            timeHoldJump = Mathf.Clamp(timeHoldJump, 0, 1);
            timeHoldJump += Time.fixedDeltaTime;
            Vector2 jumper = new Vector2(playerRB.velocity.x, minJump + varJump * timeHoldJump);
            playerRB.velocity = jumper;
        }
    }

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
    }
    void FlipSprite()
    {
        bool playerHasHorizontalMovement = Mathf.Abs(playerRB.velocity.x) > Mathf.Epsilon;
        if (playerHasHorizontalMovement)
        {
           transform.localScale = new Vector3(Mathf.Sign(playerRB.velocity.x), transform.localScale.y, transform.localScale.z);
        }
    }

    
    #region Air and Ground Check
    private void OnCollisionExit2D(Collision2D other)
    {
        if(other.gameObject.tag == "Ground")
        {
            onAir = true;
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            onAir = false;
            jumpTimes = 0;
        }
    }
    #endregion
}
