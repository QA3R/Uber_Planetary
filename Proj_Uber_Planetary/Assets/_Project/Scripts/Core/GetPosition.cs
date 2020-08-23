using UnityEngine;

namespace UberPlanetary.Core
{
    public class GetPosition : MonoBehaviour, IEventValueProvider<Vector3>
    {
        public Vector3 GetValue => transform.position;
    }
}
