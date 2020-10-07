
using UnityEngine;
using UberPlanetary.Currency;

namespace UberPlanetary.General
{
    public class EndCondition : MonoBehaviour
    {
        public int goalAmount;

        private bool _isGameOver;

        private CurrencyManager _currencyMngr;
        private Clock clock;

        public GameObject winScreen, loseScreen;

        private void Awake()
        {
            _currencyMngr = GetComponent<CurrencyManager>();
            clock = FindObjectOfType<Clock>();
        }
        private void Start()
        {
            _currencyMngr.OnValueChanged.AddListener(CheckWin);
            clock.onTimeUp += Lose;
        }

        
        void CheckWin(int money)
        {
            if (money <= goalAmount) return;

            Win();
        }

        public void Win()
        {
            _isGameOver = true;
            winScreen.SetActive(true);
            Debug.Log("YOU DID IT! You paid your debt to those suckers and now you're free!");
            Time.timeScale = 0;
        }
        public void Lose()
        {
            _isGameOver = true;
            loseScreen.SetActive(true);
            Debug.Log("You couldn't pay in time and now you'll sleep with the fishes");
            Time.timeScale = 0;
        }
    }
}

