using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    Animator playerRB;
    private void Awake()
    {
        playerRB = GetComponent<Animator>();
    }
    private void Start()
    {
        
    }
}
