using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBallControl : MonoBehaviour
{
    [SerializeField] GameObject cannonBall;
    [SerializeField] List<GameObject> cannonBallPool;
    [SerializeField] GameObject cannonMuzzleLeft;
    [SerializeField] GameObject cannonMuzzleRight;
    Vector3 muzzle;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    GameObject getBall()
    {
        if (this.GetComponent<SpriteRenderer>().flipX == false)
        {
            muzzle = cannonMuzzleLeft.transform.position;
        }
        else
        {
            muzzle = cannonMuzzleRight.transform.position;
        }
        foreach (GameObject b in cannonBallPool)
        {
            if (b.gameObject.activeSelf) continue;
            return b;
        }
        GameObject newBall = Instantiate(cannonBall, muzzle, Quaternion.identity);
        cannonBallPool.Add(newBall);
        return newBall;
    }

    public void CreateBall()
    {
        GameObject firedBall = getBall();
        firedBall.GetComponent<CannonBall>().GenerateBall();
        firedBall.transform.position = muzzle;
        int indexFacing = 1;
        if (this.GetComponent<SpriteRenderer>().flipX) indexFacing = -1;
        firedBall.GetComponent<CannonBall>().SetFacingLeft(indexFacing);
    }
}
