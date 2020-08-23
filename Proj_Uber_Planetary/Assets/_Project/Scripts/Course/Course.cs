using System.Collections.Generic;
using UberPlanetary.Core;
using UnityEngine;
using UnityEngine.Events;

namespace UberPlanetary.Course
{
    /// <summary>
    /// Generates a list of checkpoints from the provided objet's children and calls functions on the interface appropriately 
    /// </summary>
    public class Course : MonoBehaviour
    {
        private List<ICheckPoint> _checkPoints = new List<ICheckPoint>();
        private int _courseIndex = 0;

        [SerializeField] UnityEvent<Vector3> onTargetChanged;
        [SerializeField] GameObject ringHolder;
        
        private void Start()
        {
            for (int i = 0; i < ringHolder.transform.childCount; i++)
            {
                _checkPoints.Add(ringHolder.transform.GetChild(i).GetComponent<ICheckPoint>());
            }
            UpdateCourse();
        }

        //Error checking for end of list
        public void UpdateCourse()
        {
            if (_courseIndex < _checkPoints.Count)
            {
                _checkPoints[_courseIndex].SetAsCurrent();
                onTargetChanged?.Invoke(_checkPoints[_courseIndex].Position());
                if (_courseIndex <_checkPoints.Count -1)
                {
                    _checkPoints[++_courseIndex].SetAsNext();
                }
            }
        }
    }
}
