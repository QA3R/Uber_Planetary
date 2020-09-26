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
        private float originalPassiveMovementSpeed;


        private void Awake()
        {
            originalPassiveMovementSpeed = passiveMovementSpeed;
        }

        public override void MoveVertical(float val)
        {
            Move(transform.up, val, verticalMovementSpeed);

            // base.MoveVertical(val);
            if (transform.localPosition.y <= heightMinMax.x)
            {
                transform.localPosition = new Vector3(transform.localPosition.x, heightMinMax.x, transform.localPosition.z);
                return;
            }

            if (transform.localPosition.y >= heightMinMax.y)
            {
                transform.localPosition = new Vector3(transform.localPosition.x, heightMinMax.y, transform.localPosition.z);
            }
        }

        public override void MoveForward(float val)
        {
            base.MoveForward(val);
            if (Mathf.Abs(val) > 0)
            {
                //passiveMovementSpeed = originalPassiveMovementSpeed;
                StopAllCoroutines();
                StartCoroutine(LerpSpeed(passiveMovementSpeed,originalPassiveMovementSpeed, speedSmoothingDuration));
            }
        }

        public override void MoveBackward(float val)
        {
            base.MoveBackward(val);
            if (Mathf.Abs(val) > 0)
            {
                //passiveMovementSpeed = 0;
                StopAllCoroutines();
                StartCoroutine(LerpSpeed(passiveMovementSpeed,0, speedSmoothingDuration));

            }
        }

        private IEnumerator LerpSpeed(float from, float to, float duration)
        {
            float t = 0;
            while (t <= duration)
            {
                passiveMovementSpeed = Mathf.Lerp(from, to, smoothingCurve.Evaluate(t.Remap(0, duration, 0, 1)));
                t += Time.fixedDeltaTime;
                yield return new WaitForFixedUpdate();
            }
        }
    }
}