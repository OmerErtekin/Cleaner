using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guillotine : MonoBehaviour
{
    private DustController dustScript;
    private bool isCut = false,isGoingDown = false;
    private float lastChangeTime = 0;
    public GameObject bladeObject;
    void Start()
    {
        dustScript = DustController.dustControllerScript;
    }

    void Update()
    {
        GoUpsideDown();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("DustBlock") && !isCut)
        {
            isCut = true;
            StartCoroutine(dustScript.MultipleCut(3));
        }
        if(other.gameObject.CompareTag("Stick"))
        {
            Debug.Log("salla");
        }
    }

    void GoUpsideDown()
    {
        if(Time.time > lastChangeTime)
        {
            lastChangeTime = Time.time + 1;
            isGoingDown = !isGoingDown;
        }

        if(isGoingDown)
        {
            bladeObject.transform.localPosition-= new Vector3(0, 5f * Time.deltaTime, 0);
        }
        else
        {
            bladeObject.transform.localPosition += new Vector3(0, 5f * Time.deltaTime, 0);
        }
    }
}
