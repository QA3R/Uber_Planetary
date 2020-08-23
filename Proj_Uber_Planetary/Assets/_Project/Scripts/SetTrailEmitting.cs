using UnityEngine;

namespace UberPlanetary
{
    public class SetTrailEmitting : MonoBehaviour
    {
        private TrailRenderer _trail;
        
        private void Awake()
        {
            _trail = GetComponent<TrailRenderer>();
        }

        public void SetEmitting(float val)
        {
            _trail.emitting = val > 0;
        }

        public void ToggleEmitting()
        {
            _trail.emitting = !_trail.emitting;
        }
    }
}
