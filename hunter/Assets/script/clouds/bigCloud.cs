using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigCloud : MonoBehaviour
{
    [SerializeField] float speed = 1f;
    [SerializeField] Rigidbody2D rigi;
    Vector2 mover = new Vector2(-1, 0);
    Vector2 oldposition = new Vector2(31, 1);
    // Start is called before the first frame update
    void Start()
    {
        rigi = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 position = rigi.position;
        position += mover * speed * Time.deltaTime;
        rigi.MovePosition(position);
        
    }
    public void ResetBigCloud()
    {
        this.transform.position = oldposition;
    }

}
