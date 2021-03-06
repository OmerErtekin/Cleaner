using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class StickMovement : MonoBehaviour
{
    public bool isCameToFinish = false, isGameFinished = false, isGameStarted = false, isCollidingWithPhone = false;
    public PathCreator pathScript;
    private float lastFrameFingerPositionX, moveFactorX, distanceTravelled,punchDirection = 0;
    private bool isPunchTaken = false;
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
        DecreaseSpeed();
        CatchTheParent();
        PunchMovement();
        if(Input.GetKeyDown(KeyCode.P))
        {
            StartCoroutine(TakePunch(1));
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            StartCoroutine(TakePunch(-1));
        }
    }

    public IEnumerator TakePunch(int direction)
    {
        if (isPunchTaken)
            yield break;

        punchDirection = direction;
        isPunchTaken = true;
        yield return new WaitForSeconds(0.25f);
        isPunchTaken = false;
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
        if (pathScript.path.length > distanceTravelled)
        {
            distanceTravelled += Time.deltaTime * movementSpeed;
            transform.SetPositionAndRotation(pathScript.path.GetPointAtDistance(distanceTravelled) + new Vector3(0, 1f, 0),
                Quaternion.Euler(transform.rotation.eulerAngles.x, pathScript.path.GetRotationAtDistance(distanceTravelled).eulerAngles.y, transform.rotation.eulerAngles.z));
        }

        //Swerve part
        float swerveAmount = Time.fixedDeltaTime * swerveSpeed * moveFactorX;
        swerveAmount = Mathf.Clamp(swerveAmount, -maxSwerveAmount, maxSwerveAmount);
        var smoothSwerveAmount = Mathf.Lerp(0, swerveAmount, 0.125f);
        if((smoothSwerveAmount < 0 && stickObject.transform.localPosition.x > -1.5f) || smoothSwerveAmount > 0 && stickObject.transform.localPosition.x < 1.5f)
            stickObject.transform.localPosition += new Vector3(smoothSwerveAmount, 0, 0);
    }

    private void FinishMove()
    {
        //For simulate the gravity, vector (0,-1,0) added
        if (isCameToFinish && !isGameFinished)
            stickRb.velocity = (movementSpeed * transform.forward) + new Vector3(0, -1, 0);
    }

    private void CatchTheParent()
    {
        // If the position difference between parent and stick is too high, stick is accerelating a little and catch the parent
        if (isGameFinished || isCameToFinish || isCollidingWithPhone)
            return; 
        if(stickObject.transform.localPosition.z < 0)
        {
            stickObject.transform.localPosition += new Vector3(0, 0, 1f) * Time.deltaTime;
        }

    }


    void DecreaseSpeed()
    {
        //Changing speed at finish for smoother experience
        if(isGameFinished)
        {
            movementSpeed = Mathf.Lerp(movementSpeed, 0, 3 * Time.deltaTime);
            stickRb.velocity = (movementSpeed * transform.forward);
        }
    }

    void PunchMovement()
    {
        if (!isPunchTaken)
            return;

        if((punchDirection == 1 && stickObject.transform.localPosition.x < 1.5f) || (punchDirection == -1 && stickObject.transform.localPosition.x > -1.5f))
        {
            stickObject.transform.localPosition += new Vector3(25 * punchDirection * Time.deltaTime,0,0);
        }
    }


}
