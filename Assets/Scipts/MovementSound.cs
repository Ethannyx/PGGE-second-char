using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementSound : MonoBehaviour
{
    public AudioClip defaultFootstepSound;
    public AudioClip alternativeFootstepSound;
    public float raycastDistance = 0.1f;
    public LayerMask defaultLayer;
    public LayerMask grassLayer;
    AudioSource aud;

    // Start is called before the first frame update
    void Start()
    {
        aud = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // Your other update logic here
    }

    public void playSound()
    {
        // Check if the object is on a specific layer
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, raycastDistance, defaultLayer))
        {
            //Set the default footstep sound
            aud.clip = defaultFootstepSound;
        }
        else if ((Physics.Raycast(transform.position, Vector3.down, out hit, raycastDistance, grassLayer)))
        {
            // if player is on grass
            aud.clip = alternativeFootstepSound;
        }

        // Play the footstep sound
        aud.Play();
    }
}
