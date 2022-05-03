using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    bool hasCollided = false;
    [SerializeField] float speed = 5;
    public int facingLeft = 1;
    private IEnumerator coroutine;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!this.hasCollided)
        {
            this.transform.Translate(Vector2.left * this.speed * Time.deltaTime * this.facingLeft);
        }
    }

    public IEnumerator DeactiveDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        this.gameObject.SetActive(false);
    }

    public void GenerateSpike()
    {
        this.gameObject.SetActive(true);
        this.coroutine = DeactiveDelay(5f);
        StartCoroutine(this.coroutine);
        this.hasCollided = false;
        animator = GetComponent<Animator>();
        this.animator.SetInteger("isDestroyed", 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            this.hasCollided = true;
            animator.SetInteger("isDestroyed", 1);
            StopCoroutine(this.coroutine);
        }
    }

    public void SetFacingLeft(int index)
    {
        this.facingLeft = index;
    }

    public void DeactiveBall()
    {
        this.gameObject.SetActive(false);
    }

    void Hit()
    {

    }
}
