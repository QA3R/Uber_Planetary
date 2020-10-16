using System;
using UberPlanetary.Core.Interfaces;
using UnityEngine;

namespace UberPlanetary.Navigation
{
    /// Implements the ILandmark interface and maintains its state
    public class Landmark : MonoBehaviour, ILandmark, IListElement
    {
        //serialized fields
        [SerializeField]private GameObject iconHolder;
        [SerializeField]private String landmarkStringId;
        [SerializeField]private int landmarkIntId;

        //events
        public event Action OnReached;

        //public properties
        public ILandmarkIcon LocationIcon { get; set; }
        public Transform GetTransform => transform;
        public string LandmarkStringID => landmarkStringId;
        public int LandmarkIntID => landmarkIntId;
        public IGeneralLandmark parentLandmark { get; private set; }
        
        private void Awake()
        {
            LocationIcon = iconHolder.GetComponent<ILandmarkIcon>();
            parentLandmark = GetComponentInParent<IGeneralLandmark>();
            Add();
        }
        public void Add()
        {
            NavigationManager.Instance.Landmarks.Add(this);
            //parentLandmark.landmarkGrouping.Add(this);
        }
        public void Remove()
        {
            NavigationManager.Instance.Landmarks.Remove(this);
            //parentLandmark.landmarkGrouping.Add(this);
        }
        public void OnLocationReached()
        {
            OnReached?.Invoke();
        }
    }
}