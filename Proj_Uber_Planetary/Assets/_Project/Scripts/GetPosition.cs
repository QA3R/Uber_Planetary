using UberPlanetary.Core;
using UberPlanetary.Core.Interfaces;
using UnityEngine;

namespace UberPlanetary
{
    public class GetPosition : MonoBehaviour, IEventValueProvider<Vector3>
    {
        public Vector3 GetValue => transform.position;
    }
}
