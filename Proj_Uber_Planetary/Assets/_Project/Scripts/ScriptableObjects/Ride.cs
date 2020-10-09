using UberPlanetary.Core;
using UberPlanetary.Navigation;
using UnityEngine;

namespace UberPlanetary.ScriptableObjects
{
    [System.Serializable]
    public class Ride 
    {
        public int rideReward;
        
        [Tooltip("Optional, leave empty and the quest manager will find a random location for it")]
        [SerializeField] private string endLocationStringID, startLocationStringID;
        [Tooltip("Optional, leave empty and the quest manager will find a random location for it")]
        [SerializeField] private int endLocationIntID, startLocationIntID;

        private ILandmark _rideStartLandmark;
        private ILandmark _rideEndLandmark;
        private ILandmark _rideCurrentLandmark;

        public ILandmark RideCurrentLandmark
        {
            get => _rideCurrentLandmark;
            set => _rideCurrentLandmark = value;
        }

        public int RideReward => rideReward;

        public ILandmark RideEndLandmark
        {
            get
            {
                if (_rideEndLandmark == null)
                {
                    if (!string.IsNullOrEmpty(endLocationStringID))
                    {
                        if (NavigationManager.Instance.stringLandmarkDictionary[endLocationStringID] != null)
                        {
                            _rideEndLandmark = NavigationManager.Instance.stringLandmarkDictionary[endLocationStringID];
                        }
                    }
                    else
                    {
                        if (endLocationIntID != 0)
                        {
                            if (NavigationManager.Instance.intLandmarkDictionary[endLocationIntID] != null)
                            {
                                _rideEndLandmark = NavigationManager.Instance.intLandmarkDictionary[endLocationIntID];
                            }
                        }
                    }
                }
                return _rideEndLandmark;
            }
            set => _rideEndLandmark = value;
        }
        public ILandmark RideStartLandmark
        {
            get
            {
                if (_rideStartLandmark == null)
                {
                    if (!string.IsNullOrEmpty(startLocationStringID))
                    {
                        if (NavigationManager.Instance.stringLandmarkDictionary[startLocationStringID] != null)
                        {
                            _rideStartLandmark = NavigationManager.Instance.stringLandmarkDictionary[startLocationStringID];
                        }
                    }
                    else
                    {
                        if (startLocationIntID != 0)
                        {
                            if (NavigationManager.Instance.intLandmarkDictionary[startLocationIntID] != null)
                            {
                                _rideEndLandmark = NavigationManager.Instance.intLandmarkDictionary[startLocationIntID];
                            }
                        }
                    }
                }
                return _rideStartLandmark;
            }
            set => _rideStartLandmark = value;
        }
    }
}
