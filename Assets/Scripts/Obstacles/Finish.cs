using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    private GameManager managerScript;
    private StickMovement movementScript;
    private DustController dustScript;
    void Start()
    {
        managerScript = GameManager.managerScript;
        movementScript = StickMovement.movementScript;
        dustScript = DustController.dustControllerScript;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("DustBlock"))
        {
            movementScript.isCameToFinish = true;
            dustScript.RemoveDust();
        }

        if (other.gameObject.CompareTag("Stick"))
        {
            movementScript.isCameToFinish = true;
            StartCoroutine(managerScript.FinishTheGame());
        }
    }
}
