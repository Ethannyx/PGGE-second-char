using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crouching : MonoBehaviour
{
    public bool isCrouching = false;
    public Animator mAnimator;
    
    public void ToggleCrouch()
    {
        if (isCrouching)
        {
            unCrouch();
        }
        else
        {
            Crouch();
        }
    }

    void Crouch()
    {
        mAnimator.SetBool("Crouch", true); 
        isCrouching = true;
    }

    void unCrouch()
    {
        mAnimator.SetBool("Crouch", false); 
        isCrouching = false;
    }
}
