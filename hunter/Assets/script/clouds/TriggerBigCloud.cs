using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBigCloud : MonoBehaviour
{
    [SerializeField] GameObject bigCloud;
    // Start is called before the first frame update
    void Start()
    {
        bigCloud = GameObject.FindGameObjectWithTag("BigCloud");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        bigCloud.GetComponent<BigCloud>().ResetBigCloud();
        Debug.Log("va cham");
    }
}
