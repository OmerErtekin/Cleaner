using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glue : MonoBehaviour
{
    private DustController dustControllerScript;
    private GameManager managerScript;

    private void Start()
    {
        managerScript = GameManager.managerScript;
        dustControllerScript = DustController.dustControllerScript;
    }
    private void OnTriggerEnter(Collider other)
    {
        //A glue remove the dust block that it collided. If there isn't any block, than game will be over.
        if(other.gameObject.CompareTag("DustBlock"))
        {
            dustControllerScript.RemoveDust();
        }
        if(other.gameObject.CompareTag("Stick"))
        {
            managerScript.FinishTheGame();
        }
    }
}
