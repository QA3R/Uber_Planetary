using System.Collections;
using System.Collections.Generic;
using TMPro;
using UberPlanetary.ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;



namespace UberPlanetary.News
    {
    //NOTE: Add to the correct namespace
    public class ArticleItem : MonoBehaviour
    {
        //NOTE: See if there are any variables you are not using anymore, basically cleanup
        #region Variables
        [SerializeField] private TextMeshProUGUI articleTitle;
        [SerializeField] private Button button;
        private GameObject _articlePanel;
        private GameObject _articleHolder;
        private NewsArticleSO _articleSo;
        private NewsApplication _newsApp;
        #endregion

        private void Start()
        {
            _newsApp = GameObject.FindObjectOfType<NewsApplication>();
        }

        /// <summary>
        /// Assigns a reference of the passed in newsArticleSO from the NewsManager to a private NewsArticleSO that exists in this class
        /// Gets reference to the NewsApplication script 
        /// Set the headline text to the passed in newsArticleSO's headline
        /// </summary>
        public void Initalize(NewsArticleSO newsArticleSO)
        {
            _articleSo = newsArticleSO;
            articleTitle.text = newsArticleSO.ArticleHeadline; 
        }

        /// <summary>
        ///  Activates the gameobject (ArticlePanel)
        ///  Sets the infomration based on the passed information from the NewsManager
        /// </summary>
        public void TogglePanel() 
        {
            _newsApp.ArticlePanel.gameObject.SetActive(true);
            _newsApp.ArticlePanelHeadline.text = _articleSo.ArticleHeadline;
            _newsApp.ArticlePanelText.text = _articleSo.ArticleStory;
            _newsApp.ArticlePanelImage.sprite = _articleSo.ArticleSprite;      

        }
    }
}
