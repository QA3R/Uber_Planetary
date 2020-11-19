using UberPlanetary.Navigation;
using UberPlanetary.Rides;
using UnityEngine;

namespace UberPlanetary.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Customer", menuName = "ScriptableObjects/Create Customer", order = 1)]
    public class CustomerSO : ScriptableObject
    {
        #region Variables
        [SerializeField] private string customerName;
        [SerializeField] private Sprite customerFace;
        [SerializeField] private float customerMood;
        [SerializeField] private DialogueSO customerDialogue;
        [SerializeField] private NewsArticleSO completedStoryline;
        [SerializeField] private NewsArticleSO baseStoryline;
        [SerializeField] private Ride customerRide;
        #endregion

        #region Properties
        public string CustomerName => customerName;
        public Sprite CustomerFace => customerFace;
        public NewsArticleSO CompletedStoryline => completedStoryline;
        public NewsArticleSO BaseStoryline => baseStoryline;
        public Ride CustomerRide => customerRide;
        public DialogueSO CustomerDialogue => customerDialogue;
        public float CustomerMood
        {
            get => customerMood;
            set => customerMood = Mathf.Clamp01(value);
        }
        #endregion
    }
}

