using System;
using UberPlanetary.CollisionHandling;
using UberPlanetary.Player.Movement;

namespace UberPlanetary.Player
{
    /// Functions for what happens when player is hit
    public class PlayerDamageResponse : DamageResponse
    {
        private PlayerController playerController;
        private void Awake()
        {
            playerController = GetComponent<PlayerController>();
        }

        public void ReduceSpeed(float val)
        {
            //playerController.MovementLossMultiplier -= val;
        }
    }
}