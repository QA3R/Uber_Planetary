using Cinemachine;
using UberPlanetary.Core;
using UberPlanetary.Core.ExtensionMethods;
using UnityEngine;

namespace UberPlanetary.Player
{
    /// Controls the Cinemachine camera's properties and provides functions to modify them
    public class CinemachineController : MonoBehaviour
    {
        //Private members
        private CinemachineVirtualCamera _cvCam;
        private CinemachineBasicMultiChannelPerlin _cvNoise;
        private float _currentFov;
        private float _currentShakeFrequency;
        
        //Exposed fields
        [SerializeField] private float fovMin, fovMax, shakeMax;
        [SerializeField] private AnimationCurve shakeCurve;

        private void Awake()
        {
            _cvCam = GetComponent<CinemachineVirtualCamera>();
            //Note: Using GetCinemachineComponent here
            _cvNoise = _cvCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        }

        /// Expects a value between 0 to 1, and remaps it to camera's FOV min max.
        public void SetFov(float val)
        {
            _currentFov = val.Remap(0, 1, fovMax, fovMin);
            _cvCam.m_Lens.FieldOfView = _currentFov;
        }

        /// Expects a value between 0 to 1 and remaps it to the camera shake frequency based on the animation curve
        public void SetShakeFrequency(float val)
        {
            _currentShakeFrequency = val.Remap(0, 1, 0, shakeMax );
            _cvNoise.m_FrequencyGain = _currentShakeFrequency * shakeCurve.Evaluate(val);
        }
    }
}
