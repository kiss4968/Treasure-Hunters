using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingSword : MonoBehaviour
{
    [SerializeField] float flySpeed;
    [SerializeField] GameObject player;
    Animator swordAnim;
    Rigidbody2D swordRb;

    private void Awake()
    {
        swordRb = GetComponent<Rigidbody2D>();
        swordAnim = GetComponent<Animator>();
        
    }
    void Start()
    {
        swordRb.velocity = new Vector2(flySpeed * player.transform.localScale.x, 0);
        transform.localScale = player.transform.localScale;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag != "Player")
        {
            swordAnim.SetTrigger("hitObject");
            swordRb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
        if(other.gameObject.tag == "Player")
        {
            Destroy(gameObject, 0.5f);
        }
    }
}
