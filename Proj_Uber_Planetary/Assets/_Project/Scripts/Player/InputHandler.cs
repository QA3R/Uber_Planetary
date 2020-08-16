using System;
using UberPlanetary.Core;
using UnityEngine;

namespace UberPlanetary.Player
{
    public class InputHandler : MonoBehaviour, IInputProvider
    {
        //Delegate Deceleration
        public event Action<Vector3> OnRotate;
        public event Action<float> OnMove;
        public event Action<Vector3> OnMousePosition;
        public event Action<float> OnBoost;
        
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
            OnRotate?.Invoke(new Vector3(
                Input.GetAxisRaw(xRotAxisName), 
                Input.GetAxisRaw(yRotAxisName), 
                Input.GetAxisRaw(zRotAxisName)
            ));

            OnMove?.Invoke(Input.GetAxis(forwardAxisName));
            OnBoost?.Invoke(Input.GetAxis(boostAxisName));

            OnMousePosition?.Invoke(Input.mousePosition);
        }
    }
}
