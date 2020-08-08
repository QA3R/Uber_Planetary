using UnityEngine;

namespace UberPlanetary
{
    public class InputHandler : MonoBehaviour
    {
        //Delegate Signature  
        public delegate void RotationDelegate(Vector3 dir);
        public delegate void MousePositionDelegate(Vector3 dir);
        public delegate void MovementDelegate(float val);
        public delegate void BoostDelegate(float val);

        //Delegate Deceleration
        public RotationDelegate rotationDelegate;
        public MovementDelegate movementDelegate;
        public BoostDelegate boostDelegate;
        public MousePositionDelegate mousePositionDelegate;

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
            rotationDelegate?.Invoke(new Vector3(
                Input.GetAxisRaw(xRotAxisName), 
                Input.GetAxisRaw(yRotAxisName), 
                Input.GetAxisRaw(zRotAxisName)
            ));

            movementDelegate?.Invoke(Input.GetAxis(forwardAxisName));

            boostDelegate?.Invoke(Input.GetAxis(boostAxisName));

            mousePositionDelegate?.Invoke(Input.mousePosition);
        }    
    
    }
}
