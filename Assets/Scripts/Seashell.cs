using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seashell : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rigi;
    SpriteRenderer spriteRenderer;
    [SerializeField] GameObject pearl;
    [SerializeField] List<GameObject> pearlPool;
    [SerializeField] GameObject muzzleLeft;
    [SerializeField] GameObject muzzleRight;

    [SerializeField] float hp = 100;

    [SerializeField] GameObject frag1;
    [SerializeField] GameObject frag2;
    [SerializeField] GameObject frag3;
    [SerializeField] GameObject frag4;

    Vector3 muzzle;

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
            Shoot();
        }
    }

    void OnMouseDown()
    {
        Hit(50);
    }

    void Shoot()
    {
        animator.SetInteger("stateIndex", 3);
    }

    void Idle()
    {
        animator.SetInteger("stateIndex", 0);
    }

    GameObject getPearl()
    {
        if (this.GetComponent<SpriteRenderer>().flipX == false)
        {
            muzzle = muzzleLeft.transform.position;
        }
        else
        {
            muzzle = muzzleRight.transform.position;
        }
        foreach (GameObject b in pearlPool)
        {
            if (b.gameObject.activeSelf) continue;
            return b;
        }
        GameObject newpearl = Instantiate(pearl, muzzle, Quaternion.identity);
        pearlPool.Add(newpearl);
        return newpearl;
    }

    public void CreatePearl()
    {
        GameObject firedpearl = getPearl();
        firedpearl.GetComponent<Spike>().GenerateSpike();
        firedpearl.transform.position = muzzle;
        int indexFacing = 1;
        if (this.GetComponent<SpriteRenderer>().flipX)
        {
            indexFacing = -1;
            firedpearl.GetComponent<SpriteRenderer>().flipX = true;
        }
        firedpearl.GetComponent<Spike>().SetFacingLeft(indexFacing);
    }

    void Hit(float damage)
    {
        hp -= damage;
        if (hp > 0) animator.SetInteger("stateIndex", 4);
        else animator.SetInteger("stateIndex", 5);
    }

    void EndOfHit()
    {
        if (hp > 0)
        {
            animator.SetInteger("stateIndex", 0);
        }
    }

    void Crash()
    {
        frag1.SetActive(true);
        frag2.SetActive(true);
        frag3.SetActive(true);
        frag4.SetActive(true);
        frag1.GetComponent<Rigidbody2D>().AddForce(new Vector2(50, 200));
        frag2.GetComponent<Rigidbody2D>().AddForce(new Vector2(100, 100));
        frag3.GetComponent<Rigidbody2D>().AddForce(new Vector2(-100, 100));
        frag4.GetComponent<Rigidbody2D>().AddForce(new Vector2(-100, 200));
        this.gameObject.SetActive(false);
    }
}
