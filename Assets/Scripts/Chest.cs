using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    Animator animator;
    [SerializeField] GameObject paddle;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown()
    {
        Open();
    }

    void Open()
    {
        animator.SetInteger("stateIndex", 1);
    }

    void PaddleFly()
    {
        paddle.SetActive(true);
        paddle.GetComponent<Rigidbody2D>().AddForce(new Vector2(100, 100));
    }
}
