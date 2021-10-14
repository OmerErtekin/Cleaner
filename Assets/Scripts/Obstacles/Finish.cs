using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    private GameManager managerScript;
    private StickMovement movementScript;
    private DustController dustScript;
    private bool isCame = false;
    private float lastDropTime = 0;
    void Start()
    {
        managerScript = GameManager.managerScript;
        movementScript = StickMovement.movementScript;
        dustScript = DustController.dustControllerScript;
    }

    private void LateUpdate()
    {
        FinalDustRemove();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("DustBlock"))
        {
            isCame = true;
            movementScript.isCameToFinish = true;
        }

        if (other.gameObject.CompareTag("Stick"))
        {
            movementScript.isCameToFinish = true;
            managerScript.FinishTheGame();
        }
    }


    void FinalDustRemove()
    {
        if (isCame && dustScript.dustCount != 0 && Time.time > lastDropTime)
        {
            lastDropTime = Time.time + 0.15f;
            dustScript.RemoveDust();
        }
    }

}
