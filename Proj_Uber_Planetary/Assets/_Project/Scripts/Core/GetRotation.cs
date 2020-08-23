using UnityEngine;

namespace UberPlanetary.Core
{
    public class GetRotation : MonoBehaviour , IEventValueProvider<Quaternion>
    {
        public Quaternion GetValue => transform.rotation;
    }
}
