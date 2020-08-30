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

        public float ForwardSpeed
        {
            get => forwardMovementSpeed;
            set => forwardMovementSpeed = value;
        }

        private void Awake()
        {
            _playerSpeed = GetComponent<SpeedCalculator>();
        }
        
        public void MoveForward(float val)
        {
            Move(val, forwardMovementSpeed);
        }

        public void MoveBackward(float val)
        {
            Move(val, backwardMovementSpeed);
        }
        
        private void Update()
        {
            Move(1, passiveMovementSpeed);
        }

        /// Translate Object in direction with speed
        private void Move(float val, float speedMultiplier)
        {
            transform.Translate(transform.forward * (val * (speedMultiplier * Time.deltaTime)), Space.World);
        }
    }
}