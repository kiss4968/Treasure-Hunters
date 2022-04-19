using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingSword : MonoBehaviour
{
    [SerializeField] float flySpeed;
    GameObject player;
    Animator swordAnim;
    Rigidbody2D swordRb;

    private void Awake()
    {
        swordRb = GetComponent<Rigidbody2D>();
        swordAnim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");      
    }
    private void Start()
    {
      //  swordRb.velocity = new Vector2(flySpeed, 0);
    }

    private void Update()
    {
        transform.Translate(new Vector3(4*Time.deltaTime, 0, 0));
    }

    public void FixedUppdate()
    {
        //swordRb.velocity = new Vector2(flySpeed * player.transform.localScale.x, 0);
       // transform.localScale = player.transform.localScale;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("hit");
        if(other.gameObject.tag != "Player")
        {
            swordAnim.SetTrigger("hitObject");
            swordRb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
        if(other.gameObject.tag == "Player")
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Trigger");
    }
}
