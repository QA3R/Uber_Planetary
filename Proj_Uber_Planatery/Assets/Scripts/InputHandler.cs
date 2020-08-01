using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public delegate void RotationDelegate(Vector3 dir);

    public delegate void MovementDelegate(float val);
    public delegate void BoostDelegate();

    public RotationDelegate rotationDelegate;
    public MovementDelegate movementDelegate;
    public BoostDelegate boostDelegate;

    [SerializeField]private string xRotAxisName;
    [SerializeField]private string yRotAxisName;
    [SerializeField]private string zRotAxisName;
    
    private void Update()
    {
        rotationDelegate?.Invoke(new Vector3(Input.GetAxis(xRotAxisName), Input.GetAxis(yRotAxisName), Input.GetAxis(zRotAxisName)).normalized);

        movementDelegate?.Invoke(Input.GetAxis("Vertical"));

        if (Input.GetButton("Jump"))
        {
            boostDelegate?.Invoke();
        }
    }    

    private void FixedUpdate()
    {
    }
}
