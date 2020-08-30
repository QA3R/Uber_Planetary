using UberPlanetary.Core;
using UnityEngine;

namespace UberPlanetary.Player.Movement
{
    /// Controls Cursor Icon's state based on provided input
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

        /// Clamp Cursor's Position inside Provided Rect
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

        /// Remaps Cursor's position to be between -1 and 1
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
        
        /// Get Component Reference from GameObject
        private void AssignComponents()
        {
            _inputHandler = FindObjectOfType<InputHandler>().GetComponent<IInputProvider>();
        }

        /// Assign methods to respective delegates
        private void AssignDelegates()
        {
            _inputHandler.OnMousePosition += MapCursorIcon;
        }

        /// UnAssign methods on Disable
        private void OnDisable()
        {
            _inputHandler.OnMousePosition -= MapCursorIcon;
        }
    }
}