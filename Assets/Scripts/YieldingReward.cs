using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YieldingReward : MonoBehaviour
{
    [SerializeField] List<GameObject> rewards = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void YieldReward()
    {
        foreach (var reward in rewards)
        {
            GameObject spawnReward = Instantiate(reward, this.gameObject.transform.position, Quaternion.identity);
            spawnReward.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-200f, 200f), 100));
        }
    }
}
