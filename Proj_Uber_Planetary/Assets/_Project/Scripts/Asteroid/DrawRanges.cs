using System;
using UnityEngine;

namespace UberPlanetary.Asteroid
{
    public class DrawRanges : MonoBehaviour
    {
        [SerializeField] private float mainRadius;
        [SerializeField] private float disperseRadius;
        [SerializeField] private Color innerColor;
        [SerializeField] private Color outerColor;

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = innerColor;
            Gizmos.DrawWireSphere(transform.position, mainRadius);
            Gizmos.color = outerColor;
            Gizmos.DrawWireSphere(transform.position, mainRadius + disperseRadius);
        }
    }
}
