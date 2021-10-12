using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glue : MonoBehaviour
{
    private DustController dustControllerScript;

    private void Start()
    {
        dustControllerScript = DustController.dustControllerScript;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Stick"))
        {
            if(dustControllerScript.dustCount != 0)
                dustControllerScript.RemoveDust();
            //else
                //Game over
        }
    }
}
