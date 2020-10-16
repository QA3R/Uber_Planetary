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
        private static CurrencyManager _instance;
        public static CurrencyManager Instance => _instance;
        
        private int _amount;

        [SerializeField] private Text cashText;
        [SerializeField] private UnityEvent<int> onValueChanged = new UnityEvent<int>();
        
        public UnityEvent<int> OnValueChanged => onValueChanged;
       
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
        
        void Awake ()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
                //Debug.Log("Instance was found");
            }
            else
            {
                _instance = this;
                //Debug.Log("Instance was not found");
                DontDestroyOnLoad(gameObject);
            }
        }
    }
}
