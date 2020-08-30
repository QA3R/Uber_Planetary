using UberPlanetary.CollisionHandling;
using UnityEngine;

namespace UberPlanetary.Asteroid
{
    /// Encapsulates the event for when an Asteroid is Hit, Call TakeDamage where appropriate.
    public class AsteroidDamageResponse : DamageResponse
    {
        [ContextMenu("Take Hit")]
        public void TestHit()
        {
            TakeDamage();
        }
    }
}