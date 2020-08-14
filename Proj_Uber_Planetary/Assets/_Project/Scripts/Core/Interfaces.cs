using UnityEngine.Events;

namespace UberPlanetary.Core
{
    /// <summary>
    /// Finds the appropriate IEventValueProvider and exposes an event based on provided value
    /// </summary>
    public interface IEventExposer<T> where T : struct
    {
        void SetReference();
        void InvokeEvent();
        
        IEventValueProvider<T> EventValueProvider { get; set; }
    }

    /// <summary>
    /// Returns a value of type T when requested by a IEventExposer
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IEventValueProvider<T> where T : struct
    {
        T GetValue { get;}
        // T GetValue();
    }

    public interface ITakeDamage
    {
        void TakeDamage();
    }
}