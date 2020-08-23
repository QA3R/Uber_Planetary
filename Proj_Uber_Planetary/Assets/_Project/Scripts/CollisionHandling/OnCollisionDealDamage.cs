using UberPlanetary.Core;
using UnityEngine;

namespace UberPlanetary.CollisionHandling
{
    /// <summary>
    /// Calls the TakeDamage function on colliding with a ITakeDamage of matching Tag.
    /// </summary>
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