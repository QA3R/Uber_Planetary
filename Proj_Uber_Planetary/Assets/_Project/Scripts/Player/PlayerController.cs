using System.Diagnostics.CodeAnalysis;
using UberPlanetary.Core;
using UnityEngine;

namespace UberPlanetary.Player
{
    public class PlayerController : MonoBehaviour , IEventValueProvider<float>
    {
        //Private Members
        private InputHandler _inputHandler;
        private CursorController _cursorController;
        private float _originalRotLossMultiplier;
        private Vector3 _previousPosition;
        private Vector3 _currentPosition;
    
        //Exposed fields in inspector
        [SerializeField] private float xRotationSpeed;
        [SerializeField] private float yRotationSpeed;
        [SerializeField] private float zRotationSpeed;
        [SerializeField] [Range(0,1)] private float rotationLossMultiplier;
        [SerializeField] private float movementSpeed;
        [SerializeField] private float boostSpeed;
    
        //Exposed value for current speed remapped to be -1 to 1.
        public float ShipSpeed { get; private set; }
    
        private void Awake()
        {
            AssignComponents();
            AssignDelegates();
        
            _originalRotLossMultiplier = rotationLossMultiplier;
            rotationLossMultiplier = 1f;
        }
    
        /// <summary>
        /// Rotate object based on mouse cursor position and other inputs
        /// </summary>
        /// <param name="dir"></param>
        private void Rotate(Vector3 dir)
        {
            transform.Rotate(new Vector3(-_cursorController.CursorAxis.y * xRotationSpeed,
                _cursorController.CursorAxis.x * yRotationSpeed,
                -dir.z * zRotationSpeed) * (rotationLossMultiplier * Time.deltaTime));
        }

        /// <summary>
        /// Translate object forward
        /// </summary>
        /// <param name="val"></param>
        private void Move(float val)
        {
            transform.Translate(transform.forward * (val * (movementSpeed * Time.deltaTime)), Space.World);
        }

        /// <summary>
        /// Translate object forward and reduce rotation speed
        /// </summary>
        /// <param name="val"></param>
        private void Boost(float val)
        {
            if (val >= .1)
            {
                rotationLossMultiplier = _originalRotLossMultiplier;
            }
            else
            { 
                rotationLossMultiplier = 1f;
            }
            transform.Translate(transform.forward * (val * (boostSpeed * Time.deltaTime)), Space.World);
        }

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
            ShipSpeed = (_currentPosition - _previousPosition).magnitude / Time.deltaTime;
            ShipSpeed = ShipSpeed.Remap(0, boostSpeed + movementSpeed, 0, 1);
        }

        /// <summary>
        /// Get Component Reference from GameObject
        /// </summary>
        private void AssignComponents()
        {
            _cursorController = GetComponentInChildren<CursorController>();
            _inputHandler = GetComponent<InputHandler>();
        }
    
        /// <summary>
        /// Assign methods to respective delegates
        /// </summary>
        private void AssignDelegates()
        {
            _inputHandler.rotationDelegate += Rotate;
            _inputHandler.movementDelegate += Move;
            _inputHandler.boostDelegate += Boost;
        }

        /// <summary>
        /// UnAssign methods on Disable
        /// </summary>
        [SuppressMessage("ReSharper", "DelegateSubtraction")]
        private void OnDisable()
        {
            if (_inputHandler.rotationDelegate != null) _inputHandler.rotationDelegate -= Rotate;
            if (_inputHandler.movementDelegate != null) _inputHandler.movementDelegate -= Move;
            if (_inputHandler.boostDelegate != null) _inputHandler.boostDelegate -= Boost;
        }

        public float GetValue()
        {
            return ShipSpeed;
        }
    }
}
