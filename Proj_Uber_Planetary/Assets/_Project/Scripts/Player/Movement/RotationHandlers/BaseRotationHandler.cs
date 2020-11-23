using UberPlanetary.Core;
using UberPlanetary.Core.Interfaces;
using UnityEngine;

namespace UberPlanetary.Player.Movement.RotationHandlers
{
    public class BaseRotationHandler : MonoBehaviour, IRotationHandler
    {
        protected float _originalRotLossMultiplier;
        protected Rigidbody _rigidbody;


        [SerializeField] protected float xRotationSpeed = 80;
        [SerializeField] protected float yRotationSpeed = 60;
        [SerializeField] protected float zRotationSpeed = 100;
        [SerializeField] [Range(0,1)] protected float rotationLossMultiplier = .5f;


        protected virtual void Awake()
        {
            _originalRotLossMultiplier = rotationLossMultiplier;
            rotationLossMultiplier = 1f;
            _rigidbody = gameObject.GetComponent<Rigidbody>();
        }
        
        public virtual void DampenRotation(float val)
        {
            rotationLossMultiplier = Mathf.Lerp(1, _originalRotLossMultiplier, val);
        }

        /// Rotate object based on mouse cursor position and other inputs
        public virtual void Rotate(Vector3 dir)
        {
            Quaternion deltaRot = Quaternion.Euler(new Vector3(dir.x * xRotationSpeed, dir.y * yRotationSpeed, -dir.z * zRotationSpeed) * (rotationLossMultiplier * Time.deltaTime));
            _rigidbody.MoveRotation(transform.localRotation * deltaRot);
            //transform.Rotate(new Vector3(dir.x * xRotationSpeed,dir.y * yRotationSpeed,-dir.z * zRotationSpeed) * (rotationLossMultiplier * Time.deltaTime));
        }
    }
}