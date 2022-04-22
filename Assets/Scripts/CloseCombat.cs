using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CloseCombat : MonoBehaviour
{
    [Header("Attack")]
    [SerializeField] Transform attackPoint;
    
    Rigidbody2D playerRB;
    Animator playerAnim;
    [SerializeField] float throwDelay;
    bool canThrow = true;
    [SerializeField] bool canReceiveInput = true;
    float attackDelay = 0.5f;
    bool immobilizeMovement;
    public static bool isAttacking;

    private void Awake()
    {
        playerAnim = GetComponentInChildren<Animator>();
        playerRB = GetComponent<Rigidbody2D>();
    }

    public void Attack(InputAction.CallbackContext obj)
    {
        if(!canReceiveInput) { return; }
        if (obj.performed)
        {
            PlayerControl.currentState = PlayerControl.State.Attacking.ToString();
            immobilizeMovement = true;
            canReceiveInput = false;
            isAttacking = true;
            int ran = Random.Range(1, 4);
            switch (ran)
            {
                case 1:
                    PerformAttack1();
                    break;
                case 2:
                    PerformAttack2();
                    break;
                case 3:
                    PerformAttack3();
                    break;
            }
            canReceiveInput = true;
        }
        if (obj.canceled)
        {
            playerAnim.SetBool("Attacking", false);
            immobilizeMovement = false;
            isAttacking = false;
        }
    }

    private void FixedUpdate()
    {
        //ImmobilizeMovement();
    }
    public void Throw(InputAction.CallbackContext obj)
    {
        if (!canThrow) return;
        Coroutine throwDelay = StartCoroutine(ThrowDelay());
        GameObject throwPrefabs = Pool.swordPool.GetPooledSwords();
        if (throwPrefabs != null)
        {
            Debug.Log("Press 2");

            throwPrefabs.transform.position = attackPoint.position;
            throwPrefabs.transform.rotation = Quaternion.identity;            
            throwPrefabs.SetActive(true);
        }
    }

    IEnumerator AttackDelay()
    {
        canReceiveInput = false;
        yield return new WaitForSeconds(attackDelay);
        canReceiveInput = true;
    }
    IEnumerator ThrowDelay()
    {
        canThrow = false;
        yield return new WaitForSeconds(throwDelay);
        canThrow = true;
    }

    public void PerformAttack1()
    {
        playerAnim.SetBool("Attacking",true);
        playerAnim.SetFloat("Speed", 0f);
    }

    public void PerformAttack2()
    {
        playerAnim.SetBool("Attacking", true);
        playerAnim.SetFloat("Speed", 1f);
    }
    public void PerformAttack3()
    {
        playerAnim.SetBool("Attacking", true);
        playerAnim.SetFloat("Speed", 2f);
    }
    void ImmobilizeMovement()
    {
        if (!immobilizeMovement) 
        {
            playerRB.constraints = RigidbodyConstraints2D.None;
            playerRB.freezeRotation = true;
            transform.rotation = Quaternion.identity;
        }
        else
        {
            playerRB.constraints = RigidbodyConstraints2D.FreezePosition;
            playerRB.freezeRotation = true;
        }
    }
    
}
