using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    public static Pool swordPool;
    [SerializeField] GameObject sword;
    List<GameObject> swords = new List<GameObject>();
    [SerializeField] int maxAmount;
    private void Awake()
    {
        swordPool = this;
    }
    private void Start()
    {
        for (int i = 0; i < maxAmount; i++)
        {
            swords.Add(Instantiate(sword, transform.position, Quaternion.identity));
            swords[i].SetActive(false);

        }
    }
    public GameObject GetPooledSwords()
    {
        for(int i = 0; i < maxAmount; i++)
        {
            if (!swords[i].activeInHierarchy)
            {
                return swords[i];
            }            
        }
        return null;
    }
}
