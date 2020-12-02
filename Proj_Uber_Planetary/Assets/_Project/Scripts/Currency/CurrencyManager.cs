using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UberPlanetary.Currency
{
    /// <summary>
    /// Keeps track of the money the player has and exposes event to update value
    /// </summary>
    
    public class CurrencyManager : MonoBehaviour
    {
        #region Variables
        private static CurrencyManager _instance;
        public static CurrencyManager Instance => _instance;
        
        private int _amount;

        [SerializeField] private Text cashText;
        [SerializeField] private UnityEvent<int> onValueChanged = new UnityEvent<int>();
        
        public UnityEvent<int> OnValueChanged => onValueChanged;
        #endregion

        #region Properties
        public int Amount
        {
            get => _amount;
            set
            {
                _amount = value;
                cashText.text = Amount.ToString();
                onValueChanged?.Invoke(_amount);
            } 
        }
        #endregion

        #region Awake method
        void Awake ()
        {
            // Checks if there is a pre-existing CurrencyManager
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
                //Debug.Log("Instance was found");
            }
            else
            {
                //Sets the instance of the TimeManager to this object and assigns this Object as DontDestroyOnLoad
                _instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }
        #endregion
    }
}
