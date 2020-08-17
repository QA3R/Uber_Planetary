﻿using System;
using System.Collections.Generic;
using UberPlanetary.Core;
using UnityEngine;

namespace UberPlanetary.CheckPoints
{
    public class Course : MonoBehaviour
    {
        private List<ICheckPoint> _checkPoints = new List<ICheckPoint>();
        private int _courseIndex = 0;

        private void Start()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                _checkPoints.Add(transform.GetChild(i).GetComponent<ICheckPoint>());
            }
            UpdateCourse();
        }

        public void UpdateCourse()
        {
            if (_courseIndex < _checkPoints.Count)
            {
                _checkPoints[_courseIndex].SetAsCurrent();
                if (_courseIndex <_checkPoints.Count -1)
                {
                    _checkPoints[++_courseIndex].SetAsNext();
                }
            }
        }
    }
}