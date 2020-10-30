using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UberPlanetary.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Article", menuName = "ScriptableObjects/Create Article", order = 1)]
    public class NewsArticleSO : ScriptableObject
    {
        #region Variables
        [SerializeField] private string _articleHeadline;
        [SerializeField] private string _articleStory;
        [SerializeField] private Sprite _articleSprite;
        #endregion

        public string ArticleHeadline => _articleHeadline;
        public string ArticleStory => _articleStory;
        public Sprite ArticleSprite => _articleSprite;
    }
}

