using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Socket : MonoBehaviour
{
    private DustController dustScript;
    private GameManager managerScript;
    private bool isShocked = false;
    public GameObject shockEffect;
    void Start()
    {
        managerScript = GameManager.managerScript;
        dustScript = DustController.dustControllerScript;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("DustBlock") && !isShocked)
        {
            isShocked = true;
            Instantiate(shockEffect, other.gameObject.transform.position + new Vector3(0,1,0), transform.rotation);
            StartCoroutine(dustScript.ElectricShock(1));
        }
        else if(other.gameObject.CompareTag("Stick") && !isShocked)
        {
            isShocked = true;
            Instantiate(shockEffect, other.gameObject.transform.position, transform.rotation);
            managerScript.FinishTheGame();
        }
    }
}
