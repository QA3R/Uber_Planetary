using System;
using UberPlanetary.CollisionHandling;
using UberPlanetary.Player.Movement;

namespace UberPlanetary.Player
{
    /// Functions for what happens when player is hit
    public class PlayerDamageResponse : DamageResponse
    {
        private PlayerController _playerController;
        private void Awake()
        {
            _playerController = GetComponent<PlayerController>();
        }

        public void ReduceSpeed(float val)
        {
            //_playerController.MovementLossMultiplier -= val;
        }
    }
}