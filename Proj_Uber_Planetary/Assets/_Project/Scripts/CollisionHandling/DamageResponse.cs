using System;
using UberPlanetary.Core;
using UnityEngine;
using UnityEngine.Events;

namespace UberPlanetary.CollisionHandling
{
    /// Inherit from this and write additional functions to be a part of damage response along with calling the TakeDamage method if applicable
    public class DamageResponse : MonoBehaviour, ITakeDamage
    {
        public UnityEvent onDamageTaken;
        
        public void TakeDamage()
        {
            onDamageTaken?.Invoke();
        }
    }
}
