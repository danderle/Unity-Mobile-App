using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Motor : MonoBehaviour 
{
    #region Public Members

    public float MoveSpeed = 5.0f;

    public float Drag = 0.5f;

    public float TerminalRotationSpeed = 25.0f;

    public VirtualJoystick MoveJoystick;

    public float BoostSpeed = 20.0f;

    public float BoostCooldown = 2.0f;

    #endregion

    #region Private Members

    private float mLastBoost;

    private Rigidbody Controller;

    private Transform mCamTransform;

    #endregion

    private void Start()
    {
        mLastBoost = Time.time - BoostCooldown;

        Controller = GetComponent<Rigidbody>();
        Controller.maxAngularVelocity = TerminalRotationSpeed;
        Controller.drag = Drag;

        mCamTransform = Camera.main.transform;
    }


    private void Update()
    {
        Vector3 direction = Vector3.zero;

        direction.x = Input.GetAxis("Horizontal");
        direction.z = Input.GetAxis("Vertical");

        if (direction.magnitude > 1)
            direction.Normalize();

        if (MoveJoystick.InputDirection != Vector3.zero)
            direction = MoveJoystick.InputDirection;

        //Rotate our diection vector with camera
        Vector3 rotatedDir = mCamTransform.TransformDirection(direction);
        rotatedDir = new Vector3(rotatedDir.x, 0, rotatedDir.z);
        rotatedDir = rotatedDir.normalized * direction.magnitude;

        Controller.AddForce(rotatedDir * MoveSpeed);
    }

    public void Boost()
    {
        if(Time.time - mLastBoost > BoostCooldown)
        {
            mLastBoost = Time.time;
            Controller.AddForce(Controller.velocity.normalized * BoostSpeed, ForceMode.VelocityChange);
        }

    }
}
