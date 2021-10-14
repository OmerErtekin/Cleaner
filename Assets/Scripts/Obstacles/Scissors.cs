using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scissors : MonoBehaviour
{
    private GameManager managerScript;
    private DustController controllerScript;
    private bool isCut = false;
   
    void Start()
    {
        managerScript = GameManager.managerScript;
        controllerScript = DustController.dustControllerScript;
    }

    private void OnTriggerEnter(Collider other)
    {
        //A scissors cut can cut the dust from touched dust to last dust
        if (other.gameObject.CompareTag("DustBlock") && !isCut)
        {
            isCut = true;
            StartCoroutine(controllerScript.ScissorsCut(other.gameObject));
        }
        if(other.gameObject.CompareTag("Stick") && !isCut)
        {
            isCut = true;
            managerScript.FinishTheGame();
        }
    }
}
