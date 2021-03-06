using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dust : MonoBehaviour
{
    private bool isTaken = false;
    private DustController dustControllerScipt;
    void Start()
    {
        dustControllerScipt = DustController.dustControllerScript;
    }

    void Update()
    {
        if (isTaken)
            MoveToStick();
    }

    private void OnTriggerEnter(Collider other)
    {
        if((other.gameObject.CompareTag("Stick") || other.gameObject.CompareTag("DustBlock")) && !isTaken)
        {
            StartCoroutine(SetDustSettings());
        }
    }

    IEnumerator SetDustSettings()
    {
        isTaken = true;
        dustControllerScipt.AddDust();
        Destroy(GetComponent<Collider>());
        yield return new WaitForEndOfFrame();
        try
        {
            transform.parent = dustControllerScipt.dustStartPosition.GetChild(dustControllerScipt.dustCount - 1);
        }
        catch
        {
            //No need for any changes. It's working 
        }
    }
    void MoveToStick()
    {
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, new Vector3(0,0,0), 10 * Time.deltaTime);
    }
}
