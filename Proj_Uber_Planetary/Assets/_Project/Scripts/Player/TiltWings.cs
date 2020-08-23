using UberPlanetary.Core;
using UnityEngine;

namespace UberPlanetary.Player
{
    public class TiltWings : MonoBehaviour
    {
        [SerializeField] private float xMin, xMax, yMin, yMax;
        private float _currentXRot, _currentYRot;

        public void SetWingRotation(Vector2 val)
        {
            _currentXRot = val.y.Remap(-1, 1, xMin, xMax);
            _currentYRot = val.x.Remap(-1, 1, yMin, yMax);
            
            transform.localRotation = Quaternion.Euler(_currentXRot, _currentYRot, 0);
        }
    }
}
