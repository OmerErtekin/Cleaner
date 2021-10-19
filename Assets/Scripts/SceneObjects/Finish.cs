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
        if(other.gameObject.CompareTag("Stick") && dustScript.dustCount == 0 && !isCame)
        {
            isCame = false;
            managerScript.FinishTheGame();
        }
        if (other.gameObject.CompareTag("DustBlock") && !isCame)
        {
            isCame = true;
            movementScript.isCameToFinish = true;
        }
    }


    void FinalDustRemove()
    {
        if (isCame && dustScript.dustCount != 0 && Time.time > lastDropTime)
        {
            lastDropTime = Time.time + 0.2f;
            dustScript.RemoveDust();
        }
        else if(isCame && dustScript.dustCount == 0 && Time.time > lastDropTime)
        {
            movementScript.isCameToFinish = true;
            managerScript.FinishTheGame();
        }
    }

}
