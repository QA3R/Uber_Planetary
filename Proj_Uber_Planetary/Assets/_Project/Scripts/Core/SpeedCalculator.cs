using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

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
        private float _remappedSpeed;

        //Exposed fields
        [SerializeField] private float iMax;
        [SerializeField] private Text speedText;

        //public properties
        public float InMax => iMax;
        //Exposed value for current speed remapped to be 0 to 1.
        public float Speed => _remappedSpeed;
        public float GetValue => _remappedSpeed;

        private void Start()
        {
            StartCoroutine(CalculateVelocity());
        }

        private void Update()
        {
            //CalculateSpeed();
            // Debug.Log("Current Speed : " + _speed);
            speedText.text = _speed.ToString();
        }

        // /// <summary>
        // /// Speed calculated based on position delta over time
        // /// </summary>
        // private void CalculateSpeed()
        // {
        //     _previousPosition = _currentPosition;
        //     _currentPosition = transform.position;
        //     _speed = (_currentPosition - _previousPosition).magnitude / Time.deltaTime;
        //     _remappedSpeed = _speed.Remap(0, iMax, 0, 1);
        // }

        /// <summary>
        /// Velocity calculated based on position delta over time
        /// </summary>
        private IEnumerator CalculateVelocity()
        {
            while (Application.isPlaying)
            {
                _previousPosition = transform.position;
                yield return new WaitForEndOfFrame();
                _speed = Mathf.RoundToInt(Vector3.Distance(transform.position, _previousPosition) / Time.deltaTime);
                _remappedSpeed = _speed.Remap(0, iMax, 0, 1);
            }
        }
    }
}