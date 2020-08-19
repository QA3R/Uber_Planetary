using System;
using UberPlanetary.Core;
using UnityEngine;

namespace UberPlanetary
{
    public class ChangeTrailWidth : MonoBehaviour
    {
        private TrailRenderer _trail;
        [SerializeField] private float minStarWidth, maxStartWidth, minEndWidth, maxEndWidth;
        [SerializeField] private AnimationCurve curve;

        private void Awake()
        {
            _trail = GetComponent<TrailRenderer>();
        }

        public void SetWidth(float val)
        {
            _trail.startWidth = curve.Evaluate(val.Remap(0, 1, minStarWidth, maxStartWidth));
            _trail.endWidth = curve.Evaluate(val.Remap(0, 1, minEndWidth, maxEndWidth));
        }
    }
}
