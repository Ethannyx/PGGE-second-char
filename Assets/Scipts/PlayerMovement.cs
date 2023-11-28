using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    [HideInInspector]
    public CharacterController mCharacterController;
    public Animator mAnimator;

    //Walking
    public float mWalkSpeed = 1.0f;
    public float mRotationSpeed = 50.0f;

    //Attacking
    private enum AttackPattern
    {
        Pattern1,
        Pattern2
    }
    private AttackPattern currentAttackPattern = AttackPattern.Pattern1;

    //Jumping
    /*public float jumpHeight = 5f; 
    public float gravity = 9.8f;  
    private bool isJumping = false;
    private float yVelocity = 0f; */
    
    //Crouching
    private bool isCrouching = false;
    private float crouchDuration = 2.0f;
    private bool isJumping = false;
    private float jumpDuration = 0.2f;

    void Start()
    {
        mCharacterController = GetComponent<CharacterController>();
    }

    void Update()
    {

        //Movement
        float hInput = Input.GetAxis("Horizontal");
        float vInput = Input.GetAxis("Vertical");
        float speed = mWalkSpeed;
        //Sprinting
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = mWalkSpeed * 2.0f;
        }   
        //running front walking back movement
        if (mAnimator == null) return;
        transform.Rotate(0.0f, hInput * mRotationSpeed * Time.deltaTime,
        0.0f);
        Vector3 forward =
        transform.TransformDirection(Vector3.forward).normalized;
        forward.y = 0.0f;
        mCharacterController.Move(forward * vInput * speed *
        Time.deltaTime);
        mAnimator.SetFloat("PosX", hInput * speed / (2.0f * mWalkSpeed));
        mAnimator.SetFloat("PosZ", vInput * speed / (2.0f * mWalkSpeed));

        //jumping animation
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            StartCoroutine(JumpAnimation()); 
            StartCoroutine(LandingAnimation());
        }

        //crouch animation
        if (Input.GetKeyDown(KeyCode.F) && !isCrouching)
        {
        if (mAnimator != null)
        {
            StartCoroutine(CrouchAnimation());   
        }

        //Attack animation
        }
        if (Input.GetMouseButtonDown(0))
        {
        if (mAnimator != null)
        {
            // Checks the current attack pattern and trigger the corresponding animation
            if (currentAttackPattern == AttackPattern.Pattern1)
            {
                attackPattern1();
            }
            else if (currentAttackPattern == AttackPattern.Pattern2)
            {
                attackPattern2();
            }
        }
        }
        //Checks if E is pressed
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Switch the attack pattern when the "E" key is pressed
            SwitchAttackPattern();
            Debug.Log("Attack Pattern Switched");
        }

    }






    //Switches the current attack pattern
    void SwitchAttackPattern()
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

    //Attack Pattern Triggers
    void attackPattern1()
    {
        mAnimator.SetTrigger("Punch");
    }
    void attackPattern2()
    {
        mAnimator.SetTrigger("ComboPunch");
    }

    //Changing y value due to jumping

    //Coroutine for crouching
    IEnumerator CrouchAnimation()
    {
        isCrouching = true;
        mAnimator.SetTrigger("Crouch");

        float elapsedTime = 0f;
        Vector3 originalPosition = transform.position;
        Vector3 targetDownPosition = new Vector3(originalPosition.x, originalPosition.y - 0.5f, originalPosition.z);

        while (elapsedTime < crouchDuration)
        {
            transform.position = Vector3.Lerp(originalPosition, targetDownPosition, elapsedTime / crouchDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure that the final position is exactly the target position
        transform.position = targetDownPosition;

        yield return new WaitForSeconds(2.4f);

        // Change the y value after the crouch animation is finished
        Vector3 targetUpPosition = new Vector3(originalPosition.x, originalPosition.y + 0.5f, originalPosition.z);
        elapsedTime = 0f;

        while (elapsedTime < crouchDuration)
        {
            transform.position = Vector3.Lerp(targetDownPosition, targetUpPosition, elapsedTime / crouchDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure that the final position is exactly the target up position
        transform.position = targetUpPosition;

        isCrouching = false;
    }

    /*void ChangeYValue(float amount)
    {
        // Directly modify the y value of the object's position
        transform.position += new Vector3(0f, amount, 0f);
    }*/

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


