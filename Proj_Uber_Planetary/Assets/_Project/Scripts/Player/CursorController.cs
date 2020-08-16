using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UberPlanetary.Core;
using UnityEngine;
using UnityEngine.UI;

namespace UberPlanetary.Player
{
    /// <summary>
    /// Controls Cursor Icon's state based on provided input
    /// </summary>
    public class CursorController : MonoBehaviour, IEventValueProvider<Vector2>
    {
        //Private Members
        private IInputProvider _inputHandler;
        private Vector3 _cursorPos;
        private Vector2 _cursorAxis = Vector2.zero;
        private Vector2 _rawCursorAxis = Vector2.zero;
        private float _remappedX, _remappedY;
        
        //Assigned Rect in inspector
        [SerializeField] private RectTransform allowedAreaRect;
        [SerializeField] private RectTransform deadZoneRect;
        
        //Exposed Cursor position remapped to -1 to 1 range so it acts like an Axis
        public Vector2 CursorAxis => _cursorAxis;
        public Vector2 GetValue => _rawCursorAxis;

        private void Awake()
        {
            AssignComponents();
            AssignDelegates();
            SetCursorProperties();
        }

        private void SetCursorProperties()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.lockState = CursorLockMode.Confined;
        }

        /// <summary>
        /// Clamp Cursor's Position inside Provided Rect
        /// </summary>
        /// <param name="pos"></param>
        private void MapCursorIcon(Vector3 pos)
        {
            _cursorPos = pos;
            Rect newAllowedRect = allowedAreaRect.RectTransformToScreenSpace();
            Rect newDeadRect = deadZoneRect.RectTransformToScreenSpace();
            
            _cursorPos.x = Mathf.Clamp(_cursorPos.x,  newAllowedRect.xMin ,newAllowedRect.xMax );
            _cursorPos.y = Mathf.Clamp(_cursorPos.y, newAllowedRect.yMin ,newAllowedRect.yMax );
            
            _cursorPos.z = 0;
            
            transform.position = _cursorPos;
        
            TranslateCursorAxis(newAllowedRect, newDeadRect);
        }

        /// <summary>
        /// Remaps Cursor's position to be between -1 and 1
        /// </summary>
        /// <param name="allowedRect"></param>
        /// <param name="deadRect"></param>
        private void TranslateCursorAxis(Rect allowedRect, Rect deadRect)
        {
            _remappedX = _cursorPos.x;
            _remappedY = _cursorPos.y;
            
            _remappedX = _remappedX.Remap(allowedRect.xMin, allowedRect.xMax, -1f, 1f);
            _remappedY = _remappedY.Remap(allowedRect.yMin, allowedRect.yMax, -1f, 1f);

            _rawCursorAxis.x = _remappedX;
            _rawCursorAxis.y = _remappedY;
            
            if (_cursorPos.x.IsBetween(deadRect.xMin, deadRect.xMax))
            {
                _remappedX = 0;
            }
            if (_cursorPos.y.IsBetween(deadRect.yMin, deadRect.yMax))
            {
                _remappedY = 0;
            }
            
            _cursorAxis.x = _remappedX;
            _cursorAxis.y = _remappedY;
        }
        
        /// <summary>
        /// Get Component Reference from GameObject
        /// </summary>
        private void AssignComponents()
        {
            _inputHandler = GetComponentInParent<InputHandler>();
        }

        /// <summary>
        /// Assign methods to respective delegates
        /// </summary>
        private void AssignDelegates()
        {
            _inputHandler.MousePositionDelegate += MapCursorIcon;
        }

        /// <summary>
        /// UnAssign methods on Disable
        /// </summary>
        private void OnDisable()
        {
            _inputHandler.MousePositionDelegate -= MapCursorIcon;
        }

    }
}