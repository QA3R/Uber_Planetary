using UberPlanetary.CollisionHandling;
using UnityEngine;

namespace UberPlanetary.Asteroid
{
    public class AsteroidDamageResponse : DamageResponse
    {
        [ContextMenu("Take Hit")]
        public void TestHit()
        {
            TakeDamage();
        }
    }
}