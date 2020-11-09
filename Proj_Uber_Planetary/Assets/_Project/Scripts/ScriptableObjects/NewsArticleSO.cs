using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;
using UnityEngine.UI;

namespace UberPlanetary.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Article", menuName = "ScriptableObjects/Create Article", order = 1)]
    public class NewsArticleSO : ScriptableObject
    {
        #region Variables
        [SerializeField] private string articleHeadline;
        [SerializeField] private string articleStory;
        [SerializeField] private Sprite articleSprite;
        #endregion

        #region Properties
        public string ArticleHeadline 
        {
            get => articleHeadline;
            set => articleHeadline = value;
        }

        public string ArticleStory 
        { 
            get => articleStory;
            set => articleStory = value;
        
        }
        public Sprite ArticleSprite 
        {
            get => articleSprite;
            set => articleSprite = value;
        }
        #endregion
    }
}

