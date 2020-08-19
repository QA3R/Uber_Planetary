using System;
using UberPlanetary.Core;
using UnityEngine;

namespace UberPlanetary
{
    public class ChangeTrailWidth : MonoBehaviour
    {
        private TrailRenderer _trail;
        [SerializeField] private float minStarWidth, maxStartWidth, minEndWidth, maxEndWidth;

        private void Awake()
        {
            _trail = GetComponent<TrailRenderer>();
        }

        public void SetWidth(float val)
        {
            _trail.startWidth = val.Remap(0, 1, minStarWidth, maxStartWidth);
            _trail.endWidth = val.Remap(0, 1, minEndWidth, maxEndWidth);
        }
    }
}
