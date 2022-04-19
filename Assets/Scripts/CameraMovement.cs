using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] Camera mainCamera;


    private void FixedUpdate()
    {
        transform.position = player.transform.position + new Vector3(0, 0, -10);
        mainCamera.orthographicSize = 7f;
    }
}
