using System.Collections; //NOTE: You can remove unused using statements too btw
using System.Collections.Generic;
using UberPlanetary.ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//NOTE: Namespace
public class NewsApplication : MonoBehaviour
{
    #region Variables
    [SerializeField] private GameObject _articleStory; //NOTE: naming convention
    [SerializeField] private NewsArticleSO testStory;
    [SerializeField] private Transform newsArticleHolder;
    [SerializeField] private GameObject articlePanel;
    [SerializeField] private TextMeshProUGUI articlePanelText;
    [SerializeField] private TextMeshProUGUI articlePanelHeadline;
    [SerializeField] private Image articlePanelImage;
    #endregion
//
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
    void TestPopulate()
    {
        PopulateArticleBoard(testStory); //NOTE: You can remove this too when you are done testing,
                                         //but we still need to hook things up so you can leave it for later
    }

    // Will instantiate a prefab of the articles in the websitepanel
    void PopulateArticleBoard (NewsArticleSO newsArticleSO)
    {
        // Will eventually need to happen based on the number of customers involved with that particular day
        GameObject tempArticleStory = Instantiate(_articleStory, newsArticleHolder);
        tempArticleStory.GetComponent<ArticleItem>().Initalize(newsArticleSO, this);
    }
}
