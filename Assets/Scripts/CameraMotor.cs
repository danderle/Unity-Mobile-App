using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraMotor : MonoBehaviour
{
    public Transform LookAt;

    private Vector3 mDesiredPosition;
    private Vector3 Offset;

    private Vector3 TouchPosition;
    private float SwipeResistance = 200.0f;

    private float SmoothSpeed = 7.5f;
    private float mDistance = 6.0f;
    private float mY_Offset = 1.5f;
    
    /// <summary>
    /// Start this instance.
    /// </summary>
    private void Start()
    {
        Offset = new Vector3(0, mY_Offset, -1f * mDistance);
    }

    /// <summary>
    /// Update this instance.
    /// </summary>
    private void Update()
    {
        //keyboard inputs
        if(Input.GetKeyDown(KeyCode.LeftArrow))
            SlideCamera(true);
        else if(Input.GetKeyDown(KeyCode.RightArrow))
            SlideCamera(false);

        //Mouse input
        if(Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            TouchPosition = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1))
        {
            float swipeForce = TouchPosition.x - Input.mousePosition.x;

            if (Mathf.Abs(swipeForce) > SwipeResistance)
            {

                if (swipeForce < 0)
                    SlideCamera(true);
                else
                    SlideCamera(false);
            }

        }
    }

    private void FixedUpdate()
    {
        mDesiredPosition = LookAt.position + Offset;
        transform.position = Vector3.Lerp(transform.position, mDesiredPosition, SmoothSpeed * Time.deltaTime);
        transform.LookAt(LookAt.position + Vector3.up * mY_Offset);
    }

    /// <summary>
    /// Slides the camera.
    /// </summary>
    /// <param name="left">If set to <c>true</c> left.</param>
    public void SlideCamera(bool left)
    {
        if(left)
            Offset = Quaternion.Euler(0, 90, 0) * Offset;
        else
            Offset = Quaternion.Euler(0, -90, 0) * Offset;
    }
}