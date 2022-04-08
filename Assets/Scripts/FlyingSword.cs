using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingSword : MonoBehaviour
{
    [SerializeField] float flySpeed;
    Animator swordAnim;
    Rigidbody2D swordRb;

    private void Awake()
    {
        swordRb = GetComponent<Rigidbody2D>();
        swordAnim = GetComponent<Animator>();
    }
    void Start()
    {
        swordRb.velocity = new Vector2(flySpeed, 0);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag != "Player")
        {
            swordAnim.SetTrigger("hitObject");
        }
    }
}
