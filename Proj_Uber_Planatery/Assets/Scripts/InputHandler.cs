using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public delegate void RotationDelegate(Vector3 dir);

    public delegate void MovementDelegate();

    public RotationDelegate rotationDelegate;
    public MovementDelegate movementDelegate;
    
    private void Update()
    {
        rotationDelegate?.Invoke(new Vector3(Input.GetAxis("Pitch"), Input.GetAxis("Yaw"), Input.GetAxis("Roll")));
        
    }

    private void FixedUpdate()
    {
        if (Input.GetButton("Jump"))
        {
            movementDelegate?.Invoke();
        }
    }
}
