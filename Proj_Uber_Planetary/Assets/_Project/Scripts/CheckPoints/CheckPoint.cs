using System;
using UberPlanetary.Core;
using UnityEngine;

namespace UberPlanetary.CheckPoints
{
    
    public class CheckPoint : MonoBehaviour, ICheckPoint
    {
        private Material _material;
        private Course _course;
        private BoxCollider _collider;
        
        [SerializeField][ColorUsage(false, true)] private Color highlightColor, softHighlightColor;
        [SerializeField] private string colorPropertyName;

        private void Awake()
        {
            _material = GetComponent<Renderer>().material;
            _course = GetComponentInParent<Course>();
            _collider = GetComponent<BoxCollider>();
        }

        public void SetAsCurrent()
        {
            HighlightCheckPoint(colorPropertyName, highlightColor);
            _collider.enabled = true;
        }

        public void SetAsNext()
        {
            HighlightCheckPoint(colorPropertyName, softHighlightColor);
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
