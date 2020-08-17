using System;
using UberPlanetary.Core;
using UnityEngine;
using UnityEngine.Events;

namespace UberPlanetary.CheckPoints
{
    
    public class CheckPoint : MonoBehaviour, ICheckPoint
    {
        private Material _material;
        private Course _course;
        private BoxCollider _collider;
        
        [SerializeField][ColorUsage(false, true)] private Color currentRingLitColor, currentRingShadedColor, nextRingLitColor, nextRingShadedColor ;
        [SerializeField] private string litColorPropertyName, shadedColorPropertyName;
        [SerializeField] private UnityEvent onCurrentCheckPoint;

        private void Awake()
        {
            _material = GetComponent<Renderer>().material;
            _course = GetComponentInParent<Course>();
            _collider = GetComponent<BoxCollider>();
        }

        public void SetAsCurrent()
        {
            onCurrentCheckPoint?.Invoke();
            HighlightCheckPoint(litColorPropertyName, currentRingLitColor);
            HighlightCheckPoint(shadedColorPropertyName, currentRingShadedColor);
            _collider.enabled = true;
        }

        public void SetAsNext()
        {
            HighlightCheckPoint(litColorPropertyName, nextRingLitColor);
            HighlightCheckPoint(shadedColorPropertyName, nextRingShadedColor);
        }
        
        private void HighlightCheckPoint(string id, Color color)
        {
            _material.SetColor(id, color);
        }

        public void UpdateCourse()
        {
            _course.UpdateCourse();
        }
    }
}
