using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotemHead : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rigi;
    SpriteRenderer spriteRenderer;
    [SerializeField] GameObject spike;
    [SerializeField] List<GameObject> spikePool;
    [SerializeField] GameObject muzzleLeft;
    [SerializeField] GameObject muzzleRight;

    [SerializeField] float hp = 100;

    [SerializeField] GameObject frag1;
    [SerializeField] GameObject frag2;
    [SerializeField] GameObject frag3;

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
        animator.SetInteger("stateIndex", 1);
    }

    void Idle()
    {
        animator.SetInteger("stateIndex", 0);
    }

    GameObject getSpike()
    {
        if (this.GetComponent<SpriteRenderer>().flipX == false)
        {
            muzzle = muzzleLeft.transform.position;
        }
        else
        {
            muzzle = muzzleRight.transform.position;
        }
        foreach (GameObject b in spikePool)
        {
            if (b.gameObject.activeSelf) continue;
            return b;
        }
        GameObject newSpike = Instantiate(spike, muzzle, Quaternion.identity);
        spikePool.Add(newSpike);
        return newSpike;
    }

    public void CreateSpike()
    {
        GameObject firedSpike = getSpike();
        firedSpike.GetComponent<Spike>().GenerateSpike();
        firedSpike.transform.position = muzzle;
        int indexFacing = 1;
        if (this.GetComponent<SpriteRenderer>().flipX)
        {
            indexFacing = -1;
            firedSpike.GetComponent<SpriteRenderer>().flipX = true;
        }
        firedSpike.GetComponent<Spike>().SetFacingLeft(indexFacing);
    }

    void Hit(float damage)
    {
        hp -= damage;
        if (hp > 0) animator.SetInteger("stateIndex", 2);
        else animator.SetInteger("stateIndex", 3);
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
        frag1.GetComponent<Rigidbody2D>().AddForce(new Vector2(50, 200));
        frag2.GetComponent<Rigidbody2D>().AddForce(new Vector2(-200, 100));
        frag3.GetComponent<Rigidbody2D>().AddForce(new Vector2(200, 100));
        this.gameObject.SetActive(false);
    }
}
