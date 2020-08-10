using System;
using UberPlanetary.Core;
using UnityEngine;
using UnityEngine.Events;

namespace UberPlanetary.Player
{
    public class GlobalPlayerSpeedEvent : MonoBehaviour, IEventExposer<float>
    {
        [SerializeField] private UnityEvent<float> onValueChange;
        public IEventValueProvider<float> EventValueProvider { get; set; }

        private void Start()
        {
            SetReference();
        }

        private void Update()
        {
            InvokeEvent();
        }

        public void SetReference()
        {
            EventValueProvider = FindObjectOfType<PlayerController>().GetComponent<IEventValueProvider<float>>();
        }

        public void InvokeEvent()
        {
            onValueChange?.Invoke(EventValueProvider.GetValue());
        }

    }
}