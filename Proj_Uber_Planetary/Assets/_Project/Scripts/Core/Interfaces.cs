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

    public interface ITakeDamage
    {
        void TakeDamage();
    }

    public interface IRotationHandler
    {
        void Rotate(Vector3 dir);
        void DampenRotation();
        void ResetRotation();
    }

    public interface IMovementHandler
    {
        void Move(float val);
    }

    public interface IBoostHandler
    {
        void Boost(float val);
    }

    public interface IInputProvider
    {
        event Action<Vector3> RotationDelegate;
        event Action<float> MovementDelegate;
        event Action<Vector3> MousePositionDelegate;
        event Action<float> BoostDelegate;
    }
}