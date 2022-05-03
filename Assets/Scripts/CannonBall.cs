using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    bool hasCollided = false;
    [SerializeField] float speed = 5;
    public int facingLeft = 1;
    private IEnumerator coroutine;
    GameObject childBall;
    GameObject childExplosion;
    // Start is called before the first frame update
    void Start()
    {
        this.childBall = this.transform.GetChild(0).gameObject;
        this.childExplosion = this.transform.GetChild(1).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (!this.hasCollided) this.transform.Translate(Vector2.left * this.speed * Time.deltaTime * this.facingLeft);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("hit");
        if (collision.gameObject.tag=="Ground")
        {
            this.hasCollided = true;
            this.childBall.SetActive(false);
            this.childExplosion.SetActive(true);
            StopCoroutine(this.coroutine);
            //this.coroutine = DeactiveDelay(0.575f);
            //StartCoroutine(this.coroutine);
        }
    }

    public IEnumerator DeactiveDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        this.gameObject.SetActive(false);
    }

    public void GenerateBall()
    {
        this.childBall = this.transform.GetChild(0).gameObject;
        this.childExplosion = this.transform.GetChild(1).gameObject;
        this.gameObject.SetActive(true);
        this.coroutine = DeactiveDelay(5f);
        StartCoroutine(this.coroutine);
        this.childBall.SetActive(true);
        this.childExplosion.SetActive(false);
        this.hasCollided=false;
    }

    public void SetFacingLeft(int index)
    {
        this.facingLeft = index;
    }

    public void DeactiveBall()
    {
        this.gameObject.SetActive(false);
    }
}
