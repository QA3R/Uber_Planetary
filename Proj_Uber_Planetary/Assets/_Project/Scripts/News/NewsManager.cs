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

        #region Start and Awake
        private void Awake()
        {
            _rideManager = GameObject.FindObjectOfType<RideManager>();
        }

        // Start is called before the first frame update
        void Start()
        {
            _rideManager.onCustomerPickedUp.AddListener(AddCompeltedStory);
            EndCondition.onGameOver += FindMissedUserStories;
        }
        #endregion

        #region Methods
        // Will take the _currentCustomer from the RideManager and add it's NewsArticleSO to the list of NewsArticleSo's to populate
        void AddCompeltedStory(CustomerSO so)
        {
            newsArticleSOList.Add(so.CompletedStoryline);
        }

        // Checks for non-completed quests in our client list
        private void FindMissedUserStories()
        {
            foreach (var customerSo in RideLoader.CurrentCustomerList)
            {
                newsArticleSOList.Add(customerSo.BaseStoryline);
            }
            EndCondition.onGameOver -= FindMissedUserStories;
        }
        void OnDisable()
        {
            _rideManager.onCustomerPickedUp.RemoveListener(AddCompeltedStory);
        }
    }
    #endregion
}
