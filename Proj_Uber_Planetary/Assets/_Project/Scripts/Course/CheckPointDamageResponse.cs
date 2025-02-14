using UberPlanetary.CollisionHandling;
using UnityEngine;

namespace UberPlanetary.Course
{
    /// Inherits DamageResponse and calls its damage function on trigger enter
    public class CheckPointDamageResponse : DamageResponse
    {
        [SerializeField] private string damageableTag;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(damageableTag))
            {
                TakeDamage();
            }
        }
    }
}
