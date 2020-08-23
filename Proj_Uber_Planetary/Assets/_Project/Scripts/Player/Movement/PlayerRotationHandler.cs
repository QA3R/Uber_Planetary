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

        public void DampenRotation()
        {
            rotationLossMultiplier = _originalRotLossMultiplier;
        }

        public void ResetRotationMultiplier()
        {
            rotationLossMultiplier = 1f;
        }

        private void Awake()
        {
            _originalRotLossMultiplier = rotationLossMultiplier;
            rotationLossMultiplier = 1f;
        }

        /// <summary>
        /// Rotate object based on mouse cursor position and other inputs
        /// </summary>
        /// <param name="dir"></param>
        public void Rotate(Vector3 dir)
        {
            transform.Rotate(new Vector3(dir.x * xRotationSpeed,dir.y * yRotationSpeed,-dir.z * zRotationSpeed) * (rotationLossMultiplier * Time.deltaTime));
        }
    }
}