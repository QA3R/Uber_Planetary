using UnityEngine;

namespace UberPlanetary.Core
{
    /// <summary>
    /// Rotates the object in the axis with a speed
    /// </summary>
    public class RotateOverTimeOnAxis : MonoBehaviour
    {
        //[SerializeField] private bool x, y, z;
        [SerializeField] [Range(0,1)] private float xMultiplier, yMultiplier, zMultiplier;
        [SerializeField] private float speed;

        private void Update()
        {
            transform.Rotate(new Vector3(xMultiplier, yMultiplier, zMultiplier) * (speed * Time.deltaTime));
        }
    }
}
