using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementSound : MonoBehaviour
{
    public AudioClip grassFootstepSound;
    public AudioClip concreteFootstepSound;
    public AudioClip metalFootstepSound;
    public float raycastDistance = 0.1f;
    public LayerMask grassLayer;
    public LayerMask concreteLayer;
    public LayerMask metalLayer;
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
        if (Physics.Raycast(transform.position, Vector3.down, out hit, raycastDistance, grassLayer))
        {
            //Set the default footstep sound
            aud.Stop();
            aud.clip = grassFootstepSound;
        }
        else if ((Physics.Raycast(transform.position, Vector3.down, out hit, raycastDistance, concreteLayer)))
        {
            // if player is on grass
            aud.Stop();
            aud.clip = concreteFootstepSound;
        }
        else if ((Physics.Raycast(transform.position, Vector3.down, out hit, raycastDistance, metalLayer)))
        {
            aud.Stop();
            aud.clip = metalFootstepSound;
        }

        // Play the footstep sound
        aud.Play();
    }
}
