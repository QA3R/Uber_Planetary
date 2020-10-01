using System;
using System.Collections;
using UberPlanetary.Core;
using UnityEngine;

namespace UberPlanetary.Player.Movement
{
    public class GearController : MonoBehaviour
    {
        private IInputProvider _inputProvider;
        private IMovementHandler _movementHandler;
        private int gearIndex = 1;
        private bool _isRunning;
        
        [SerializeField] private int gearCount;
        [SerializeField] private AnimationCurve smoothingCurve;
        [SerializeField] private float speedIncrement;
        [SerializeField] private float smoothingDuration;

        private void Awake()
        {
            _inputProvider = GetComponent<IInputProvider>();

            _movementHandler = GetComponent<IMovementHandler>();
        }

        private void Start()
        {
            _inputProvider.ClickInfo[KeyCode.Mouse0].OnDown += GearUp;
            _inputProvider.ClickInfo[KeyCode.Mouse1].OnDown += GearDown;
        }

        private void GearUp()
        {
            if(gearIndex >= gearCount || _isRunning) return;
            StartCoroutine(LerpPassiveSpeed(_movementHandler.MovementSpeed,
                _movementHandler.MovementSpeed + speedIncrement, smoothingDuration));
            //_movementHandler.MovementSpeed += speedIncrement;
            gearIndex++;
        }

        private void GearDown()
        {
            if(gearIndex <= 1 || _isRunning) return;
            StartCoroutine(LerpPassiveSpeed(_movementHandler.MovementSpeed,
                _movementHandler.MovementSpeed - speedIncrement, smoothingDuration));
            //_movementHandler.MovementSpeed -= speedIncrement;
            gearIndex--;
        }
        
        protected IEnumerator LerpPassiveSpeed(float from, float to, float duration)
        {
            _isRunning = true;
            float t = 0;
            while (t <= duration)
            {
                _movementHandler.MovementSpeed = Mathf.Lerp(@from, to, smoothingCurve.Evaluate(t.Remap(0, duration, 0, 1)));
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
