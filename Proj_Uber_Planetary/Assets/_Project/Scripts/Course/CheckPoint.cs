using UberPlanetary.Core;
using UberPlanetary.Core.Interfaces;
using UnityEngine;
using UnityEngine.Events;

namespace UberPlanetary.Course
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

        ///Sets the color and invokes corresponding event
        public void SetAsCurrent()
        {
            onCurrentCheckPoint?.Invoke();
            HighlightCheckPoint(litColorPropertyName, currentRingLitColor);
            HighlightCheckPoint(shadedColorPropertyName, currentRingShadedColor);
        }

        ///Changes the color to the next ring colors
        public void SetAsNext()
        {
            HighlightCheckPoint(litColorPropertyName, nextRingLitColor);
            HighlightCheckPoint(shadedColorPropertyName, nextRingShadedColor);
        }

        public Vector3 Position()
        {
            return transform.position;
        }

        /// Takes a string id and color and assigns that to shader's property
        private void HighlightCheckPoint(string id, Color color)
        {
            _material.SetColor(id, color);
        }

        ///called from unity event to update the course
        public void UpdateCourse()
        {
            _course.UpdateCourse();
        }
    }
}
