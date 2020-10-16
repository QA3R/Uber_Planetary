using System.Collections.Generic;
using UberPlanetary.Core.Interfaces;
using UberPlanetary.Navigation;
using UberPlanetary.ScriptableObjects;
using UnityEngine;
using UnityEngine.Events;

namespace UberPlanetary.Rides
{
    /// Details of any given ride, start location, end location, the rewards, etc.
    [System.Serializable]
    public class Ride
    {
        //exposed fields
        public int CashReward =>rideRewards.cashReward;

        [Tooltip("Optional, leave empty and the quest manager will find a random location for it")]
        [SerializeField] private string endLocationStringID, startLocationStringID;
        [Tooltip("Optional, leave empty and the quest manager will find a random location for it")]
        [SerializeField] private int endLocationIntID, startLocationIntID;


        [Space(10)]
        
        [Header("Rewards")]
        public RideRewards rideRewards;
        public UnityEvent<CustomerSO> onRideSuccessful = new UnityEvent<CustomerSO>();


        //private memebers
        private ILandmark _rideStartLandmark;
        private ILandmark _rideEndLandmark;
        private ILandmark _rideCurrentLandmark;

        //public properties
        public ILandmark RideCurrentLandmark
        {
            get => _rideCurrentLandmark;
            set => _rideCurrentLandmark = value;
        }

        public int RideCashReward => CashReward;

        public ILandmark RideStartLandmark
        {
            get
            {
                if (_rideStartLandmark == null) //if location is not assigned

                {
                    if (!string.IsNullOrEmpty(startLocationStringID)) //Error handling
                    {
                        var boop = NavigationManager.Instance.stringLandmarkDictionary[startLocationStringID];
                        _rideStartLandmark = boop ?? _rideStartLandmark; //assignment
                    }
                    else if (startLocationIntID != 0) //Error handling
                    {
                        var boop = NavigationManager.Instance.intLandmarkDictionary[startLocationIntID];
                        _rideStartLandmark = boop ?? _rideStartLandmark; //assignment
                    }
                }
                return _rideStartLandmark;
            }
            set => _rideStartLandmark = value;
        }
        
        public ILandmark RideEndLandmark
        {
            get
            {
                if (_rideEndLandmark == null) //if location is not assigned
                {
                    if (!string.IsNullOrEmpty(endLocationStringID)) //Error handling
                    {
                        var boop = NavigationManager.Instance.stringLandmarkDictionary[endLocationStringID];
                        _rideEndLandmark = boop ?? _rideEndLandmark; //assignment
                    }
                    else if (endLocationIntID != 0) //Error handling
                    {
                        var boop = NavigationManager.Instance.intLandmarkDictionary[endLocationIntID];
                        _rideEndLandmark = boop ?? _rideEndLandmark; //assignment
                    }
                }
                return _rideEndLandmark;
            }
            set => _rideEndLandmark = value;
        }
        
    }

    [System.Serializable]
    public class RideRewards
    {
        public int cashReward;
        public List<CustomerSO> unlockedCustomers;
    
    }
}