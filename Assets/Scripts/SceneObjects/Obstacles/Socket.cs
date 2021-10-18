using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Socket : MonoBehaviour
{
    private DustController dustScript;
    private bool isShocked = false;
    public GameObject shockEffect;
    private CameraShaker shaker;
    void Start()
    {
        shaker = CameraShaker.shaker;
        dustScript = DustController.dustControllerScript;
    }

    private void OnTriggerEnter(Collider other)
    {
        if((other.gameObject.CompareTag("DustBlock") || other.gameObject.CompareTag("Stick")) && !isShocked)
        {
            shaker.CameraShake(5);
            isShocked = true;
            Instantiate(shockEffect, other.gameObject.transform.position + new Vector3(0,1,0), transform.rotation);
            StartCoroutine(dustScript.MultipleCut(1));
        }
    }
}
