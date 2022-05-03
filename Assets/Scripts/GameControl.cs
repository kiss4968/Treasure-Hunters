using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    [SerializeField] GameObject objectToSpaw;
    bool isSpawn = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!isSpawn)
            {
                Instantiate(objectToSpaw, this.transform.position, Quaternion.identity);
                isSpawn = true;
            }
            
        }
    }
}
