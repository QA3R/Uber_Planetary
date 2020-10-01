using UberPlanetary.Core;
using UnityEngine;

namespace UberPlanetary.Player.Movement.RotationHandlers
{
    public class ClampedRotationHandler : BaseRotationHandler
    {

        [SerializeField] protected Vector2 xRotMinMax, yRotMinMax, zRotMinMax;
        public override void Rotate(Vector3 dir)
        {
            // var rotation = transform.rotation;
            // dir.x = transform.rotation.eulerAngles.x > xRotMinMax.x && transform.rotation.eulerAngles.x < xRotMinMax.y ? dir.x : 0;
            // dir.y = transform.rotation.eulerAngles.y > yRotMinMax.x && transform.rotation.eulerAngles.y < yRotMinMax.y ? dir.y : 0;
            // dir.z = transform.rotation.eulerAngles.z > zRotMinMax.x && transform.rotation.eulerAngles.z < zRotMinMax.y ? dir.z : 0;

            Debug.Log(dir);
            transform.Rotate(new Vector3(dir.x * xRotationSpeed,dir.y * yRotationSpeed,-dir.z * zRotationSpeed) * (rotationLossMultiplier * Time.deltaTime), Space.World);
            
        }
    }
}