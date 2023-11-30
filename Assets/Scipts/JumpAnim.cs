using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpAnim : MonoBehaviour
{
    public bool isJumping = false;
    public float jumpDuration = 0.2f;
    public Animator mAnimator;
    
    public void Jumpanims()
    {
        StartCoroutine(JumpAnimation()); 
        StartCoroutine(LandingAnimation());
    }

    IEnumerator JumpAnimation()
    {
        isJumping = true;
        mAnimator.SetTrigger("Jump");

        float elapsedTime = 0f;
        Vector3 originalPosition = transform.position;
        Vector3 targetPosition = new Vector3(originalPosition.x, originalPosition.y + 1.0f, originalPosition.z);

        while (elapsedTime < jumpDuration)
        {
            transform.position = Vector3.Lerp(originalPosition, targetPosition, elapsedTime / jumpDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure that the final position is exactly the target position
        transform.position = targetPosition;
    }

    IEnumerator LandingAnimation()
    {
        yield return new WaitForSeconds(0.5f);
        mAnimator.SetTrigger("Landing");

        float elapsedTime = 0f;
        Vector3 originalPosition = transform.position;
        Vector3 targetDownPosition = new Vector3(originalPosition.x, originalPosition.y - 1.5f, originalPosition.z);

        // Move down
        while (elapsedTime < jumpDuration)
        {
            transform.position = Vector3.Lerp(originalPosition, targetDownPosition, elapsedTime / jumpDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure that the final position is exactly the target down position
        transform.position = targetDownPosition;

        // Delay before moving up
        yield return new WaitForSeconds(1.0f);

        // Upward movement
        Vector3 targetUpPosition = new Vector3(originalPosition.x, originalPosition.y - 1.0f, originalPosition.z);
        elapsedTime = 0f;

        while (elapsedTime < jumpDuration)
        {
            transform.position = Vector3.Lerp(targetDownPosition, targetUpPosition, elapsedTime / jumpDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure that the final position is exactly the target up position
        transform.position = targetUpPosition;

        isJumping = false;
    }

}
