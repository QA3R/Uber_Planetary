using System.Diagnostics.CodeAnalysis;
using UberPlanetary.Core;
using UnityEngine;

namespace UberPlanetary.Player
{
    /// <summary>
    /// Delegates tasks to other classes according to input
    /// </summary>
    public class PlayerController : MonoBehaviour 
    {
        //Private Members
        private IInputProvider _inputHandler;
        private CursorController _cursorController;
        private IRotationHandler _rotationHandler;
        private IMovementHandler _movementHandler;
        private IBoostHandler _boostHandler;
        
        private void Awake()
        {
            AssignComponents();
            AssignDelegates();
        }
        
        private void Rotate(Vector3 dir)
        {
            _rotationHandler.Rotate(new Vector3(-_cursorController.CursorAxis.y,_cursorController.CursorAxis.x,dir.z));
        }
        
        private void Move(float val)
        {
            _movementHandler.Move(val);
        }
        
        private void OnBoost(float val)
        {
            if (val >= .1)
            {
                _rotationHandler.DampenRotation();
            }
            else
            { 
                _rotationHandler.ResetRotationMultiplier();
            }
            _boostHandler.Boost(val);
        }
        
        /// <summary>
        /// Get Component Reference from GameObject
        /// </summary>
        private void AssignComponents()
        {
            _cursorController = GetComponentInChildren<CursorController>();
            _inputHandler = GetComponent<InputHandler>();
            _boostHandler = GetComponent<IBoostHandler>();
            _rotationHandler = GetComponent<IRotationHandler>();
            _movementHandler = GetComponent<IMovementHandler>();
        }
    
        /// <summary>
        /// Assign methods to respective delegates
        /// </summary>
        private void AssignDelegates()
        {
            _inputHandler.OnRotate += Rotate;
            _inputHandler.OnMove += Move;
            _inputHandler.OnBoost += OnBoost;
        }

        /// <summary>
        /// UnAssign methods on Disable
        /// </summary>
        private void OnDisable()
        {
            _inputHandler.OnRotate -= Rotate;
            _inputHandler.OnMove -= Move;
            _inputHandler.OnBoost -= OnBoost;
        }
    }
}
