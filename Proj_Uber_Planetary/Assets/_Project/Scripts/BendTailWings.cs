using UberPlanetary.Core;
using UnityEngine;

namespace UberPlanetary
{
    public class BendTailWings : MonoBehaviour
    {
        [SerializeField] private Transform leftTail, rightTail;
        [SerializeField] private float min, max;
        [SerializeField] private AnimationCurve curve;
        private float _leftCurrent, _rightCurrent;
        
        public void SetTailRotation(float val)
        {
            _leftCurrent = curve.Evaluate(val).Remap(0, 1, min, max);
            _rightCurrent = curve.Evaluate(val).Remap(0, 1, -min, -max);
            
            leftTail.transform.localRotation = Quaternion.Euler(0,_leftCurrent,0);
            rightTail.transform.localRotation = Quaternion.Euler(0,_rightCurrent,0);
        }
    }
}
