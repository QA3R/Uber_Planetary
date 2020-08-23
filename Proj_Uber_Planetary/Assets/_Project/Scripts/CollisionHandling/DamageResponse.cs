using UberPlanetary.Core;
using UnityEngine;
using UnityEngine.Events;

namespace UberPlanetary.CollisionHandling
{
    /// <summary>
    /// Inherit from this and write additional functions to be a part of damage response 
    /// </summary>
    public class DamageResponse : MonoBehaviour, ITakeDamage
    {
        public UnityEvent onDamageTaken;
        
        public void TakeDamage()
        {
            onDamageTaken?.Invoke();
        }
    }
}