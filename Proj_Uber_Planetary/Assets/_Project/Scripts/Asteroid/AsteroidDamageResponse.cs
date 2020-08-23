using UberPlanetary.CollisionHandling;
using UnityEngine;

namespace UberPlanetary.Asteroid
{
    /// <summary>
    /// Encapsulates the event for when an Asteroid is Hit
    /// </summary>
    public class AsteroidDamageResponse : DamageResponse
    {
        [ContextMenu("Take Hit")]
        public void TestHit()
        {
            TakeDamage();
        }
    }
}