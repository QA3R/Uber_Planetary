using System;
using UberPlanetary.Core;
using UnityEngine;

namespace UberPlanetary.Navigation
{
    public class Landmark : MonoBehaviour, ILandmark, IListElement
    {
        [SerializeField]private GameObject iconHolder;
        [SerializeField]private String landmarkStringId;
        [SerializeField]private int landmarkIntId;

        public ILandmarkIcon LocationIcon { get; set; }
        public event Action OnReached;

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