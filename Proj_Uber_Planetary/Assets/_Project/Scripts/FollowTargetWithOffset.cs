using UnityEngine;

namespace UberPlanetary
{
    public class FollowTargetWithOffset : MonoBehaviour
    {
        [SerializeField] private Transform target;
        private Vector3 _offSet;

        private void Awake()
        {
            _offSet = transform.position;
        }

        private void Update()
        {
            transform.position = target.transform.position + _offSet;
        }
    }
}
