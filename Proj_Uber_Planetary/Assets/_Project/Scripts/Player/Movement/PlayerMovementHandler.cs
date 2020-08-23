using UberPlanetary.Core;
using UnityEngine;

namespace UberPlanetary.Player.Movement
{
    public class PlayerMovementHandler : MonoBehaviour, IMovementHandler
    {
        private SpeedCalculator _playerSpeed;
        
        [SerializeField] private float forwardMovementSpeed = 80;
        [SerializeField] private float backwardMovementSpeed = 80;
        [SerializeField] private float passiveMovementSpeed = 20;


        private void Awake()
        {
            _playerSpeed = GetComponent<SpeedCalculator>();
        }
        
        /// <summary>
        /// Translate object forward
        /// </summary>
        /// <param name="val"></param>
        public void MoveForward(float val)
        {
            transform.Translate(transform.forward * (val * (forwardMovementSpeed * Time.deltaTime)), Space.World);
        }

        public void MoveBackward(float val)
        {
            transform.Translate(transform.forward * (val * (backwardMovementSpeed * Time.deltaTime)), Space.World);
        }

        private void Update()
        {
            transform.Translate(transform.forward * (passiveMovementSpeed * Time.deltaTime), Space.World);
        }
    }
}