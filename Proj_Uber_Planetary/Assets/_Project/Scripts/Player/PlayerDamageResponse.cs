using System;
using UberPlanetary.CollisionHandling;
using UberPlanetary.Player.Movement;

namespace UberPlanetary.Player
{
    /// Functions for what happens when player is hit
    public class PlayerDamageResponse : DamageResponse
    {
        private GameObject gameObject;
        private void Awake()
        {
            gameObject = GetComponent<GameObject>();
        }

        public void ReduceSpeed(float val)
        {
            //gameObject.MovementLossMultiplier -= val;
        }
    }
}