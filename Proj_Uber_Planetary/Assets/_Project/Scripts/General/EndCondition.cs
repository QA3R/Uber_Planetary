
using System;
using UnityEngine;
using UberPlanetary.Currency;
using UberPlanetary.News;

namespace UberPlanetary.General
{
    public class EndCondition : MonoBehaviour
    {
        public static event Action OnEndedGame;
        public static event Action onGameOver;


        [SerializeField] private CurrencyManager _currencyMngr;
        private bool _isGameOver;
        private Clock clock;
        public int goalAmount;
        public GameObject winScreen, loseScreen;




        private void Awake()
        {
            _currencyMngr = FindObjectOfType<CurrencyManager>();
            clock = FindObjectOfType<Clock>();
        }
        private void Start()
        {
            _currencyMngr.OnValueChanged.AddListener(CheckWin);
            //clock.onTimeUp += Lose;
            Cursor.visible = false;
        }
        
        void CheckWin(int money)
        {
            if (money <= goalAmount) return;

            Win();
        }

        public void Win()
        {
            if (OnEndedGame != null)
                OnEndedGame();

            //newsApplication.Populate();
            _isGameOver = true;
            winScreen.SetActive(true);
            Debug.Log("YOU DID IT! You paid your debt to those suckers and now you're free!");
            onGameOver?.Invoke();
            Time.timeScale = 0;
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

