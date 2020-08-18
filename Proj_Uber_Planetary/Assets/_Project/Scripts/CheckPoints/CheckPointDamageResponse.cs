using System;
using UberPlanetary.CollisionHandling;
using UnityEngine;

namespace UberPlanetary.CheckPoints
{
    public class CheckPointDamageResponse : DamageResponse
    {
        [SerializeField] private string damageableTag;
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(damageableTag))
            {
                onDamageTaken?.Invoke();
            }
        }
    }
}
