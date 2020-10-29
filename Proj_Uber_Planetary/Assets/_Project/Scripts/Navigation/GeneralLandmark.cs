using System;
using System.Collections.Generic;
using UberPlanetary.Core.Interfaces;
using UnityEngine;
using UnityEngine.Events;

namespace UberPlanetary.Navigation
{
    /// Implements the IGeneralLandmark interface and maintains its state
    public class GeneralLandmark : MonoBehaviour, IGeneralLandmark, IListElement
    {
        //private members
        private List<ILandmark> _landmarkGrouping = new List<ILandmark>();

        //serialized fields
        [SerializeField]private GameObject iconHolder;
        [SerializeField]private string stringID;
        [SerializeField]private int intID;
        [SerializeField] private UnityEvent activationEvent;

        //events
        public event Action OnReached;
        public UnityEvent ActivationEvent => activationEvent;

        //public properties
        public ILandmarkIcon LocationIcon { get; set; }
        public Transform GetTransform => transform;
        public string LandmarkStringID => stringID;
        public int LandmarkIntID => intID;
        public IGeneralLandmark parentLandmark { get; }
        public List<ILandmark> landmarkGrouping { get; set; }
        
        
        private void Awake()
        {
            LocationIcon = iconHolder.GetComponent<ILandmarkIcon>();
            
            Add();
        }

        public void Add()
        {
            NavigationManager.Instance.GeneralLandmarks.Add(this);
        }

        public void Remove()
        {
            NavigationManager.Instance.GeneralLandmarks.Remove(this);
        }

        public void OnLocationReached()
        {
            OnReached?.Invoke();
        }

        public void ActivateLandmark()
        {
            activationEvent?.Invoke();
        }
    }
}