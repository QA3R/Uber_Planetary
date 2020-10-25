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
        private float _originalPassiveMovementSpeed;
        private bool IsBacking => Mathf.Abs(_backVal) > 0;
        private float _backVal;

        public Vector2 HeightMinMax
        {
            get => heightMinMax;
            set => heightMinMax = value;
        }


        protected override void Awake()
        {
            base.Awake();
            _originalPassiveMovementSpeed = passiveMovementSpeed;
        }

        /*
        public override void MoveVertical(float val)
        {
            Move(transform.up, val, verticalMovementSpeed);
            //_movement = new Vector3(_movement.x, _movement.y +(val * verticalMovementSpeed), _movement.z);

            base.MoveVertical(val);
            if (transform.localPosition.y <= heightMinMax.x)
            {
                transform.localPosition =
                    new Vector3(transform.localPosition.x, heightMinMax.x, transform.localPosition.z);
            }
            else if (transform.localPosition.y >= heightMinMax.y)
            {
                transform.localPosition =
                    new Vector3(transform.localPosition.x, heightMinMax.y, transform.localPosition.z);
            }
        }
        */
        public override void MoveForward(float val)
        {
            base.MoveForward(val);
            if (Mathf.Abs(val) > 0)
            {
                if(Math.Abs(passiveMovementSpeed - _originalPassiveMovementSpeed) < .01f || _isRunning || IsBacking) return;
                //passiveMovementSpeed = _originalPassiveMovementSpeed;
                SafeStop();
                StartCoroutine(LerpPassiveSpeed(passiveMovementSpeed, _originalPassiveMovementSpeed, speedSmoothingDuration));
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
                StartCoroutine(LerpPassiveSpeed(passiveMovementSpeed, 0, speedSmoothingDuration));
            }
        }
    }
}