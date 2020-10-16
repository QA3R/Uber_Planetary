using System;
using System.Collections.Generic;
using System.Linq;
using UberPlanetary.Core.Interfaces;
using UberPlanetary.Quests;
using UberPlanetary.ScriptableObjects;
using UnityEngine;
using Random = UnityEngine.Random;

namespace UberPlanetary.Navigation
{
    /// Maintains the list of Navigation points in the world and provides functions to generate new ones
    public class NavigationManager : MonoBehaviour
    {
        //Instance
        private static NavigationManager _instance;
        public static NavigationManager Instance => _instance;

        //private members
        private List<ILandmark> _landmarks = new List<ILandmark>();
        private List<IGeneralLandmark> _generalLandmarks = new List<IGeneralLandmark>();
        private RideManager _rideManager;
        private Dictionary<Type, List<ILandmark>> _typeDictionary = new Dictionary<Type, List<ILandmark>>();

        //public dictionaries
        public Dictionary<string, ILandmark> stringLandmarkDictionary = new Dictionary<string, ILandmark>();
        public Dictionary<int, ILandmark> intLandmarkDictionary = new Dictionary<int, ILandmark>();
        
        //public properties
        public List<ILandmark> Landmarks
        {
            get => _landmarks;
        }

        public List<IGeneralLandmark> GeneralLandmarks
        {
            get => _generalLandmarks;
        }

        private void Awake()
        {
            //_instance ??= this;

            _instance = _instance ? _instance : this;
            _rideManager = FindObjectOfType<RideManager>();
        }

        private void Start()
        {
            _rideManager.onRideAccepted.AddListener(SetDestination);
            _rideManager.onCustomerPickedUp.AddListener(SetDestination);
            InitializeDictionaries();
        }

        /// Update the dictionaries of landmarks based on the registered landmarks
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
            _typeDictionary.Add(typeof(ILandmark), _landmarks);
            _typeDictionary.Add(typeof(IGeneralLandmark), _landmarks);
        }

        //Enable the image and wait for player to reach that destination.
        private void SetDestination(CustomerSO customerSo)
        {
            var locationIcon = customerSo.CustomerRide.RideCurrentLandmark.LocationIcon;
            locationIcon.ToggleImage();
        }
        

        /// Returns a ILandmark randomly from the collection
        public T GetRandomLandmark<T>() where T : ILandmark
        {
            var listToUse = _typeDictionary[typeof(T)];
            var rand = Random.Range(0, listToUse.Count);
            return (T) listToUse[rand]; //_generalLandmarks[rand];
        }
        
        /// Returns the furthest ILandmark from the provided vector3
        public T GetFurthestLandmark<T>(Vector3 from) where T : ILandmark
        {
            var listToUse = _typeDictionary[typeof(T)];

            return (T) listToUse.OrderBy(x => (from - x.GetTransform.position).magnitude).Last();
        }        
        
        /// Returns the nearest ILandmark from the provided vector3
        public T GetNearestLandmark<T>(Vector3 from) where T : ILandmark
        {
            var listToUse = _typeDictionary[typeof(T)];
            
            return (T) listToUse.OrderBy(x => (from - x.GetTransform.position).magnitude).First();
        }

        /// Returns a random ILandmark from the provided vector3
        public T GetRandomLandmarkWithinRadius<T>(Vector3 from, float radius) where T : ILandmark
        {
            var listToUse = _typeDictionary[typeof(T)];

            var temp = listToUse.OrderBy(x => (from - x.GetTransform.position).magnitude < radius).ToList();
        
            var rand = Random.Range(0, temp.Count -1);
            
            return (T) temp[rand];
        }
    }
}