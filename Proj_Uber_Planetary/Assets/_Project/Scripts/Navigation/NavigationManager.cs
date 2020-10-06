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
        private List<IGeneralLandmark> _generalLandmarks;

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

        public List<IGeneralLandmark> GeneralLandmarks
        {
            get => _generalLandmarks;
            //set => _generalLandmarks = value;
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

        public IGeneralLandmark getRandomGeneralLandmark()
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

            var rand = Random.Range(0, temp.Count);
            
            return temp[rand];
        }
    }
}