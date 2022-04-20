using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] Camera mainCamera;
    [SerializeField] float size;
    [SerializeField] float minCamSize, maxCamSize;
    public static CameraMovement cameraMovement;

    private void Awake()
    {
        cameraMovement = this;
        size = minCamSize;
        mainCamera.orthographicSize = size;
    }

    private void FixedUpdate()
    {
        transform.position = player.transform.position + new Vector3(0, 0, -10);
    }
    public void CameraOnRunning()
    {
        size = Mathf.Clamp(size, minCamSize, maxCamSize);
        size += Time.fixedDeltaTime;
        mainCamera.orthographicSize = size;
    }
    public void CameraOnIdling()
    {
        size = Mathf.Clamp(size, minCamSize, maxCamSize);
        size -= Time.fixedDeltaTime;
        mainCamera.orthographicSize = size;
    }
}
