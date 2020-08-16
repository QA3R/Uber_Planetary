using UberPlanetary.Core;
using UnityEngine;

namespace UberPlanetary.Player
{
    public class PlayerBoostHandler : MonoBehaviour, IBoostHandler
    {
        [SerializeField] private float boostSpeed = 180;
        
        /// <summary>
        /// Translate object forward and reduce rotation speed
        /// </summary>
        /// <param name="val"></param>
        public void Boost(float val)
        {
            transform.Translate(transform.forward * (val * (boostSpeed * Time.deltaTime)), Space.World);
        }
    }
}