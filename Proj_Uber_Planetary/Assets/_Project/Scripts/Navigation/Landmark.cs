using System;
using UberPlanetary.Core;
using UnityEngine;
using UnityEngine.Events;

namespace UberPlanetary.Navigation
{
    public class Landmark : MonoBehaviour, ILandmark, IListElement
    {
        [SerializeField]private GameObject iconHolder;

        public ILandmarkIcon LocationIcon { get; set; }
        public event Action OnReached;

        public Transform GetTransform => transform;
        public IGeneralLandmark parentLandmark { get; private set; }
        
        private void Awake()
        {
            LocationIcon = iconHolder.GetComponent<ILandmarkIcon>();
            parentLandmark = GetComponentInParent<IGeneralLandmark>();
        }

        private void Start()
        {
            Add();
        }

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