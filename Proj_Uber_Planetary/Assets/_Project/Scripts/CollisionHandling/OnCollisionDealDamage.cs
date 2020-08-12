using UberPlanetary.Core;
using UnityEngine;

namespace UberPlanetary.CollisionHandling
{
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