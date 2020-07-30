using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private InputHandler _inputHandler;

    public float rotationSpeed;
    public float movementSpeed;

    private Rigidbody _rigidbody;
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _inputHandler = GetComponent<InputHandler>();
        _inputHandler.rotationDelegate += Rotate;
        _inputHandler.movementDelegate += Move;
    }

    private void Rotate(Vector3 dir)
    {
        transform.Rotate(dir * (rotationSpeed * Time.deltaTime));
    }

    private void Move()
    {
        _rigidbody.velocity = transform.forward * (movementSpeed * Time.deltaTime);
        // transform.Translate(transform.forward * (movementSpeed * Time.deltaTime), Space.World);
    }
}
