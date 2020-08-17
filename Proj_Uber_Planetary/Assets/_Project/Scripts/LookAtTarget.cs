using System;
using UnityEngine;

namespace UberPlanetary
{
    public class LookAtTarget : MonoBehaviour
    {
        private Vector3 _target;

        public Vector3 Target
        {
            get => _target;
            set => _target = value;
        }

        private void Update()
        {
            Vector3 dir = _target - transform.position;
            transform.rotation = Quaternion.LookRotation(dir);
        }
    }
}
