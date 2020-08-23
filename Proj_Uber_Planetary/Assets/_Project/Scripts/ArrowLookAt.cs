using System;
using UberPlanetary.Core;
using UberPlanetary.Player.Movement;
using UnityEngine;

namespace UberPlanetary
{
    public class ArrowLookAt : MonoBehaviour
    {
        private IEventValueProvider<Vector3> _lookFrom;
        [SerializeField] private float threshold;

        private LookAtTarget lookAtTarget;

        public Vector3 Target
        {
            set => lookAtTarget.LookTo = value;
        }
        
        private void Awake()
        {
            lookAtTarget = new LookAtTarget {Threshold = threshold};
            _lookFrom = FindObjectOfType<PlayerController>().GetComponent<IEventValueProvider<Vector3>>();
        }

        private void Update()
        {
            lookAtTarget.LookFrom = _lookFrom.GetValue;
            transform.rotation = lookAtTarget.GetLookRotation();
        }
    }
}
