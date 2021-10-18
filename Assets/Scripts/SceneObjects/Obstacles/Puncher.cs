using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puncher : MonoBehaviour
{
    public int hitDirection = 0;
    private StickMovement movementScript;
    private DustController dustScript;
    private CameraShaker shaker;
    public int direction = 0;
    private bool isHit = false;
    private float lastChangeTime = 0;
    public GameObject armObject;
    void Start()
    {
        movementScript = StickMovement.movementScript;
        dustScript = DustController.dustControllerScript;
        shaker = CameraShaker.shaker;
    }

    void Update()
    {
        SetTimer();
        PunchMovement();
    }

    private void OnTriggerEnter(Collider other)
    {
        if((other.gameObject.CompareTag("Stick") || other.gameObject.CompareTag("DustBlock")) && !isHit && direction == 1)
        {
            isHit = true;
            shaker.CameraShake(5);
            if (hitDirection == 1)
                StartCoroutine(movementScript.TakePunch(1));
            else if (hitDirection == -1)
                StartCoroutine(movementScript.TakePunch(-1));
            StartCoroutine(dustScript.MultipleCut(2));
        }
    }

    void SetTimer()
    {
        transform.Rotate(90 * Time.deltaTime, 0, 0);
        if(Time.time > lastChangeTime)
        {
            if(direction == 0)
            {
                lastChangeTime = Time.time + 0.25f;
                direction = 1;
                return;
            }
            if(direction == 1)
            {
                lastChangeTime = Time.time + 0.25f;
                direction = -1;
                return;
            }
            if(direction == -1)
            {
                lastChangeTime = Time.time + 0.5f;
                direction = 0;
            }
        }
    }

    void PunchMovement()
    {
        if(direction == 1)
        {
            armObject.transform.localPosition += new Vector3(8 * Time.deltaTime, 0, 0);
        }
        if(direction == 0)
        {
            armObject.transform.localPosition += new Vector3(-4 * Time.deltaTime, 0, 0);
        }
    }
}
