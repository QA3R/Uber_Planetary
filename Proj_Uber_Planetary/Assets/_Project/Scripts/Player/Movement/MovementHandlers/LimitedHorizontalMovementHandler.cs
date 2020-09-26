using System;
using System.Collections;
using UberPlanetary.Core;
using UnityEngine;

namespace UberPlanetary.Player.Movement.MovementHandlers
{
    public class LimitedHorizontalMovementHandler : BaseMovementHandler
    {
        [SerializeField] private Vector2 heightMinMax;
        [SerializeField] private float speedSmoothingDuration;
        [SerializeField] private AnimationCurve smoothingCurve;
        private float _originalPassiveMovementSpeed;
        private bool _isRunning;
        private bool IsBacking => Mathf.Abs(_backVal) > 0;
        private float _backVal;


        private void Awake()
        {
            _originalPassiveMovementSpeed = passiveMovementSpeed;
        }

        public override void MoveVertical(float val)
        {
            Move(transform.up, val, verticalMovementSpeed);

            // base.MoveVertical(val);
            if (transform.localPosition.y <= heightMinMax.x)
            {
                transform.localPosition =
                    new Vector3(transform.localPosition.x, heightMinMax.x, transform.localPosition.z);
                return;
            }

            if (transform.localPosition.y >= heightMinMax.y)
            {
                transform.localPosition =
                    new Vector3(transform.localPosition.x, heightMinMax.y, transform.localPosition.z);
            }
        }

        public override void MoveForward(float val)
        {
            base.MoveForward(val);
            if (Mathf.Abs(val) > 0)
            {
                if(Math.Abs(passiveMovementSpeed - _originalPassiveMovementSpeed) < .01f || _isRunning || IsBacking) return;
                //passiveMovementSpeed = _originalPassiveMovementSpeed;
                SafeStop();
                StartCoroutine(LerpSpeed(passiveMovementSpeed, _originalPassiveMovementSpeed, speedSmoothingDuration));
            }
        }

        public override void MoveBackward(float val)
        {
            base.MoveBackward(val);
            _backVal = val;
            if (Mathf.Abs(val) > 0)
            {
                if(passiveMovementSpeed == 0 || _isRunning) return;
                //passiveMovementSpeed = 0;
                SafeStop();
                StartCoroutine(LerpSpeed(passiveMovementSpeed, 0, speedSmoothingDuration));
            }
        }

        private void SafeStop()
        {
            StopAllCoroutines();
            _isRunning = false;
        }

        private IEnumerator LerpSpeed(float from, float to, float duration)
        {
            _isRunning = true;
            float t = 0;
            while (t <= duration)
            {
                passiveMovementSpeed = Mathf.Lerp(from, to, smoothingCurve.Evaluate(t.Remap(0, duration, 0, 1)));
                t += Time.fixedDeltaTime;
                yield return new WaitForFixedUpdate();
            }

            _isRunning = false;
        }
    }
}