using UnityEngine.Events;
using System;
using UnityEngine;
using TMPro;

namespace UberPlanetary.General
{
    public class Clock : MonoBehaviour
    {
        private bool isOver;

        public float clockSpeed, startTime, endTime;
        private float _currentTime;

        public event Action onTimeUp;
        

        private TextMeshProUGUI _clockText;
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

            int minutes = (int)(_currentTime % 60);
            int hours = (int)(_currentTime / 60) % 24;

            _clockText.text = string.Format("{0:00}:{1:00}", hours, minutes);

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

