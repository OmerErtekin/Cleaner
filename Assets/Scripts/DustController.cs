using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustController : MonoBehaviour
{
    [HideInInspector]public int dustCount = 0;
    public GameObject dustObject;
    public Transform dustStartPosition;
    public static DustController dustControllerScript;
    private Rigidbody stickRb;

    void Awake()
    {
        dustControllerScript = this;
    }
    void Start()
    {
        stickRb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            AddDust();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            RemoveDust();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            StartCoroutine(ElectricShock(3));
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            StartCoroutine(ScissorsCut(dustStartPosition.GetChild(3).gameObject));
        }
    }

    IEnumerator AddPyhsicalDust()
    {
        stickRb.AddForce(0, 100, 0);
        dustCount++;
        yield return new WaitForEndOfFrame();
        Instantiate(dustObject, dustStartPosition.position - new Vector3(0, 0.1f * dustCount, 0), transform.rotation,dustStartPosition);
    }

    void DropVisualPart()
    {
        if (dustStartPosition.childCount == 0)
            return;
        GameObject visualPart = dustStartPosition.GetChild(dustStartPosition.childCount - 1).GetChild(0).gameObject;
        Destroy(visualPart.GetComponent<Dust>());
        visualPart.transform.parent = null;
        visualPart.AddComponent<Rigidbody>().AddForce(0, 0, -200);
        visualPart.AddComponent<BoxCollider>();
        Destroy(visualPart, 5);
    }

    public void AddDust()
    {
        StartCoroutine(AddPyhsicalDust());
    }
    public void RemoveDust()
    {
        if (dustStartPosition.childCount != 0)
        {
            DropVisualPart();
            Destroy(dustStartPosition.GetChild(dustStartPosition.childCount-1).gameObject);
            dustCount--;
        }
        else
        {
            //Game over
        }
    }

    public IEnumerator ElectricShock(int count)
    {
        if(dustCount != 0)
        {
            int iterationCount;
            if (dustCount > count)
            {
                iterationCount = count;
            }
            else
            {
                iterationCount = dustCount;
            }
            for (int i = 0; i < iterationCount; i++)
            {
                yield return new WaitForSeconds(0.1f);
                RemoveDust();
            }
        }
        else
        {
            //GameOver
        }
    }

    public IEnumerator ScissorsCut(GameObject collidedBlock)
    {
        int iterationCount = dustStartPosition.childCount - collidedBlock.transform.GetSiblingIndex();
        if (iterationCount <= 0)
            yield break;
        for(int i = 0;i<iterationCount;i++)
        {
            yield return new WaitForEndOfFrame();
            RemoveDust();
        }
    }
}
