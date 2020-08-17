using System;
using UnityEngine;

namespace UberPlanetary.Asteroid
{
    /// <summary>
    /// Draws Gizmos to help visualize ranges for Asteroid's components
    /// </summary>
    public class DrawRanges : MonoBehaviour
    {
        [SerializeField] private float mainRadius;
        [SerializeField] private float disperseRadius;
        [SerializeField] private Color innerColor;
        [SerializeField] private Color outerColor;

        private void OnDrawGizmosSelected()
        {
            var position = transform.position;
            
            Gizmos.color = innerColor;
            Gizmos.DrawWireSphere(position, mainRadius);
            Gizmos.color = outerColor;
            Gizmos.DrawWireSphere(position, mainRadius + disperseRadius);
        }
    }
}
