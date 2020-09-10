using UberPlanetary.Core;
using UnityEngine;

namespace UberPlanetary.Player.Movement
{
    public class PlayerRotationHandler : MonoBehaviour, IRotationHandler
    {
        private float _originalRotLossMultiplier;

        [SerializeField] private float xRotationSpeed = 80;
        [SerializeField] private float yRotationSpeed = 60;
        [SerializeField] private float zRotationSpeed = 100;
        [SerializeField] [Range(0,1)] private float rotationLossMultiplier = .5f;
        
        private void Awake()
        {
            _originalRotLossMultiplier = rotationLossMultiplier;
            rotationLossMultiplier = 1f;
        }
        
        public void DampenRotation(float val)
        {
            rotationLossMultiplier = Mathf.Lerp(1, _originalRotLossMultiplier, val);
        }

        /// Rotate object based on mouse cursor position and other inputs
        public void Rotate(Vector3 dir)
        {
            transform.Rotate(new Vector3(dir.x * xRotationSpeed,dir.y * yRotationSpeed,-dir.z * zRotationSpeed) * (rotationLossMultiplier * Time.deltaTime));
        }

    }
}