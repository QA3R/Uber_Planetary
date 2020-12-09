using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTargetScript : MonoBehaviour
{
    public float speed;

    void FixedUpdate()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
