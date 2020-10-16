using System;
using System.Collections;
using UberPlanetary.Core;
using UberPlanetary.Core.ExtensionMethods;
using UberPlanetary.Core.Interfaces;
using UnityEngine;

namespace UberPlanetary.Player.Movement.MovementHandlers
{
    public class BaseMovementHandler : MonoBehaviour, IMovementHandler
    {
        [SerializeField] protected float forwardMovementSpeed = 80;
        [SerializeField] protected float backwardMovementSpeed = 80;
        [SerializeField] protected float sideMovementSpeed = 80;
        [SerializeField] protected float verticalMovementSpeed = 80;
        [SerializeField] protected float passiveMovementSpeed = 20;
        [SerializeField] protected AnimationCurve smoothingCurve;
        protected bool _isRunning;

        protected Rigidbody _rigidbody;
        
        public float MovementSpeed
        {
            get => forwardMovementSpeed;
            set => forwardMovementSpeed = value;
        }


        protected virtual void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }
        
        public virtual void MoveForward(float val)
        {
            Move(transform.forward, val, forwardMovementSpeed);
        }
        public virtual void MoveBackward(float val)
        {
            Move(transform.forward, val, backwardMovementSpeed);
        }
        public virtual void MoveSidewards(float val)
        {
            Move(transform.right, val, sideMovementSpeed);
        }

        public virtual void MoveVertical(float val)
        {
            Move(transform.up, val, verticalMovementSpeed);
        }
        
        protected virtual void Update()
        {
        }

        private void FixedUpdate()
        {
            Move(transform.forward,1, passiveMovementSpeed);
        }

        /// Translate Object in direction with speed
        protected void Move(Vector3 dir, float val, float speedMultiplier)
        {

            Vector3 smoothedDelta = Vector3.MoveTowards(transform.position, _rigidbody.position + (dir * val), Time.fixedDeltaTime * speedMultiplier);

            Vector3 direction = (smoothedDelta - transform.position);

            Ray ray = new Ray(transform.position, direction);
            RaycastHit hit;
            if (!Physics.Raycast(ray, out hit, direction.magnitude * 2f))
            {
                //_rigidbody.MovePosition(smoothedDelta);
                _rigidbody.MovePosition(_rigidbody.position + dir * (val * Time.fixedDeltaTime * speedMultiplier));
            }
            else
            {
                _rigidbody.MovePosition(hit.point);
            }
        }

        protected IEnumerator LerpPassiveSpeed(float from, float to, float duration)
        {
            _isRunning = true;
            float t = 0;
            while (t <= duration)
            {
                passiveMovementSpeed = Mathf.Lerp(@from, to, smoothingCurve.Evaluate(t.Remap(0, duration, 0, 1)));
                t += Time.fixedDeltaTime;
                yield return new WaitForFixedUpdate();
            }

            _isRunning = false;
        }

        protected void SafeStop()
        {
            StopAllCoroutines();
            _isRunning = false;
        }
    }
}