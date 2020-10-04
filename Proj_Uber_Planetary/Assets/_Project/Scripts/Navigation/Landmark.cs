using UberPlanetary.Core;
using UnityEngine;
using UnityEngine.Events;

namespace UberPlanetary.Navigation
{
    public class Landmark : MonoBehaviour, ILandmark
    {
        
        public ILandmarkIcon LocationIcon { get; set; }

        public UnityEvent OnReached { get; set; }
        public Vector3 GetPosition => transform.position;
        
        public void Add()
        {
            NavigationManager.Instance.Landmarks.Add(this);
        }

        public void Remove()
        {
            NavigationManager.Instance.Landmarks.Remove(this);
        }

        public void OnLocationReached()
        {
            OnReached?.Invoke();
        }
    }
}