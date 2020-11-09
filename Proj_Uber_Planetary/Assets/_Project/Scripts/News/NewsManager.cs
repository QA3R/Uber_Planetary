using System.Collections;
using System.Collections.Generic;
using UberPlanetary.Rides;
using UberPlanetary.ScriptableObjects;
using UnityEngine;

namespace UberPlanetary.News
{
    public class NewsManager : MonoBehaviour
    {
        #region Variables
        [SerializeField]
        private List<NewsArticleSO> newsArticleSOList;
        private RideManager _rideManager;
        #endregion

        #region Properites
        public List<NewsArticleSO> NewsArticleSOList {get => newsArticleSOList;}
        #endregion

        private void Awake()
        {
            _rideManager = GameObject.FindObjectOfType<RideManager>();
        }

        // Start is called before the first frame update
        void Start()
        {
            _rideManager.onRideAccepted.AddListener(FindStory);
        }

        // Will take the _currentCustomer from the RideManager and add it's NewsArticleSO to the list of NewsArticleSo's to populate
        void FindStory(CustomerSO so)
        {
            newsArticleSOList.Add(so.CompletedStoryline);
        }
        void OnDisable()
        {
            _rideManager.onRideAccepted.RemoveListener(FindStory);
        }
    }

}
