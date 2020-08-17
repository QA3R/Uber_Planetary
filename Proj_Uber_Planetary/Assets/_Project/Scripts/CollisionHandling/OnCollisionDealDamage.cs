using UberPlanetary.Core;
using UnityEngine;

namespace UberPlanetary.CollisionHandling
{
    /// <summary>
    /// Calls the TakeDamage function on colliding with a ITakeDamage of matching Tag.
    /// </summary>
    public class OnCollisionDealDamage : MonoBehaviour
    {
        [SerializeField] private string damageableTag;
        
        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag(damageableTag))
            {
                other.gameObject.GetComponent<ITakeDamage>().TakeDamage();
            }
        }
    }
}