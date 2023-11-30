using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPatterns : MonoBehaviour
{

    private enum AttackPattern
    {
        Pattern1,
        Pattern2
    }
    private AttackPattern currentAttackPattern = AttackPattern.Pattern1;
    public Animator mAnimator; 
    
    public void SwitchPattern()
    {
        // Toggle between attack patterns (Pattern1 and Pattern2)
        if (currentAttackPattern == AttackPattern.Pattern1)
        {
            currentAttackPattern = AttackPattern.Pattern2;
        }
        else
        {
            currentAttackPattern = AttackPattern.Pattern1;
        }
    }

    public void PlayAttackPattern()
    {
        // Checks the current attack pattern and trigger the corresponding animation
        if (currentAttackPattern == AttackPattern.Pattern1)
        {
            mAnimator.SetTrigger("Punch");
        }
        else if (currentAttackPattern == AttackPattern.Pattern2)
        {
            mAnimator.SetTrigger("ComboPunch");
        }
    }
}
