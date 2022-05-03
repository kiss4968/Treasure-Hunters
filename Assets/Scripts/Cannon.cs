using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rigi;
    SpriteRenderer spriteRenderer;
    [SerializeField] GameObject fireEffect;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rigi = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (Input.GetKey(KeyCode.Space))
            {
                Fire();
            }
        }
        else
        {
            Idle();
        }
    }

    void Fire()
    {
        animator.SetInteger("stateIndex", 1);
    }

    void Idle()
    {
        animator.SetInteger("stateIndex", 0);
    }

    void ActiveEffect()
    {
        fireEffect.SetActive(true);
    }

    void DeactiveEffect()
    {
        fireEffect.SetActive(false);
    }
}
