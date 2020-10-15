using System;
using System.Collections.Generic;
using UnityEngine;

namespace UberPlanetary.Core.Interfaces
{
    public interface IScrollHandler
    {
        void Scroll(float val);
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

        Dictionary<KeyCode, ButtonEvent> ClickInfo { get; }
    }
}