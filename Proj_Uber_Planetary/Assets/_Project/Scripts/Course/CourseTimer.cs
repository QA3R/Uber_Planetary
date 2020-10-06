using System;
using System.Collections;
using System.Collections.Generic;
using UberPlanetary.Core;
using UberPlanetary.Player;
using UberPlanetary.Player.Movement;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;

namespace UberPlanetary.Course
{
    public class CourseTimer : MonoBehaviour
    {
        [SerializeField] private Text timerText;
        
        private readonly List<TimeStamp> _checkPointTimes = new List<TimeStamp>();
        private readonly List<TimeStamp> _maxSpeedCheckPointTimes = new List<TimeStamp>();
        private readonly List<TimeStamp> _timeBetweenCheckPoints = new List<TimeStamp>();
        private TimeStamp _previousTimeStamp;
        
        private string _finalTimeString;
        private float _milliseconds,_seconds, _minutes;
        private float _timer, _maxSpeedTimer;
        private float _maxSpeedMilliseconds,_maxSpeedSeconds, _maxSpeedMinutes;
        
        private bool IsAboveSpeedThreshold => _playerSpeed.GetValue > .8f;
        private IEventValueProvider<float> _playerSpeed;
        
        public string CourseName { get; set; }
        
        private void Awake()
        {
            _playerSpeed = FindObjectOfType<PlayerController>().GetComponent<IEventValueProvider<float>>();
            Analytics.enabled = true;
        }

        private void ClearTimer()
        {
            _milliseconds = 0;
            _seconds = 0;
            _minutes = 0;
            _maxSpeedTimer = 0f;
            _timer = 0f;
            _checkPointTimes.Clear();
            _maxSpeedCheckPointTimes.Clear();
            _timeBetweenCheckPoints.Clear();
            timerText.text = FormatStringTimer(0,0,0);
        }

        [ContextMenu("Start Timer")]
        public void StartNewTimer()
        {
            ClearTimer();
            StartCoroutine(Timer());
        }

        public void AddNewTimeStamp()
        {
            if (_previousTimeStamp != null)
            {
                _timeBetweenCheckPoints.Add(new TimeStamp(_minutes - _previousTimeStamp.Minutes, _seconds - _previousTimeStamp.Seconds, _milliseconds - _previousTimeStamp.Milliseconds));
            }
            _previousTimeStamp = new TimeStamp(_minutes, _seconds, _milliseconds);
            _checkPointTimes.Add(_previousTimeStamp);
            _maxSpeedCheckPointTimes.Add(new TimeStamp(_maxSpeedMinutes, _maxSpeedSeconds, _maxSpeedMilliseconds));
        }

        public void ExportData()
        {
            int i = 1;
            Analytics.CustomEvent(CourseName + "Completed!");
            foreach (var timeStamps in _checkPointTimes)
            {
                Analytics.CustomEvent(CourseName + " CheckPoint Times", new Dictionary<string, object>
                {
                    { "String Time : " + i, FormatStringTimer(timeStamps.Minutes,timeStamps.Seconds, timeStamps.Milliseconds)},
                    // { "Minutes : " + i, timeStamps.Minutes},
                    // {"Seconds : " + i, timeStamps.Seconds},
                    // {"Milliseconds : " + i, timeStamps.Milliseconds}
                });
                i++;
            }
            i = 1;
            foreach (var timeStamps in _maxSpeedCheckPointTimes)
            {
                Analytics.CustomEvent(CourseName + " Max Speed CheckPoint Times", new Dictionary<string, object>
                {
                    { "String Time : " + i, FormatStringTimer(timeStamps.Minutes,timeStamps.Seconds, timeStamps.Milliseconds)},
                    // { "Minutes : " + i, timeStamps.Minutes},
                    // {"Seconds : " + i, timeStamps.Seconds},
                    // {"Milliseconds : " + i, timeStamps.Milliseconds}
                });
                i++;
            }
            i = 1;
            foreach (var timeStamps in _timeBetweenCheckPoints)
            {
                Analytics.CustomEvent(CourseName + " Time Between CheckPoint", new Dictionary<string, object>
                {
                    { "String Time : " + i, FormatStringTimer(timeStamps.Minutes,timeStamps.Seconds, timeStamps.Milliseconds)},
                    // { "Minutes : " + i, timeStamps.Minutes},
                    // {"Seconds : " + i, timeStamps.Seconds},
                    // {"Milliseconds : " + i, timeStamps.Milliseconds}
                });
                i++;
            }
        }
        
        [ContextMenu("Stop Timer")]
        public void StopTimer()
        {
            StopAllCoroutines();
            ExportData();
        }

        private IEnumerator Timer()
        {
            while (true)
            {
                _timer += Time.deltaTime;
                _milliseconds = (int)((_timer - (int) _timer) * 100);
                _seconds = (int) (_timer % 60);
                _minutes = (int) (_timer / 60 % 60);
                if (IsAboveSpeedThreshold)
                {
                    _maxSpeedTimer += Time.deltaTime;
                    _maxSpeedMilliseconds = (int)((_maxSpeedTimer - (int) _maxSpeedTimer) * 100);
                    _maxSpeedSeconds = (int) (_maxSpeedTimer % 60);
                    _maxSpeedMinutes = (int) (_maxSpeedTimer / 60 % 60);
                }
                timerText.text = FormatStringTimer(_minutes,_seconds,_milliseconds);
                yield return new WaitForEndOfFrame();
            }
        }

        private static string FormatStringTimer(float m, float s, float ms)
        {
            return $"{m:00}:{s:00}:{ms:00}";
        }
    }

    [Serializable]
    public class TimeStamp
    {
        public float Milliseconds { get; }

        public float Seconds { get; }

        public float Minutes { get; }

        public TimeStamp(float minutes, float seconds, float milliseconds)
        {
            this.Minutes = minutes;
            this.Milliseconds = milliseconds;
            this.Seconds = seconds;
        }
    }
}