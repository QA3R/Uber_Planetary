using UnityEngine.Events;
using System;
using UnityEngine;
using TMPro;

namespace UberPlanetary.General
{
    public class Clock : MonoBehaviour
    {
        #region Variables
        private TextMeshProUGUI _clockText;
        private bool isOver;
        private float _currentTime;
        private int _hours;
        private int _minutes;

        public float clockSpeed, startTime, endTime;
        public event Action onTimeUp;
        #endregion

        #region Properties
        public int Hours 
        {
            get => _hours;
            set => _hours = value;
        }
        public int Minutes
        {
            get => _minutes;
            set => _minutes = value;
        }
        #endregion

        private void Awake()
        {
            _clockText = GetComponent<TextMeshProUGUI>();
        }
        private void Start()
        {
            _currentTime = startTime;
        }

        void Update()
        {
            //if (isOver) return;

            _currentTime += Time.deltaTime * clockSpeed;

            _minutes = (int)(_currentTime % 60);
            _hours = (int)(_currentTime / 60) % 24;

            _clockText.text = string.Format("{0:00}:{1:00}", _hours, _minutes);

            /*
            if (_currentTime >= endTime)
            {
                TimeUp();
            }*/
        }

        public void TimeUp()
        {
            isOver = true;
            onTimeUp?.Invoke();
        }
    }
}

