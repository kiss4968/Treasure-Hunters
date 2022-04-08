using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CloseCombat : MonoBehaviour
{
    [Header("Attack")]
    [SerializeField] Transform attackPoint;
    [SerializeField] float attackRange = 1f;
    [SerializeField] GameObject throwPrefabs;
    Animator playerAnim;
    [SerializeField] float throwDelay;
    bool canThrow = true;
    bool canReceiveInput = true;

    private void Awake()
    {
        playerAnim = GetComponent<Animator>();
    }

    public void Attack(InputAction.CallbackContext obj)
    {
        if (obj.performed)
        {
            PerformAttack1();
        }
        if (!canReceiveInput) return;
        if (obj.performed)
        {
            Debug.Log("Get");
            
            StartCoroutine(WaitForCombo());
            PerformAttack2();
        }
    }
    public void Throw(InputAction.CallbackContext obj)
    {
        if (!canThrow) return;
        StartCoroutine(ThrowDelay());
        Instantiate(throwPrefabs, attackPoint.position, Quaternion.identity);
    }
    IEnumerator WaitForCombo()
    {
        canReceiveInput = false;
        yield return new WaitForSeconds(playerAnim.GetCurrentAnimatorStateInfo(0).length);
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
        playerAnim.SetTrigger("Attack1");
    }

    public void PerformAttack2()
    {
        playerAnim.SetTrigger("Attack2");
        playerAnim.ResetTrigger("Attack1");
    }
    public void PerformAttack3()
    {
        playerAnim.SetTrigger("Attack3");
        playerAnim.ResetTrigger("Attack2");
    }
}
