using UberPlanetary.Core;
using UnityEngine;
using UnityEngine.Events;

namespace UberPlanetary.Player
{
    public class LocalPlayerCursorEvent : MonoBehaviour, IEventExposer<Vector2>
    {
        public IEventValueProvider<Vector2> EventValueProvider { get; set; }
        
        [SerializeField] private UnityEvent<Vector2> onValueChange;


        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public void SetReference()
        {
            EventValueProvider = GetComponent<IEventValueProvider<Vector2>>();
        }

        public void InvokeEvent()
        {
            onValueChange?.Invoke(EventValueProvider.GetValue);
        }

    }
}
