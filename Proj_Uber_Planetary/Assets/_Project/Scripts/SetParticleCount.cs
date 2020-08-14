using UberPlanetary.Core;
using UnityEngine;

namespace UberPlanetary
{
    public class SetParticleCount : MonoBehaviour
    {
        //Private members
        private ParticleSystem _particleSystem;
        private float _currentParticles;
        private float _currentStartSpeed;
    
        //Exposed fields in inspector
        [SerializeField] private float maxParticles;
        [SerializeField] private float minStartSpeed;
        [SerializeField] private float maxStartSpeed;
        [SerializeField] private AnimationCurve speedAnimationCurve;
        [SerializeField] private AnimationCurve particleAnimationCurve;

        private void Awake()
        {
            _particleSystem = GetComponent<ParticleSystem>();
        }

        /// <summary>
        /// Expects a value between 0-1 and remaps it to valid range for Particle system speed
        /// </summary>
        /// <param name="val"></param>
        public void SetSpeedAmount(float val)
        {
            _currentStartSpeed = speedAnimationCurve.Evaluate(val).Remap(0, 1, minStartSpeed, maxStartSpeed);
            var particleSystemMain = _particleSystem.main;
            particleSystemMain.startSpeed = _currentStartSpeed;
        }
        
        /// <summary>
        /// Expects a value between 0-1 and remaps it to valid range for Particle system emission
        /// </summary>
        /// <param name="val"></param>
        public void SetParticleAmount(float val)
        {
            _currentParticles = particleAnimationCurve.Evaluate(val).Remap(0, 1, 0, maxParticles);
            var particleSystemEmission = _particleSystem.emission;
            particleSystemEmission.rateOverTime = _currentParticles;
        }
    }
}
