
using System;
using UnityEngine;
using UberPlanetary.Currency;

namespace UberPlanetary.General
{
    public class EndCondition : MonoBehaviour
    {
        [SerializeField] private NewsApplication newsApplication;

        public int goalAmount;

        private bool _isGameOver;

        private CurrencyManager _currencyMngr;
        private Clock clock;

        public GameObject winScreen, loseScreen;

        public static event Action onGameOver;

        private void Awake()
        {
            _currencyMngr = FindObjectOfType<CurrencyManager>();
            clock = FindObjectOfType<Clock>();
        }
        private void Start()
        {
            _currencyMngr.OnValueChanged.AddListener(CheckWin);
            clock.onTimeUp += Lose;
            Cursor.visible = false;
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
            onGameOver?.Invoke();
            Time.timeScale = 0;
            newsApplication.Populate();
            Cursor.visible = true;
        }
        public void Lose()
        {
            _isGameOver = true;
            loseScreen.SetActive(true);
            Debug.Log("You couldn't pay in time and now you'll sleep with the fishes");
            onGameOver?.Invoke();
            Time.timeScale = 0;
            Cursor.visible = false;
        }

        private void OnDisable()
        {
            _currencyMngr.OnValueChanged.RemoveListener(CheckWin);
        }
    }
}

