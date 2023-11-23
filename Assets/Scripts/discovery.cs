using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class discovery : MonoBehaviour
{
    private bool isDiscovered = false;
    private Rigidbody rb;
    
    void Start()
    {
        //rb = GetComponent<Rigidbody>();
        //if (rb != null)
        //{
        //  rb.isKinematic = true; // Set to true to allow manual control
        //}

        //GetComponent<Renderer>().enabled = false; // Set to false to make the object invisible
        GetComponent<Renderer>().enabled = true;
    }
    
    void OnCollisionEnter(Collision collision)
    {
        // Check if the collider is from a VR hand
        if (collision.gameObject.CompareTag("Hand") && !isDiscovered)
        {
       
                GetComponent<Renderer>().enabled = true;
         
            // Enable physics on the Rigidbody
            //if (rb != null)
            //{
                //rb.isKinematic = false;
            //}

            isDiscovered = true; // Set to true to prevent repeated discovery
        }
    }
}
