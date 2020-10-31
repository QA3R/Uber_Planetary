using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UberPlanetary.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Article", menuName = "ScriptableObjects/Create Article", order = 1)]
    public class NewsArticleSO : ScriptableObject
    {
        //NOTE: serialized fields should not be _fieldName they should just be fileName since they are not really private
        //They can be changed in the editor which is why we do this, even tho they are set to private technically :D
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

