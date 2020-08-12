using UberPlanetary.Core;
using UnityEngine;
using UnityEngine.Events;

namespace UberPlanetary.CollisionHandling
{
    public class DamageResponse : MonoBehaviour, ITakeDamage
    {
        public UnityEvent onDamageTaken;
        
        public void TakeDamage()
        {
            onDamageTaken?.Invoke();
        }
    }
}