using System;
using System.Collections.Generic;
using System.Linq;
using UberPlanetary.Core;
using UnityEngine;
using Random = UnityEngine.Random;

namespace UberPlanetary.Navigation
{
    public class NavigationManager : MonoBehaviour
    {
        private static NavigationManager _instance;
        
        private List<ILandmark> _landmarks;

        private Sprite passengerPickUpSprite;
        private Sprite passengerDropOffSprite;
        private Sprite additionalQuestSprite;

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

        private void Awake()
        {
            Instance = _instance ? _instance : this;
        }

        public ILandmark GetRandomLandmark()
        {
            var rand = Random.Range(0, _landmarks.Count);
            return _landmarks[rand];
        }
        public ILandmark GetFurthestLandmark(Vector3 from)
        {
            return _landmarks.OrderBy(x => (from - x.GetPosition).magnitude).Last();
        }        
        public ILandmark GetNearestLandmark(Vector3 from)
        {
            return _landmarks.OrderBy(x => (from - x.GetPosition).magnitude).First();
        }
    }
}