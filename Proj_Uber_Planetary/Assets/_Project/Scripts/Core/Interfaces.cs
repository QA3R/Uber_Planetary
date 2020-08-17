using System;
using UnityEngine;

namespace UberPlanetary.Core
{
    /// <summary>
    /// Finds the appropriate IEventValueProvider and exposes an event based on provided value
    /// </summary>
    public interface IEventExposer<T> where T : struct
    {
        /// <summary>
        /// Find The reference to the EventValueProvider
        /// </summary>
        void SetReference();
        
        /// <summary>
        /// Call Event on Update
        /// </summary>
        void InvokeEvent();
        
        /// <summary>
        /// The Value provider
        /// </summary>
        IEventValueProvider<T> EventValueProvider { get; set; }
    }

    /// <summary>
    /// Returns a value of type T when requested by a IEventExposer
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IEventValueProvider<out T> where T : struct
    {
        /// <summary>
        /// Return a value for events
        /// </summary>
        T GetValue { get;}
    }

    /// <summary>
    /// Damageable Interface
    /// </summary>
    public interface ITakeDamage
    {
        void TakeDamage();
    }

    /// <summary>
    /// Rotates object based on provided vector 3
    /// </summary>
    public interface IRotationHandler
    {
        //Rotate function
        void Rotate(Vector3 dir);
        //Reduce Rotation multiplier
        void DampenRotation();
        //ResetRotation multiplier
        void ResetRotationMultiplier();
    }

    /// <summary>
    /// Moves the object based on provided value
    /// </summary>
    public interface IMovementHandler
    {
        void MoveForward(float val);
        void MoveBackward(float val);
    }

    /// <summary>
    /// Moves the player forward based on value
    /// </summary>
    public interface IBoostHandler
    {
        void Boost(float val);
    }

    /// <summary>
    /// Input interface encapsulating events required by other classes
    /// </summary>
    public interface IInputProvider
    {
        event Action<Vector3> OnRotate;
        event Action<float> OnMoveForward;
        event Action<float> OnMoveBackward;
        event Action<Vector3> OnMousePosition;
        event Action<float> OnBoost;
    }
}