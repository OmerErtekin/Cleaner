using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scissors : MonoBehaviour
{
    private GameManager managerScript;
    private DustController controllerScript;
    private bool isCut = false,isClosing = false;
    public GameObject stickA, stickB;
    private float lastCloseTime = 0;
   
    void Start()
    {
        managerScript = GameManager.managerScript;
        controllerScript = DustController.dustControllerScript;
    }

    void Update()
    {
        OpenAndClose();
    }

    private void OnTriggerEnter(Collider other)
    {
        //A scissors cut can cut the dust from touched dust to last dust
        if (other.gameObject.CompareTag("Stick") && !isCut)
        {
            isCut = true;
            managerScript.FinishTheGame();
        }
        if (other.gameObject.CompareTag("DustBlock") && !isCut)
        {
            isCut = true;
            StartCoroutine(controllerScript.ScissorsCut(other.gameObject));
        }
    }

    void OpenAndClose()
    {
        if(Time.time > lastCloseTime)
        {
            if (isClosing)
                lastCloseTime = Time.time + 0.25f;
            else
                lastCloseTime = Time.time + 0.75f;

            isClosing = !isClosing;
        }
        if (!isClosing)
        {
            stickA.transform.Rotate(0, -240 * Time.deltaTime, 0);
            stickB.transform.Rotate(0, 240 * Time.deltaTime, 0);
        }
        else
        {
            stickA.transform.Rotate(0, 80 * Time.deltaTime, 0);
            stickB.transform.Rotate(0, -80 * Time.deltaTime, 0);
        }
    }
}
