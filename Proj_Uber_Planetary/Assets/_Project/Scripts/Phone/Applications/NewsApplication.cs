using System.Collections; //NOTE: You can remove unused using statements too btw
using System.Collections.Generic;
using UberPlanetary.ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UberPlanetary.News;

//NOTE: Namespace
public class NewsApplication : MonoBehaviour
{
    #region Variables
    [SerializeField] private GameObject articleStory; 
    [SerializeField] private NewsArticleSO Story;
    [SerializeField] private Transform newsArticleHolder;
    [SerializeField] private GameObject articlePanel;
    [SerializeField] private TextMeshProUGUI articlePanelText;
    [SerializeField] private TextMeshProUGUI articlePanelHeadline;
    [SerializeField] private Image articlePanelImage;
    [SerializeField] private NewsManager newsManager;
    [SerializeField] private NewsApplication newsApplication;
    #endregion

    #region Properties
    public GameObject ArticlePanel
    {
        get => articlePanel;
        set => articlePanel = value;
    }
    public TextMeshProUGUI ArticlePanelText 
    {   get => articlePanelText;
        set => articlePanelText = value;
    }
    public TextMeshProUGUI ArticlePanelHeadline 
    { 
        get => articlePanelHeadline; 
        set => articlePanelHeadline = value; 
    }
    public Image ArticlePanelImage 
    { 
        get => articlePanelImage; 
        set => articlePanelImage = value; 
    }
    #endregion

    // Test method which instantiates the article prefabs using PopulateArticleBoard()
    [ContextMenu ("Populate Scrollview")]
    public void Populate()
    {
        foreach (NewsArticleSO newsArticleSO in newsManager.NewsArticleSOList)
        {
            GameObject tempArticleStory = Instantiate(articleStory, newsArticleHolder);
            tempArticleStory.GetComponent<ArticleItem>().Initalize(newsArticleSO);
            
            /*
            newsArticleSO.ArticleHeadline = newsArticleSO.ArticleHeadline;
            newsArticleSO.ArticleSprite = newsArticleSO.ArticleSprite;
            newsArticleSO.ArticleStory = newsArticleSO.ArticleStory;

            */
        }
    }

    // Will instantiate a prefab of the articles in the websitepanel
    void PopulateArticleBoard (NewsArticleSO newsArticleSO)
    {
        // Will eventually need to happen based on the number of customers involved with that particular day
        GameObject tempArticleStory = Instantiate(articleStory, newsArticleHolder);
        tempArticleStory.GetComponent<ArticleItem>().Initalize(newsArticleSO);
    }
}
