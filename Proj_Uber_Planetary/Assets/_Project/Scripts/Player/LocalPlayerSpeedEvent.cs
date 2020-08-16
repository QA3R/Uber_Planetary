using System;
using UberPlanetary.Core;
using UnityEngine;
using UnityEngine.Events;

namespace UberPlanetary.Player
{
    public class LocalPlayerSpeedEvent : MonoBehaviour, IEventExposer<float>
    {
        public UnityEvent<float> onValueChange;
        
        public IEventValueProvider<float> EventValueProvider { get; set; }

        private float _previousValue;

        private void Awake()
        {
            SetReference();
        }

        private void Update()
        {
            InvokeEvent();
        }

        public void SetReference()
        {
            EventValueProvider = GetComponent<IEventValueProvider<float>>();
        }

        public void InvokeEvent()
        {
            if (Math.Abs(_previousValue - EventValueProvider.GetValue) < .0001f) return;
                onValueChange?.Invoke(EventValueProvider.GetValue);
            _previousValue = EventValueProvider.GetValue;
        }

    }
}
