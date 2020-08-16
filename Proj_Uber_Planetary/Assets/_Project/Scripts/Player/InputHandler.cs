using System;
using UberPlanetary.Core;
using UnityEngine;

namespace UberPlanetary.Player
{
    public class InputHandler : MonoBehaviour, IInputProvider
    {
        //Delegate Deceleration
        // public RotationDelegate rotationDelegate;
        // public MovementDelegate movementDelegate;
        // public BoostDelegate boostDelegate;
        // public MousePositionDelegate mousePositionDelegate;
        public event Action<Vector3> RotationDelegate;
        public event Action<float> MovementDelegate;
        public event Action<Vector3> MousePositionDelegate;
        public event Action<float> BoostDelegate;
        
        //Exposed input names
        [SerializeField] private string xRotAxisName;
        [SerializeField] private string yRotAxisName;
        [SerializeField] private string zRotAxisName;
        [SerializeField] private string boostAxisName;
        [SerializeField] private string forwardAxisName;
        
        /// <summary>
        /// Capture Input and invoke respective Delegates
        /// </summary>
        private void Update()
        {
            RotationDelegate?.Invoke(new Vector3(
                Input.GetAxisRaw(xRotAxisName), 
                Input.GetAxisRaw(yRotAxisName), 
                Input.GetAxisRaw(zRotAxisName)
            ));

            MovementDelegate?.Invoke(Input.GetAxis(forwardAxisName));
            BoostDelegate?.Invoke(Input.GetAxis(boostAxisName));

            MousePositionDelegate?.Invoke(Input.mousePosition);
        }



    }
}
