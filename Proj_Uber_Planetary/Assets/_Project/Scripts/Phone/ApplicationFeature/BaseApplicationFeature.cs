using UberPlanetary.Core.Interfaces;
using UnityEngine;
using UnityEngine.Events;

namespace UberPlanetary.Phone.ApplicationFeature
{
    public class BaseApplicationFeature : MonoBehaviour , IPhoneApplicationFeature
    {
        //Events exposed to Unity so we can drag and drop things, for eg, on select we might drag in a audio source and play a selection sound etc.
        [SerializeField] protected UnityEvent OnEnter, OnExit, OnSelect, OnDeselect;
        public IPhoneNavigable ParentNavigable { get; set; }

        public void Enter()
        {
            //Debug.Log("Entering :" + gameObject.name);
            OnEnter?.Invoke();
        }

        public void Exit()
        {
            //Debug.Log("Exiting :" + gameObject.name);
            OnExit?.Invoke();
        }

        public void Select()
        {
            //Debug.Log("<color=blue>HIGHLIGHTED </color>" + gameObject.name);
            OnSelect?.Invoke();
        }

        public void Deselect()
        {
            //Debug.Log("<color=red>UNHIGHLIGHTED </color>" + gameObject.name);
            OnDeselect?.Invoke();
        }

    }
}
