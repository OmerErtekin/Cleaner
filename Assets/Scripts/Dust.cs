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
        if(other.gameObject.CompareTag("Stick") && !isTaken)
        {
            StartCoroutine(SetDustSettings());
        }
    }

    IEnumerator SetDustSettings()
    {
        isTaken = true;
        dustControllerScipt.AddDust();
        yield return new WaitForEndOfFrame();
        transform.rotation = Quaternion.Euler(0, dustControllerScipt.dustCount * 30, 0);
        transform.parent = dustControllerScipt.dustStartPosition.GetChild(dustControllerScipt.dustCount-1);
    }
    void MoveToStick()
    {
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, new Vector3(0,0,0), 10 * Time.deltaTime);
    }
}
