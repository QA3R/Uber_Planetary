using System;
using UberPlanetary.Core;
using UberPlanetary.Core.ExtensionMethods;
using UnityEngine;

namespace UberPlanetary
{
    public class AdjustAudioPitch : MonoBehaviour
    {
        private AudioSource _audioSource;
        
        private float CurrentPitch
        {
            set
            {
                _audioSource.pitch = value;
            }
        }
        
        
        [SerializeField] private float minPitch, maxPitch;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        public void AdjustPitch(float val)
        {
            CurrentPitch = val.Remap(0, 1, minPitch, maxPitch);
        }
    }
}
