using System.Collections.Generic;
using System.Linq;
using UberPlanetary.Core;
using UberPlanetary.Quests;
using UberPlanetary.ScriptableObjects;
using UnityEngine;
using Random = UnityEngine.Random;

namespace UberPlanetary.Navigation
{
    public class NavigationManager : MonoBehaviour
    {
        private static NavigationManager _instance;
        
        private List<ILandmark> _landmarks = new List<ILandmark>();
        private List<IGeneralLandmark> _generalLandmarks = new List<IGeneralLandmark>();

        private Sprite passengerPickUpSprite;
        private Sprite passengerDropOffSprite;
        private Sprite additionalQuestSprite;
        
        private RideManager _rideManager;

        public Dictionary<string, ILandmark> stringLandmarkDictionary = new Dictionary<string, ILandmark>();
        public Dictionary<int, ILandmark> intLandmarkDictionary = new Dictionary<int, ILandmark>();

        public static NavigationManager Instance
        {
            get => _instance;
            set => _instance = value;
        }

        public List<ILandmark> Landmarks
        {
            get => _landmarks;
            //set => _landmarks = value;
        }

        public List<IGeneralLandmark> GeneralLandmarks
        {
            get => _generalLandmarks;
            //set => _generalLandmarks = value;
        }

        private void Awake()
        {
            Instance = _instance ? _instance : this;
            _rideManager = FindObjectOfType<RideManager>();
        }

        private void Start()
        {
            _rideManager.onRideAccepted.AddListener(SetDestination);
            _rideManager.onCustomerPickedUp.AddListener(SetDestination);
            InitializeDictionaries();
        }

        private void InitializeDictionaries()
        {
            foreach (ILandmark lm in _landmarks)
            {
                if (!intLandmarkDictionary.ContainsKey(lm.LandmarkIntID) && lm.LandmarkIntID != 0)
                {
                    intLandmarkDictionary.Add(lm.LandmarkIntID, lm);
                }

                if (!stringLandmarkDictionary.ContainsKey(lm.LandmarkStringID) && !string.IsNullOrEmpty(lm.LandmarkStringID))
                {
                    stringLandmarkDictionary.Add(lm.LandmarkStringID, lm);
                }
            }

        }

        private void SetDestination(CustomerSO customerSo)
        {
            customerSo.CustomerRide.RideCurrentLandmark.LocationIcon.iconImage.enabled = true;
        }
        
        public ILandmark GetRandomLandmark()
        {
            var rand = Random.Range(0, _landmarks.Count);
            return _landmarks[rand];
        }

        public IGeneralLandmark GetRandomGeneralLandmark()
        {
            var rand = Random.Range(0, _landmarks.Count);
            return _generalLandmarks[rand];
        }
        public ILandmark GetFurthestLandmark(Vector3 from)
        {
            return _landmarks.OrderBy(x => (from - x.GetTransform.position).magnitude).Last();
        }        
        public ILandmark GetNearestLandmark(Vector3 from)
        {
            return _landmarks.OrderBy(x => (from - x.GetTransform.position).magnitude).First();
        }
        public IGeneralLandmark GetFurthestGeneralLandmark(Vector3 from)
        {
            return _generalLandmarks.OrderBy(x => (from - x.GetTransform.position).magnitude).Last();
        }        
        public IGeneralLandmark GetNearestGeneralLandmark(Vector3 from)
        {
            return _generalLandmarks.OrderBy(x => (from - x.GetTransform.position).magnitude).First();
        }

        public ILandmark GetRandomLandmarkWithinRadius(Vector3 from, float radius)
        {
            var temp = _landmarks.OrderBy(x => (from - x.GetTransform.position).magnitude < radius).ToList();
        
            var rand = Random.Range(0, temp.Count -1);
            
            return temp[rand];
        }
    }
}