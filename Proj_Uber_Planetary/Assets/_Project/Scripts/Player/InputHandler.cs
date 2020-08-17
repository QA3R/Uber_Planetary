using System;
using UberPlanetary.Core;
using UnityEngine;

namespace UberPlanetary.Player
{
    /// <summary>
    /// Implements the input interface and invokes events based on proper axis's names
    /// </summary>
    public class InputHandler : MonoBehaviour, IInputProvider
    {
        //Delegate Deceleration
        public event Action<Vector3> OnRotate;
        public event Action<float> OnMoveForward;
        public event Action<float> OnMoveBackward;
        public event Action<Vector3> OnMousePosition;
        public event Action<float> OnBoost;
        
        //Exposed input names
        [SerializeField] private string xRotAxisName;
        [SerializeField] private string yRotAxisName;
        [SerializeField] private string zRotAxisName;
        [SerializeField] private string boostAxisName;
        [SerializeField] private string forwardAxisName;
        [SerializeField] private string backwardAxisName;
        
        /// <summary>
        /// Capture Input and invoke respective Delegates
        /// </summary>
        private void Update()
        {
            OnRotate?.Invoke(new Vector3(
                Input.GetAxisRaw(xRotAxisName), 
                Input.GetAxisRaw(yRotAxisName), 
                Input.GetAxisRaw(zRotAxisName)
            ));

            OnMoveForward?.Invoke(Input.GetAxis(forwardAxisName));
            OnMoveBackward?.Invoke(Input.GetAxis(backwardAxisName));
            
            OnBoost?.Invoke(Input.GetAxis(boostAxisName));

            OnMousePosition?.Invoke(Input.mousePosition);
        }
    }
}
