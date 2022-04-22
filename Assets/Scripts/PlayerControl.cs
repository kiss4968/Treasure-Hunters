using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEditor;

public class PlayerControl : MonoBehaviour
{
    #region Public Variables
    [Header ("Move and Jump")]
    [SerializeField] float moveSpeed;
    [SerializeField] float minJump, varJump;
    [SerializeField] int numberOfJumps = 1;
    [SerializeField] int jumpTimes = 0;
    public static bool onAir;
    [HideInInspector]
    [SerializeField] Rigidbody2D playerRB;
    [HideInInspector]
    [SerializeField] BoxCollider2D playerFeet;
    [SerializeField] CapsuleCollider2D body;
    [SerializeField] Vector2 deathKick;
    public static string currentState;
    [SerializeField] AudioSource soundFX;
    [SerializeField] AudioClip footSteps;
    [SerializeField] AudioClip jumpSFX;
    [SerializeField] PhysicsMaterial2D material2D;
    #endregion

    #region Private Variables
    CaptainInput captainInput;
    Vector2 moveInput;
    bool running, jumping, attacking;
    bool alive = true;
    bool jumpButtonHold;
    float timeHoldJump = 0;
    float maxHoldTime = 0.5f;
    #endregion

    #region States
    public enum State
    {
        Idling,
        Running,
        Jumping,
        Falling,
        Attacking,
        Death,
        Count
    }
    #endregion

    #region Framework Methods
    private void Awake()
    {   
        captainInput = new CaptainInput();
        MovementInput();
        captainInput.Player.Jump.performed += Jump_performed;
        captainInput.Player.Jump.canceled += Jump_canceled;
        body = GetComponent<CapsuleCollider2D>();
        currentState = State.Idling.ToString();
        material2D.friction = 0f;

    }
    void FixedUpdate()
    {
        Move();
        Jump();
        ChangeState();
    }
    #endregion

    #region Jump Logic
    private void Jump_canceled(InputAction.CallbackContext obj)
    {
        jumpButtonHold = false;
        jumping = false;
    }

    private void Jump_performed(InputAction.CallbackContext obj)
    {
        if (!alive) return;
        jumpTimes++;
        if (jumpTimes <= numberOfJumps)
        {
            PlayJumpSFX();
            jumping = true;
            jumpButtonHold = true;
            timeHoldJump = 0f;
        }
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
    
    #region Move Logic
    private void MovementInput()
    {
        captainInput.Player.Move.performed += context => moveInput = context.ReadValue<Vector2>();
        captainInput.Player.Move.canceled += context => moveInput = context.ReadValue<Vector2>();
    }

    void Move()
    {
        if (!alive || CloseCombat.isAttacking) return;        
        Vector2 playerVelocity = new Vector2(moveInput.x * moveSpeed, playerRB.velocity.y);
        playerRB.velocity = playerVelocity;
        running = Mathf.Abs(playerRB.velocity.x) > Mathf.Epsilon;
        if (running)
        {
            transform.localScale = new Vector3(Mathf.Sign(playerRB.velocity.x), transform.localScale.y, transform.localScale.z);
            PlayFootStepsSFX();
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

    #region DEATH
    public void Die()
    {
        alive = false;
        material2D.friction = 1f;
        playerRB.velocity += new Vector2(deathKick.x * transform.localScale.x * -1, deathKick.y);        
        currentState = State.Death.ToString();
        UIManager.uIManager.OpenGameOverPanel();
    }
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            Die();
            Debug.Log("kill");
        }
    }
    #endregion

    #region Change State
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
        if (!alive)
        {
            currentState = State.Death.ToString();
        }
    }
    #endregion

    #region SFX Handler
    
    private void PlayFootStepsSFX()
    {
        if (!soundFX.isPlaying && playerFeet.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            soundFX.clip = footSteps;
            soundFX.Play();
        }
    }
    private void PlayJumpSFX()
    {        
        soundFX.PlayOneShot(jumpSFX);
    }
    #endregion
}
