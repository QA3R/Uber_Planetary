using UberPlanetary.Core;
using UberPlanetary.Core.Interfaces;
using UnityEngine;

namespace UberPlanetary.Player.Movement.BoostHandlers
{
    public class BaseBoostHandler : MonoBehaviour, IBoostHandler
    {
        [SerializeField] private float boostSpeed = 180;
        
        /// Translate object forward and reduce rotation speed
        public void Boost(float val)
        {
            transform.Translate(transform.forward * (val * (boostSpeed * Time.deltaTime)), Space.World);
        }
    }
}