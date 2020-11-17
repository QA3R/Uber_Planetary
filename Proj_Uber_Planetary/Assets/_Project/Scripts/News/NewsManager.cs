using System.Collections;
using System.Collections.Generic;
using UberPlanetary.General;
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
            _rideManager.onCustomerPickedUp.AddListener(FindStory);
            EndCondition.onGameOver += MissedUserStories;
        }

        // Will take the _currentCustomer from the RideManager and add it's NewsArticleSO to the list of NewsArticleSo's to populate
        void FindStory(CustomerSO so)
        {
            newsArticleSOList.Add(so.CompletedStoryline);
        }

        private void MissedUserStories()
        {
            foreach (var customerSo in RideLoader.CurrentCustomerList)
            {
                newsArticleSOList.Add(customerSo.CompletedStoryline);
                //TODO: Fix here Daren
            }

            EndCondition.onGameOver -= MissedUserStories;

        }
        void OnDisable()
        {
            _rideManager.onCustomerPickedUp.RemoveListener(FindStory);
        }
    }

}
