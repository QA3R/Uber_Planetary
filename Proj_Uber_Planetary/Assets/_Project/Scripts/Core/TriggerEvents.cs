using System;
using UnityEngine;
using UnityEngine.Events;

namespace UberPlanetary.Core
{
    public class TriggerEvents : MonoBehaviour
    {
        [SerializeField] private UnityEvent onTriggerEnter, onTriggerExit, onTriggerStay;

        [SerializeField] private string[] triggerTags;

        private void OnTriggerEnter(Collider other)
        {
            for (int i = 0; i < triggerTags.Length; i++)
            {
                if (other.gameObject.CompareTag(triggerTags[i]))
                {
                    onTriggerEnter?.Invoke();
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            for (int i = 0; i < triggerTags.Length; i++)
            {
                if (other.gameObject.CompareTag(triggerTags[i]))
                {
                    onTriggerExit?.Invoke();
                }
            }
        }

        private void OnTriggerStay(Collider other)
        {
            for (int i = 0; i < triggerTags.Length; i++)
            {
                if (other.gameObject.CompareTag(triggerTags[i]))
                {
                    onTriggerStay?.Invoke();
                }
            }
        }
    }
}
