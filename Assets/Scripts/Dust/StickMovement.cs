using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class StickMovement : MonoBehaviour
{
    public bool isCameToFinish = false,isGameFinished = false,isGameStarted = false;
    public PathCreator pathScript;
    private float lastFrameFingerPositionX, moveFactorX,distanceTravelled;
    public float swerveSpeed = 50, maxSwerveAmount = 10,movementSpeed = 10;
    private GameObject stickObject;
    private Rigidbody stickRb;
    public static StickMovement movementScript;

    private void Awake()
    {
        movementScript = this;
    }
    void Start()
    {
        stickObject = transform.GetChild(0).gameObject;
        stickRb = stickObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        HandleInput();
        HandleMovement();
        FinishMove();
    }


    void HandleInput()
    {
        //Precise a point to start the swerve movement
        if (Input.GetMouseButtonDown(0))
        {
            lastFrameFingerPositionX = Input.mousePosition.x;
        }
        else if (Input.GetMouseButton(0))
        {
            moveFactorX = Input.mousePosition.x - lastFrameFingerPositionX;
            lastFrameFingerPositionX = Input.mousePosition.x;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            moveFactorX = 0f;
        }
    }

    void HandleMovement()
    {
        if (isCameToFinish || isGameFinished || !isGameStarted)
            return;

        //Movement part
        if(pathScript.path.length > distanceTravelled + 0.1f)
            distanceTravelled += Time.deltaTime * movementSpeed;
        transform.SetPositionAndRotation(pathScript.path.GetPointAtDistance(distanceTravelled) + new Vector3(0, 3f, 0),
            Quaternion.Euler(transform.rotation.eulerAngles.x,pathScript.path.GetRotationAtDistance(distanceTravelled).eulerAngles.y,transform.rotation.eulerAngles.z));

        //Swerve part
        float swerveAmount = Time.fixedDeltaTime * swerveSpeed * moveFactorX;
        swerveAmount = Mathf.Clamp(swerveAmount, -maxSwerveAmount, maxSwerveAmount);
        var smoothSwerveAmount = Mathf.Lerp(0, swerveAmount, 0.125f);
        if((smoothSwerveAmount < 0 && stickObject.transform.localPosition.x > -2) || smoothSwerveAmount > 0 && stickObject.transform.localPosition.x < 2)
            stickObject.transform.localPosition += new Vector3(smoothSwerveAmount, 0, 0);
    }

    private void FinishMove()
    {
        if(isCameToFinish && !isGameFinished)
            stickRb.velocity = movementSpeed * transform.forward;
    }

    public void StopTheStick()
    {
        isGameFinished = true;
        stickRb.velocity = Vector3.zero;
    }
}
