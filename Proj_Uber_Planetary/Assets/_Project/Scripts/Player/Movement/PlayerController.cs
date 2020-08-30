using UberPlanetary.Core;
using UnityEngine;

namespace UberPlanetary.Player.Movement
{
    /// Delegates tasks to other classes according to input
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
        
        private void MoveForward(float val)
        {
            _movementHandler.MoveForward(val);
        }

        private void MoveBackward(float val)
        {
            _movementHandler.MoveBackward(val);
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
        
        /// Get Component Reference from GameObject
        private void AssignComponents()
        {
            _cursorController = FindObjectOfType<CursorController>();
            _inputHandler = GetComponent<InputHandler>();
            _boostHandler = GetComponent<IBoostHandler>();
            _rotationHandler = GetComponent<IRotationHandler>();
            _movementHandler = GetComponent<IMovementHandler>();
        }
    
        /// Assign methods to respective delegates
        private void AssignDelegates()
        {
            _inputHandler.OnRotate += Rotate;
            _inputHandler.OnMoveForward += MoveForward;
            _inputHandler.OnBoost += OnBoost;
            _inputHandler.OnMoveBackward += MoveBackward;
        }

        /// UnAssign methods on Disable
        private void OnDisable()
        {
            _inputHandler.OnRotate -= Rotate;
            _inputHandler.OnMoveForward -= MoveForward;
            _inputHandler.OnBoost -= OnBoost;
            _inputHandler.OnMoveBackward -= MoveBackward;
        }
    }
}
