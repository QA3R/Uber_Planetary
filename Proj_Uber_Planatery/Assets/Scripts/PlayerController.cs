using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private InputHandler _inputHandler;

    public float rotationSpeed;
    public float movementSpeed;
    public float boostSpeed;
    
    private Rigidbody _rigidbody;

    private CursorController _cursorController;
    private void Awake()
    {
        _cursorController = GetComponentInChildren<CursorController>();
        _rigidbody = GetComponent<Rigidbody>();
        _inputHandler = GetComponent<InputHandler>();
        _inputHandler.rotationDelegate += Rotate;
        _inputHandler.movementDelegate += Move;
        _inputHandler.boostDelegate += Boost;
        //Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Rotate(Vector3 dir)
    {
        transform.Rotate(new Vector3(-_cursorController.CursorAxis.y,_cursorController.CursorAxis.x,-dir.z) * (rotationSpeed * Time.deltaTime));
    }

    private void Move(float val)
    {
        // _rigidbody.velocity = transform.forward * (val * (movementSpeed * Time.deltaTime));
        transform.Translate(transform.forward * (val * (movementSpeed * Time.deltaTime)), Space.World);
    }

    private void Boost()
    {
        transform.Translate(transform.forward * (boostSpeed * Time.deltaTime), Space.World);
    }
}
