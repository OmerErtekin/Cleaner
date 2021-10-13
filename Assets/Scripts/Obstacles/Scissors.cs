using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scissors : MonoBehaviour
{
    DustController controllerScript;
    bool isCut = false;
    void Start()
    {
        controllerScript = DustController.dustControllerScript;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("DustBlock") && !isCut)
        {
            isCut = true;
            StartCoroutine(controllerScript.ScissorsCut(other.gameObject));
        }
    }
}
