using System;
using UberPlanetary.CollisionHandling;

namespace UberPlanetary.Player
{
    /// <summary>
    /// Functions for what happens when player is hit
    /// </summary>
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