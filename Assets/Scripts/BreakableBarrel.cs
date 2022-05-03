using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableBarrel : MonoBehaviour
{
    [SerializeField] float hp = 100;
    Animator animator;
    GameObject piece0, piece1, piece2, piece3;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        piece0 = this.transform.parent.GetChild(1).gameObject;
        piece1 = this.transform.parent.GetChild(2).gameObject;
        piece2 = this.transform.parent.GetChild(3).gameObject;
        piece3 = this.transform.parent.GetChild(4).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Hit(float damage)
    {
        hp -= damage;
        if (hp > 0) animator.SetInteger("stateIndex", 1);
        else animator.SetInteger("stateIndex", 2);
    }

    void BackToIdle()
    {
        animator.SetInteger("stateIndex", 0);
    }

    void OnMouseDown()
    {
        Hit(50);
    }

    void Crash()
    {
        piece0.SetActive(true);
        piece1.SetActive(true);
        piece2.SetActive(true);
        piece3.SetActive(true);
        piece0.GetComponent<Rigidbody2D>().AddForce(new Vector2(-100, 200));
        piece1.GetComponent<Rigidbody2D>().AddForce(new Vector2(100, 200));
        piece2.GetComponent<Rigidbody2D>().AddForce(new Vector2(-100, 50));
        piece3.GetComponent<Rigidbody2D>().AddForce(new Vector2(100, 50));
        this.gameObject.SetActive(false);
    }
}
