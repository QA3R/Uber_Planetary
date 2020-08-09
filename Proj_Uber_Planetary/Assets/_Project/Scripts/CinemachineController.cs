using Cinemachine;
using UnityEngine;

namespace UberPlanetary
{
    public class CinemachineController : MonoBehaviour
    {
        private CinemachineVirtualCamera _cvCam;
        private CinemachineBasicMultiChannelPerlin _cvNoise;
        [SerializeField] private float fovMin, fovMax, shakeMax;
        private float _currentFov;
        private float _currentShakeFrequency;

        private void Awake()
        {
            _cvCam = GetComponent<CinemachineVirtualCamera>();
            _cvNoise = _cvCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        }

        public void SetFov(float val)
        {
            _currentFov = val.Remap(0, 1, fovMax, fovMin);
            _cvCam.m_Lens.FieldOfView = _currentFov;

        }

        public void SetShakeFrequency(float val)
        {
            _currentShakeFrequency = val.Remap(0, 1, 0, shakeMax);
            _cvNoise.m_FrequencyGain = _currentShakeFrequency;
        }
    }
}
