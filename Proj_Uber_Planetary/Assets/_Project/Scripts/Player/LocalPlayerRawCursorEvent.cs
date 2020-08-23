using System;
using UberPlanetary.Core;
using UnityEngine;
using UnityEngine.Events;

namespace UberPlanetary.Player
{
    /// <summary>
    /// Finds the Cursor Axis value provider and invokes event
    /// </summary>
    public class LocalPlayerRawCursorEvent : MonoBehaviour, IEventExposer<Vector2>
    {
        public IEventValueProvider<Vector2> EventValueProvider { get; set; }
        
        [SerializeField] private UnityEvent<Vector2> onValueChange;

        private Vector2 _previousValue;
        private void Awake()
        {
            SetReference();
        }

        private void Update()
        {
            //if (_previousValue == EventValueProvider.GetValue) return;
            InvokeEvent();
        }

        public void SetReference()
        {
            EventValueProvider = GetComponent<IEventValueProvider<Vector2>>();
        }

        public void InvokeEvent()
        {
            onValueChange?.Invoke(EventValueProvider.GetValue);
            _previousValue = EventValueProvider.GetValue;
        }
    }
}
