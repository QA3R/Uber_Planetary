using UnityEngine;

namespace UberPlanetary.Core
{
    /// <summary>
    /// Calculates the objects speed based on previous frame and translates it into a 0-1 range
    /// </summary>
    public class SpeedCalculator : MonoBehaviour , IEventValueProvider<float>
    {
        //private members
        private Vector3 _previousPosition;
        private Vector3 _currentPosition;
        private float _speed;

        //Exposed fields
        [SerializeField] private float iMax;

        //public properties
        public float InMax => iMax;
        //Exposed value for current speed remapped to be 0 to 1.
        public float Speed => _speed;
        public float GetValue => Speed;
        
        private void Update()
        {
            CalculateShipSpeed();
        }

        /// <summary>
        /// Speed calculated based on position delta over time
        /// </summary>
        private void CalculateShipSpeed()
        {
            _previousPosition = _currentPosition;
            _currentPosition = transform.position;
            _speed = (_currentPosition - _previousPosition).magnitude / Time.deltaTime;
            _speed = _speed.Remap(0, iMax, 0, 1);
        }
    }
}