using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinkStar : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rigi;
    SpriteRenderer spriteRenderer;
    [SerializeField] float speed = 2;
    [SerializeField] GameObject dustRight;
    [SerializeField] GameObject dustLeft;

    [SerializeField] float hp = 100;

    private bool isFacingLeft = true;

    [SerializeField] GameObject attackEffectRight;
    [SerializeField] GameObject attackEffectLeft;
    [SerializeField] GameObject dustFall;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rigi = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (GetComponent<SpriteRenderer>().flipX) isFacingLeft = false;
    }

    // Update is called once per frame
    void Update()
    {
        isFacingLeft = !GetComponent<SpriteRenderer>().flipX;
        if (Input.GetKey("a"))
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
            spriteRenderer.flipX = false;
            animator.SetInteger("stateIndex", 1);
            dustRight.SetActive(true);
            dustLeft.SetActive(false);
        }
        if (Input.GetKeyUp("a")) Idle();
        if (Input.GetKey("d"))
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
            spriteRenderer.flipX = true;
            animator.SetInteger("stateIndex", 1);
            dustRight.SetActive(false);
            dustLeft.SetActive(true);
        }
        if (Input.GetKeyUp("d")) Idle();
        if (Input.GetKey(KeyCode.Space))
        {
            animator.SetInteger("stateIndex", 6);
        }
    }

    public void disableTrans()
    {
        animator.SetBool("inTrans", false);
    }

    public void enableTrans()
    {
        animator.SetBool("inTrans", true);
    }

    void OnMouseDown()
    {
        Hit(50);
    }

    void Hit(float damage)
    {
        hp -= damage;
        if (hp > 0) animator.SetInteger("stateIndex", 8);
        else animator.SetInteger("stateIndex", 9);
        rigi.velocity = new Vector2(0, 0);
        rigi.AddForce(new Vector2(100, 100));
    }

    void Idle()
    {
        animator.SetInteger("stateIndex", 0);
        dustLeft.SetActive(false);
        dustRight.SetActive(false);
        attackEffectRight.SetActive(false);
        attackEffectLeft.SetActive(false);
        rigi.velocity = new Vector2(0, 0);
    }

    void SetState(int index)
    {
        animator.SetInteger("stateIndex", index);
    }

    void Attack()
    {
        animator.SetInteger("stateIndex", 7);
        if (isFacingLeft)
        {
            attackEffectRight.SetActive(true);
            rigi.velocity = new Vector2(-5, 0);
        }
        else
        {
            attackEffectLeft.SetActive(true);
            rigi.velocity = new Vector2(5, 0);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            dustFall.SetActive(true);
            if (animator.GetInteger("stateIndex") == 9)
            {
                animator.SetInteger("stateIndex", 10);
            }
            else
            {
                animator.SetInteger("stateIndex", 5);
            }
        }
    }
}
