using UberPlanetary.Core;
using UberPlanetary.Core.Interfaces;
using UnityEngine;

namespace UberPlanetary.CollisionHandling
{
    /// Calls the TakeDamage function on colliding with a ITakeDamage of matching Tag.
    public class OnCollisionDealDamage : MonoBehaviour
    {
        [SerializeField] private string[] damageableTags;
        
        private void OnCollisionEnter(Collision other)
        {
            for (int i = 0; i < damageableTags.Length; i++)
            {
                if (other.gameObject.CompareTag(damageableTags[i]))
                {
                    other.gameObject.GetComponent<ITakeDamage>().TakeDamage();
                    break;
                }
            }
        }
    }
}