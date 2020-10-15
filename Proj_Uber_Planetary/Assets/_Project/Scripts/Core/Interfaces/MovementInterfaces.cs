using UnityEngine;

namespace UberPlanetary.Core.Interfaces
{
    /// Moves the player forward based on value
    public interface IBoostHandler
    {
        void Boost(float val);
    }

    /// Moves the object based on provided value
    public interface IMovementHandler
    {
        void MoveForward(float val);
        void MoveBackward(float val);
        void MoveSidewards(float val);
        void MoveVertical(float val);

        float MovementSpeed { get; set; }
    }

    /// Rotates object based on provided vector 3
    public interface IRotationHandler
    {
        //Rotate function
        void Rotate(Vector3 dir);

        ///Reduce Rotation multiplier
        void DampenRotation(float val);
    }
}