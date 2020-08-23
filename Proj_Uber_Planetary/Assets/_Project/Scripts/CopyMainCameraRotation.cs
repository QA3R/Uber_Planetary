using UberPlanetary.Core;
using UnityEngine;

namespace UberPlanetary
{
    public class CopyMainCameraRotation : MonoBehaviour
    {
        private IEventValueProvider<Quaternion> _camRotation;

        private void Awake()
        {
            _camRotation = Camera.main.GetComponent<IEventValueProvider<Quaternion>>();
        }

        private void Update()
        {
            transform.rotation = _camRotation.GetValue;
        }
    }
}
