using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustController : MonoBehaviour
{
    public GameObject dustObject;
    private Rigidbody stickRb;
    public Transform dustStartPosition;
    public int dustCount = 0;
    public static DustController dustControllerScript;

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
    }

    IEnumerator AddPyhsicalDust()
    {
        if(stickRb.velocity.y < 0.5f)
            stickRb.AddForce(0, 100, 0);
        dustCount++;
        yield return new WaitForEndOfFrame();
        Instantiate(dustObject, dustStartPosition.position - new Vector3(0, 0.1f * dustCount, 0), transform.rotation,dustStartPosition);
    }

    void DropVisualPart()
    {
        GameObject visualPart = dustStartPosition.GetChild(dustStartPosition.childCount - 1).GetChild(0).gameObject;
        Destroy(visualPart.GetComponent<Dust>());
        visualPart.transform.parent = null;
        visualPart.AddComponent<Rigidbody>();
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
}
