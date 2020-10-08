using UberPlanetary.Core;
using UnityEngine;

namespace UberPlanetary.ScriptableObjects
{
    [System.Serializable]
    public class Ride 
    {
        public int rideReward;
        
        [Tooltip("Optional, leave empty and the quest manager will find a random location for it")]
        [SerializeField] private GameObject rideEndLocationHolder, rideStartLocationHolder;

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
                    if (rideEndLocationHolder != null)
                    {
                        if (rideEndLocationHolder.GetComponent<ILandmark>() != null)
                        {
                            _rideEndLandmark = rideEndLocationHolder.GetComponent<ILandmark>();
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
                    if (rideEndLocationHolder != null)
                    {
                        if (rideStartLocationHolder.GetComponent<ILandmark>() != null)
                        {
                            _rideStartLandmark = rideStartLocationHolder.GetComponent<ILandmark>();
                        }
                    }
                }
                return _rideStartLandmark;
            }
            set => _rideStartLandmark = value;
        }
    }
}
