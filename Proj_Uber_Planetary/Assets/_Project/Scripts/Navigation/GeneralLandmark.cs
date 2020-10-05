using System.Collections.Generic;
using UberPlanetary.Core;
using UnityEngine;
using UnityEngine.Events;

namespace UberPlanetary.Navigation
{
    public class GeneralLandmark : MonoBehaviour, IGeneralLandmark
    {
        [SerializeField]private GameObject iconHolder;
        
        private List<ILandmark> _landmarkGrouping = new List<ILandmark>();
        
        public ILandmarkIcon LocationIcon { get; set; }

        public UnityEvent OnReached { get; set; }
        public Transform GetTransform => transform;
        public IGeneralLandmark parentLandmark { get; }
        public List<ILandmark> landmarkGrouping { get; set; }
        
        
        private void Awake()
        {
            LocationIcon = iconHolder.GetComponent<ILandmarkIcon>();

            PopulateChildLandmarks();
        }

        private void PopulateChildLandmarks()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).GetComponent<ILandmark>() != null)
                {
                    landmarkGrouping.Add(transform.GetChild(i).GetComponent<ILandmark>());
                }
            }
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
    }
}