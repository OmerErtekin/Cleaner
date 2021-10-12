using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickMovement : MonoBehaviour
{
    private float lastFrameFingerPositionX, moveFactorX;
    public float swerveSpeed = 50, maxSwerveAmount = 10,movementSpeed = 10;
    private Rigidbody stickRb;
    void Start()
    {
        stickRb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        HandleInput();
        HandleMovement();
    }


    void HandleInput()
    {
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
        //Movement part
        stickRb.velocity = new Vector3(stickRb.velocity.x, stickRb.velocity.y, movementSpeed);
        //Swerve part
        float swerveAmount = Time.fixedDeltaTime * swerveSpeed * moveFactorX;
        swerveAmount = Mathf.Clamp(swerveAmount, -maxSwerveAmount, maxSwerveAmount);
        var smoothSwerveAmount = Mathf.Lerp(0, swerveAmount, 0.125f);
        transform.Translate(smoothSwerveAmount, 0, 0);
    }
}
