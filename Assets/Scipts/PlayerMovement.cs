using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    [HideInInspector]
    public CharacterController mCharacterController;
    public Animator mAnimator;
    public int currentammo = 4;

    //Walking
    public float mWalkSpeed = 1.0f;
    public float mRotationSpeed = 50.0f;

    //Referencing functions from other codes
    private JumpAnim jumplanding;
    private AttackPatterns attacking;
    private Crouching crouch;


    void Start()
    {
        mCharacterController = GetComponent<CharacterController>();
        jumplanding = GetComponent<JumpAnim>();
        attacking = GetComponent<AttackPatterns>();
        crouch = GetComponent<Crouching>();
    }

    void Update()
    {

        //Movement
        float hInput = Input.GetAxis("Horizontal");
        float vInput = Input.GetAxis("Vertical");
        float speed = mWalkSpeed;
        bool isMoving = hInput != 0.0f || vInput != 0.0f;
        

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
        if (Input.GetKeyDown(KeyCode.Space) && !jumplanding.isJumping)
        {
            jumplanding.Jumpanims();
        }

        //crouch animation
        if (Input.GetKeyDown(KeyCode.F))
        {
        if (mAnimator != null)
        {
            crouch.ToggleCrouch();
        }

        //Attack animation
        }
        if (Input.GetMouseButtonDown(0))
        {
            if (mAnimator != null)
            {
               if (currentammo > 0)
                {
                    attacking.PlayAttackPattern();
                    currentammo = Mathf.Max(0, currentammo - 1);
                    Debug.Log("current ammo is " + currentammo);
                }
                else
                {
                    Debug.Log("Please reload");
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            attacking.SwitchPattern();
            Debug.Log("Attack Pattern Switched");
        }

        //Reload Animation
        if (Input.GetKeyDown(KeyCode.R))
        {
        if (mAnimator != null)
        {
            mAnimator.SetTrigger("Reload");
            currentammo = 4;
        }
        }
    }
}



