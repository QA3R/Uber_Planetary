using System;
using UnityEngine;

namespace UberPlanetary
{
    public class LockRotationInAxis : MonoBehaviour
    {
        [SerializeField][Range(0,1)] private int xAxis,yAxis,zAxis;
        private void Update()
        {
            var rotation = transform.localRotation;
            rotation = Quaternion.Euler(rotation.eulerAngles.x * xAxis,rotation.eulerAngles.y * yAxis,rotation.eulerAngles.z * zAxis);
            transform.localRotation = rotation;
        }
    }
}
