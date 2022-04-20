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

    private void Update()
    {
        transform.Translate(new Vector3(flySpeed * Time.deltaTime, 0, 0));
    }

    
    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("hit");
        if(other.gameObject.tag != "Player")
        {
            swordAnim.SetTrigger("hitObject");
            Vector3 currentPos = transform.position;
            transform.position = currentPos;
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
