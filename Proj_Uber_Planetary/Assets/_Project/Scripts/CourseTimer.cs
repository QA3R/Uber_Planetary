using System;
using System.Collections;
using System.Collections.Generic;
using UberPlanetary.CheckPoints;
using UberPlanetary.Core;
using UberPlanetary.Player;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;

namespace UberPlanetary
{
    public class CourseTimer : MonoBehaviour
    {
        [SerializeField] private Text TimerText;
        
        private List<TimeStamp> _checkPointTimes = new List<TimeStamp>();
        private List<TimeStamp> _maxSpeedCheckPointTimes = new List<TimeStamp>();
        private List<TimeStamp> _timeBetweenCheckPoints = new List<TimeStamp>();
        private TimeStamp _previousTimeStamp;
        
        private string _finalTimeString;
        private float _milliseconds,_seconds, _minutes;
        private float _timer, _maxSpeedTimer;
        private float _maxSpeedMilliseconds,_maxSpeedSeconds, _maxSpeedMinutes;
        
        private bool _isAboveSpeedThreshold => _playerSpeed.GetValue > .9f;
        private IEventValueProvider<float> _playerSpeed;
        
        public string CourseName { get; set; }
        
        private void Awake()
        {
            _playerSpeed = FindObjectOfType<PlayerController>().GetComponent<IEventValueProvider<float>>();
            Analytics.enabled = true;
        }

        public void ClearTimer()
        {
            // _finalTimeString = "00:00:00";
            _milliseconds = 0;
            _seconds = 0;
            _minutes = 0;
            _maxSpeedTimer = 0f;
            _timer = 0f;
            _checkPointTimes.Clear();
            _maxSpeedCheckPointTimes.Clear();
            _timeBetweenCheckPoints.Clear();
            FormatStringTimer();
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
                    { "String Time : " + i, $"{timeStamps.Minutes:00}:{timeStamps.Seconds:00}:{timeStamps.Milliseconds:00}"},
                    { "Minutes : " + i, timeStamps.Minutes},
                    {"Seconds : " + i, timeStamps.Seconds},
                    {"Milliseconds : " + i, timeStamps.Milliseconds}
                });
                i++;
            }
            i = 1;
            foreach (var timeStamps in _maxSpeedCheckPointTimes)
            {
                Analytics.CustomEvent(CourseName + " Max Speed CheckPoint Times", new Dictionary<string, object>
                {
                    { "String Time : " + i, $"{timeStamps.Minutes:00}:{timeStamps.Seconds:00}:{timeStamps.Milliseconds:00}"},
                    { "Minutes : " + i, timeStamps.Minutes},
                    {"Seconds : " + i, timeStamps.Seconds},
                    {"Milliseconds : " + i, timeStamps.Milliseconds}
                });
                i++;
            }
            i = 1;
            foreach (var timeStamps in _timeBetweenCheckPoints)
            {
                Analytics.CustomEvent(CourseName + " Time Between CheckPoint", new Dictionary<string, object>
                {
                    { "String Time : " + i, $"{timeStamps.Minutes:00}:{timeStamps.Seconds:00}:{timeStamps.Milliseconds:00}"},
                    { "Minutes : " + i, timeStamps.Minutes},
                    {"Seconds : " + i, timeStamps.Seconds},
                    {"Milliseconds : " + i, timeStamps.Milliseconds}
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
                if (_isAboveSpeedThreshold)
                {
                    _maxSpeedTimer += Time.deltaTime;
                    _maxSpeedMilliseconds = (int)((_maxSpeedTimer - (int) _maxSpeedTimer) * 100);
                    _maxSpeedSeconds = (int) (_maxSpeedTimer % 60);
                    _maxSpeedMinutes = (int) (_maxSpeedTimer / 60 % 60);
                }
                FormatStringTimer();
                yield return new WaitForEndOfFrame();
            }
        }

        private void FormatStringTimer()
        {
            //TimerText.text = string.Format("{0:00}:{1:00}:{2:00}", _minutes, _seconds, _milliseconds);
            TimerText.text = $"{_minutes:00}:{_seconds:00}:{_milliseconds:00}";
        }
    }

    [System.Serializable]
    public class TimeData
    {
        private string _courseName;
        private List<TimeStamp> _checkPointTimes;
        private List<TimeStamp> _maxSpeedCheckPointTimes;
        private List<TimeStamp> _timeBetweenCheckPoints;

        public TimeData(string courseName, List<TimeStamp> checkPointTimes, List<TimeStamp> maxSpeedCheckPointTimes, List<TimeStamp> timeBetweenCheckPoints)
        {
            _courseName = courseName;
            _checkPointTimes = checkPointTimes;
            _maxSpeedCheckPointTimes = maxSpeedCheckPointTimes;
            _timeBetweenCheckPoints = timeBetweenCheckPoints;
        }
        
    }
    
    [System.Serializable]
    public class TimeStamp
    {
        private float _milliseconds,_seconds, _minutes;

        public float Milliseconds => _milliseconds;

        public float Seconds => _seconds;

        public float Minutes => _minutes;

        public TimeStamp(float minutes, float seconds, float milliseconds)
        {
            this._minutes = minutes;
            this._milliseconds = milliseconds;
            this._seconds = seconds;
        }
    }
}
