using System;
using System.Collections.Generic;
using UnityEngine;

namespace UberPlanetary.Core
{
    /// Finds the appropriate IEventValueProvider and exposes an event based on provided value
    public interface IEventExposer<T> where T : struct
    {
        /// Find The reference to the EventValueProvider
        void SetReference();
        
        /// Call Event on Update
        void InvokeEvent();
        
        /// The Value provider
        IEventValueProvider<T> EventValueProvider { get; set; }
    }

    /// Returns a value of type T when requested by a IEventExposer
    public interface IEventValueProvider<out T> where T : struct
    {
        /// Return a value for events
        T GetValue { get;}
    }

    /// Damageable Interface
    public interface ITakeDamage
    {
        void TakeDamage();
    }

    /// Rotates object based on provided vector 3
    public interface IRotationHandler
    {
        //Rotate function
        void Rotate(Vector3 dir);
        ///Reduce Rotation multiplier
        void DampenRotation(float val);
    }

    /// Moves the object based on provided value
    public interface IMovementHandler
    {
        void MoveForward(float val);
        void MoveBackward(float val);
        void MoveSidewards(float val);
        void MoveVertical(float val);
        
    }

    /// Moves the player forward based on value
    public interface IBoostHandler
    {
        void Boost(float val);
    }

    /// Input interface encapsulating events required by other classes
    public interface IInputProvider
    {
        event Action<Vector3> OnRotate;
        event Action<float> OnMoveForward;
        event Action<float> OnMoveBackward;
        event Action<float> OnMoveVertical;
        event Action<float> OnMoveSideways;
        event Action<Vector3> OnMousePosition;
        event Action<float> OnBoost;
        event Action<float> OnScroll;
        Dictionary<KeyCode, ButtonEvent> ClickInfo {get;}
    }
    /// Interface for courses to communicate with checkpoints
    public interface ICheckPoint
    {
        void SetAsCurrent();
        void SetAsNext();

        Vector3 Position();
    }

    public interface IPhoneNavigator : IScrollHandler
    {
        IPhoneNavigable CurrentNavigable { get; }

        List<IPhoneNavigable> NavigableList { get; set; }
    }

    public interface IScrollHandler
    {
        void Scroll(float val);
    }
    
    public interface IPhoneNavigable
    {
        void Enter();
        void Exit();

        void Select();
        void Deselect();
    }

    public interface IPhoneApplication : IPhoneNavigable
    {
        //application specific functions
        //Like displaying notifications
        //Minimizing apps
        //Playing in background?
        //etc
        void DisplayNotification();
    }

    public interface IPhoneApplicationFeature : IPhoneNavigable
    {
        IPhoneNavigable ParentNavigable { get; set; }
        //Elements inside an application that can be interacted with
        //like a volume slider
        //a cross button to back out
        //Switching to different radio stations
        //toggling on car features
        //etc
    }
}