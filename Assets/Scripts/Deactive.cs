using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deactive : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DeactiveEffect()
    {
        this.gameObject.SetActive(false);
    }

    void DeaciveParent()
    {
        this.transform.parent.gameObject.SetActive(false);
    }

    void ManualDestroy()
    {
        Destroy(this.gameObject);
    }
}
