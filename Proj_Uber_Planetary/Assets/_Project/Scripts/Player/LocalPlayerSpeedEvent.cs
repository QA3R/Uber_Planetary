using System;
using UberPlanetary.Core;
using UberPlanetary.Core.Interfaces;
using UnityEngine;
using UnityEngine.Events;

namespace UberPlanetary.Player
{
    /// Finds the player locally and invokes an event based on its speed value.
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
            if (Math.Abs(_previousValue - EventValueProvider.GetValue) < .0001f) return;
            InvokeEvent();
        }

        public void SetReference()
        {
            EventValueProvider = GetComponent<IEventValueProvider<float>>();
        }

        public void InvokeEvent()
        {
                onValueChange?.Invoke(EventValueProvider.GetValue);
            _previousValue = EventValueProvider.GetValue;
        }

    }
}
