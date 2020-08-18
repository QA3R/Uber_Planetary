using System;
using UberPlanetary.Core;
using UberPlanetary.Player;
using UnityEngine;

namespace UberPlanetary
{
    public class LookAtTarget : MonoBehaviour
    {
        private Vector3 _target;
        private IEventValueProvider<Vector3> _playerPosition;
        [SerializeField] private float threshold;

        private void Awake()
        {
            _playerPosition = FindObjectOfType<PlayerController>().GetComponent<IEventValueProvider<Vector3>>();
        }

        public Vector3 Target
        {
            set => _target = value;
        }

        private void Update()
        {
            Vector3 dir = _target - _playerPosition.GetValue;
            if(dir.magnitude > threshold)
            transform.rotation = Quaternion.LookRotation(dir);
        }
    }
}
