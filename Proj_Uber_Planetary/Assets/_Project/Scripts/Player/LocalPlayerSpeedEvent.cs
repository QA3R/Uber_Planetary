using UberPlanetary.Core;
using UnityEngine;
using UnityEngine.Events;

namespace UberPlanetary.Player
{
    public class LocalPlayerSpeedEvent : MonoBehaviour, IEventExposer<float>
    {
        public UnityEvent<float> onValueChange;
        
        public IEventValueProvider<float> EventValueProvider { get; set; }

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
            onValueChange?.Invoke(EventValueProvider.GetValue);
        }

    }
}
