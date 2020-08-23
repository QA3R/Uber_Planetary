using UnityEngine;

namespace UberPlanetary.Core
{
    public class LookAtTarget
    {
        private Vector3 _lookTo;
        private Vector3 _lookFrom;
        private float _threshold;
        
        public Vector3 LookTo
        {
            set => _lookTo = value;
        }

        public Vector3 LookFrom
        {
            set => _lookFrom = value;
        }

        public float Threshold
        {
            set => _threshold = value;
        }
        
        public Quaternion GetLookRotation()
        {
            Vector3 dir = _lookTo - _lookFrom;
            if (dir.magnitude > _threshold)
                return Quaternion.LookRotation(dir);
            return Quaternion.identity;
        }
    }
}