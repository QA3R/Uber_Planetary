using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public delegate void RotationDelegate(Vector3 dir);
    public delegate void MousePositionDelegate(Vector3 dir);

    public delegate void MovementDelegate(float val);
    public delegate void BoostDelegate(bool isHeld, float val);

    public RotationDelegate rotationDelegate;
    public MovementDelegate movementDelegate;
    public BoostDelegate boostDelegate;
    public MousePositionDelegate mousePositionDelegate;

    [SerializeField]private string xRotAxisName;
    [SerializeField]private string yRotAxisName;
    [SerializeField]private string zRotAxisName;
    [SerializeField]private string boostAxisName;
    
    
    private void Update()
    {
        rotationDelegate?.Invoke(new Vector3(
            Input.GetAxisRaw(xRotAxisName), 
            Input.GetAxisRaw(yRotAxisName), 
            Input.GetAxisRaw(zRotAxisName)
            ));

        movementDelegate?.Invoke(Input.GetAxis("Vertical"));

        if (Input.GetButton(boostAxisName))
        {
            boostDelegate?.Invoke(true, Input.GetAxis(boostAxisName));
        }
        else
        {
            boostDelegate?.Invoke(false,Input.GetAxis(boostAxisName));
        }
        // if (Input.GetButtonUp(boostAxisName))
        // {
        //     boostDelegate?.Invoke(false,Input.GetAxis(boostAxisName));
        // }
        mousePositionDelegate?.Invoke(Input.mousePosition);
    }    
    
}
