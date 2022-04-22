using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    Animator playerAnim;
    private void Awake()
    {
        playerAnim = GetComponent<Animator>();
    }
    private void Update()
    {
        ChangeAnim();
    }

    public void ChangeAnim()
    {
            for (int i = 0; i < (int)PlayerControl.State.Count; i++)
            {
                string temp = ((PlayerControl.State)i).ToString();
                if (((PlayerControl.State)i).ToString() == PlayerControl.currentState)
                {
                    playerAnim.SetBool(((PlayerControl.State)i).ToString(), true);
                }
                else
                {
                    playerAnim.SetBool(((PlayerControl.State)i).ToString(), false);
                }
            }
        
    }
}
