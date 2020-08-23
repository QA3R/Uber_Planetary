using System;
using UberPlanetary.Core;
using UberPlanetary.Player.Movement;
using UnityEngine;
using UnityEngine.Events;

namespace UberPlanetary.Player
{
    /// <summary>
    /// Finds the player and raises an event with its speed value for other classes
    /// </summary>
    public class GlobalPlayerSpeedEvent : MonoBehaviour, IEventExposer<float>
    {
        [SerializeField] private UnityEvent<float> onValueChange;
        public IEventValueProvider<float> EventValueProvider { get; set; }

        private float _previousValue;

        private void Start()
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
            EventValueProvider = FindObjectOfType<PlayerController>().GetComponent<IEventValueProvider<float>>();
        }

        public void InvokeEvent()
        {
            onValueChange?.Invoke(EventValueProvider.GetValue);
            _previousValue = EventValueProvider.GetValue;
        }
    }
}