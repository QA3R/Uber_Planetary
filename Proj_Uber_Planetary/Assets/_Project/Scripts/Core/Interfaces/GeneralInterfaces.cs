using UnityEngine;

namespace UberPlanetary.Core.Interfaces
{
    public interface IListElement
    {
        void Add();
        void Remove();
    }
    
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

    /// Interface for courses to communicate with checkpoints
    public interface ICheckPoint
    {
        void SetAsCurrent();
        void SetAsNext();

        Vector3 Position();
    }
}