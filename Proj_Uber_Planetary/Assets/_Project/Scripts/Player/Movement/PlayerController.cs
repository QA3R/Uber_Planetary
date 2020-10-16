using System;
using UberPlanetary.Core;
using UberPlanetary.Core.Interfaces;
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
        [SerializeField] [Range(0,1)] private float axisModifier = 1;

        public float AxisModifier
        {
            get => axisModifier;
            set => axisModifier = Mathf.Clamp01(value);
        }

        private void Awake()
        {
            AssignComponents();
        }

        private void Start()
        { 
            AssignDelegates();
        }

        private void Rotate(Vector3 dir)
        {
            _rotationHandler.Rotate(new Vector3(-_cursorController.CursorAxis.y,_cursorController.CursorAxis.x,dir.z));
        }
        
        private void MoveForward(float val)
        {
            _movementHandler.MoveForward(val * axisModifier);
        }
        
        private void MoveBackward(float val)
        {
            _movementHandler.MoveBackward(val * axisModifier);
        }

        private void MoveVertical(float val)
        {
            _movementHandler.MoveVertical(val * axisModifier);
            //_movementHandler.MoveVertical(_cursorController.CursorAxis.y);
        }

        private void MoveSideways(float val)
        {
            _movementHandler.MoveSidewards(val * axisModifier);
        }
        
        private void OnBoost(float val)
        {
            _rotationHandler.DampenRotation(val);
            
            _boostHandler.Boost(val * axisModifier);
        }
        
        /// Get Component Reference from PlayerController
        private void AssignComponents()
        {
            _cursorController = FindObjectOfType<CursorController>();
            _inputHandler = GetComponent<IInputProvider>();
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
            _inputHandler.OnMoveVertical += MoveVertical;
            _inputHandler.OnMoveSideways += MoveSideways;
        }

        /// UnAssign methods on Disable
        private void OnDisable()
        {
            _inputHandler.OnRotate -= Rotate;
            _inputHandler.OnMoveForward -= MoveForward;
            _inputHandler.OnBoost -= OnBoost;
            _inputHandler.OnMoveBackward -= MoveBackward;
            _inputHandler.OnMoveVertical -= MoveVertical;
            _inputHandler.OnMoveSideways -= MoveSideways;

        }
    }
}
