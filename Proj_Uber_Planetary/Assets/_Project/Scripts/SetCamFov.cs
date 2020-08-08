using System;
using Cinemachine;
using UnityEngine;

namespace UberPlanetary
{
    public class SetCamFov : MonoBehaviour
    {
        private CinemachineVirtualCamera _cvCam;
        [SerializeField] private float min, max;
        private float _currentFov;

        private void Awake()
        {
            _cvCam = GetComponent<CinemachineVirtualCamera>();
        }

        public void SetFov(float val)
        {
            _currentFov = val.Remap(0, 1, max, min);
            _cvCam.m_Lens.FieldOfView = _currentFov;
        }
    }
}
