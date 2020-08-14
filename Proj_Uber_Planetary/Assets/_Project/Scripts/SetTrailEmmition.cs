using System;
using UnityEngine;

namespace UberPlanetary
{
    public class SetTrailEmmition : MonoBehaviour
    {

        private TrailRenderer _trail;

        private void Awake()
        {
            _trail = GetComponent<TrailRenderer>();
        }

        public void SetEmitting(float val)
        {
            _trail.emitting = val > 0;
        }
    }
}
