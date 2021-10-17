using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phone : MonoBehaviour
{
    private StickMovement movementScript;
    void Start()
    {
        movementScript = StickMovement.movementScript;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Stick") || collision.gameObject.CompareTag("DustBlock"))
        {
            movementScript.isCollidingWithPhone = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.CompareTag("Stick") || collision.gameObject.CompareTag("DustBlock"))
        {
            movementScript.isCollidingWithPhone = false;
        }
    }
}
