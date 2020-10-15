using UberPlanetary.Core.ExtensionMethods;
using UberPlanetary.Core.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace UberPlanetary.General
{
    /// Calculates the objects speed based on previous frame and exposes the values
    public class SpeedCalculator : MonoBehaviour , IEventValueProvider<float>
    {
        //private members
        private Vector3 _previousPosition;
        private Vector3 _currentPosition;
        private float _speed;
        private float _speed01;

        //Exposed fields
        [SerializeField] private float iMax;
        [SerializeField] private Text speedText;

        //public properties
        public float InMax => iMax;
        //Exposed value for current speed remapped to be 0 to 1.
        public float Speed01 => _speed01;
        public float Speed => _speed;
        public float GetValue => _speed01;
        
        private void Update()
        {
            speedText.text = _speed.ToString();
        }

        private void FixedUpdate()
        {
            CalculateSpeed();
        }

        /// Velocity calculated based on position delta over time
        private void CalculateSpeed()
        {
            _speed = Mathf.RoundToInt(Vector3.Distance(transform.position, _previousPosition) / Time.fixedDeltaTime);
            _speed01 = _speed.Remap(0, iMax, 0, 1);
            _previousPosition = transform.position;
        }
    }
}