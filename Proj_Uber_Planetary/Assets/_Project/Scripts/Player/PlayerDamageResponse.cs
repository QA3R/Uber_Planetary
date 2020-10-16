using System.Collections;
using UberPlanetary.CollisionHandling;
using UberPlanetary.Core.ExtensionMethods;
using UberPlanetary.Player.Movement;
using UnityEngine;

namespace UberPlanetary.Player
{
    /// Functions for what happens when player is hit
    public class PlayerDamageResponse : DamageResponse
    {
        private PlayerController playerController;
        
        private bool _isDecelerating, _isAccelerating;
        
        [SerializeField] private AnimationCurve decelerationCurve;
        [SerializeField] private float decelerationDuration;        
        [SerializeField] private AnimationCurve accelerationCurve;
        [SerializeField] private float accelerationDuration;
        [SerializeField][Range(0,1)] private float reduceSpeedTo;
        
        private void Awake()
        {
            playerController = GetComponent<PlayerController>();
        }
        
        [ContextMenu("ReduceSpeed")]
        public void ReduceSpeed()
        {
            //playerController.MovementLossMultiplier -= val;
            if(_isDecelerating) return;
            SafeStop();
            StartCoroutine(DeceleratePlayer(playerController.AxisModifier, reduceSpeedTo));
        }

        private void SafeStop()
        {
            StopAllCoroutines();
            _isDecelerating = false;
            _isAccelerating = false;
        }

        protected IEnumerator DeceleratePlayer(float from, float to)
        {
            _isDecelerating = true;
            float t = 0;
            while (t <= decelerationDuration)
            {
                playerController.AxisModifier = Mathf.Lerp(@from, to, decelerationCurve.Evaluate(t.Remap(0, decelerationDuration, 0, 1)));
                t += Time.fixedDeltaTime;
                yield return new WaitForFixedUpdate();
            }
            _isDecelerating = false;
            
            if(_isAccelerating) yield break;
            StartCoroutine(AcceleratePlayer(playerController.AxisModifier, 1));
        }

        protected IEnumerator AcceleratePlayer(float from, float to)
        {
            if(_isDecelerating) yield break;
            
            _isAccelerating = true;
            float t = 0;
            while (t <= accelerationDuration)
            {
                playerController.AxisModifier = Mathf.Lerp(@from, to, accelerationCurve.Evaluate(t.Remap(0, accelerationDuration, 0, 1)));
                t += Time.fixedDeltaTime;
                yield return new WaitForFixedUpdate();
            }
            _isAccelerating = false;
        }

    }
}