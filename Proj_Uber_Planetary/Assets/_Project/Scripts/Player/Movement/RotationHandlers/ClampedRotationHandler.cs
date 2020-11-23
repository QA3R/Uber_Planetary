using UberPlanetary.Core;
using UberPlanetary.Core.ExtensionMethods;
using UnityEngine;

namespace UberPlanetary.Player.Movement.RotationHandlers
{
    public class ClampedRotationHandler : BaseRotationHandler
    {

        [SerializeField] protected Vector2 xRotMinMax, yRotMinMax, zRotMinMax;
        [SerializeField] protected bool clampX, clampY, clampZ;
        [SerializeField] protected GameObject parentObject;
        public override void Rotate(Vector3 dir)
        {

            if (!clampX) return;
            
            transform.Rotate(new Vector3(0,dir.y * yRotationSpeed,-dir.z * zRotationSpeed) * (rotationLossMultiplier * Time.deltaTime));
           transform.localRotation = Quaternion.Euler(new Vector3(RotateOnAxis(dir.x, xRotMinMax), transform.localRotation.eulerAngles.y, 0));
           // _rigidbody.MoveRotation(deltaRot);

           // parentObject.transform.localRotation = 
            //_rigidbody.MoveRotation(Quaternion.Euler(RotateOnAxis(dir.x, xRotMinMax),0, 0 ));
            //base.Rotate(new Vector3(0,dir.y,dir.z));
            // else
            // {
            //     base.Rotate(new Vector3(dir.x,0,0));
            // }
            //
            // if (clampY)
            // {
            //     transform.localRotation = Quaternion.Euler(transform.localRotation.eulerAngles.x,RotateOnAxis(dir.y, yRotMinMax),transform.localRotation.eulerAngles.z ) ;
            // }
            // else
            // {
            //     base.Rotate(new Vector3(0,dir.y,0));
            // }
            //
            // if (clampZ)
            // {
            //     transform.localRotation = Quaternion.Euler(transform.localRotation.eulerAngles.x,transform.localRotation.eulerAngles.y,RotateOnAxis(dir.z, zRotMinMax) ) ;
            // }
            // else
            // {
            //     base.Rotate(new Vector3(0,0,dir.z));
            // }
            //
            // if (!clampX && !clampY && !clampZ)
            // {
            //     base.Rotate(dir);
            // }
        }

        private float RotateOnAxis( float val, Vector2 minMax)
        {
            return val.Remap(-1, 1, minMax.x, minMax.y);
        }
    }
}