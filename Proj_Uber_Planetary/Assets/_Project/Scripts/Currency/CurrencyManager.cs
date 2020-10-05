using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UberPlanetary.Currency
{
    public class CurrencyManager : MonoBehaviour
    {
        private int _amount;

        [SerializeField] private Text cashText;
        [SerializeField] private UnityEvent onValueChanged;
        
        public int Amount
        {
            get => _amount;
            set
            {
                _amount = value;
                cashText.text = Amount.ToString();
                onValueChanged?.Invoke();
            } 
        }
    }
}
