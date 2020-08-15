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
        private InputHandler _inputHandler;
        private Vector3 _cursorPos;
        private Vector2 _cursorAxis = Vector2.zero;
        private Vector2 _rawCursorAxis = Vector2.zero;
        private Image _cursorIcon;
        private float _remappedX, _remappedY;
        private List<Image> _childCursorImages = new List<Image>();
        
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
            for (int i = 0; i < transform.childCount; i++)
            {
                _childCursorImages.Add(transform.GetChild(i).GetComponent<Image>());
            }

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
            Rect newAllowedRect = RectTransformToScreenSpace(allowedAreaRect);
            Rect newDeadRect = RectTransformToScreenSpace(deadZoneRect);
            
            _cursorPos.x = Mathf.Clamp(_cursorPos.x,  newAllowedRect.xMin ,newAllowedRect.xMax );
            _cursorPos.y = Mathf.Clamp(_cursorPos.y, newAllowedRect.yMin ,newAllowedRect.yMax );
            
            _cursorPos.z = 0;
            
            transform.position = _cursorPos;
        
            TranslateCursorAxis(newAllowedRect, newDeadRect);
            SetCursorAlpha();
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
            
            if (IsBetween(_cursorPos.x, deadRect.xMin, deadRect.xMax))
            {
                _remappedX = 0;
            }
            if (IsBetween(_cursorPos.y, deadRect.yMin, deadRect.yMax))
            {
                _remappedY = 0;
            }
            
            _cursorAxis.x = _remappedX;
            _cursorAxis.y = _remappedY;
        }

        public bool IsBetween(float val, float min, float max)
        {
            return (val >= Mathf.Min(min,max) && val <= Mathf.Max(min,max));
        }
        
        /// <summary>
        /// Set Cursor's Alpha based on the current Cursor's Axis (position 0-1)
        /// </summary>
        public void SetCursorAlpha()
        {
            for (int i = 0; i < _childCursorImages.Count; i++)
            {
                var material = _childCursorImages[i];
                Color tmp = material.color;
                tmp.a = Mathf.Clamp(_cursorAxis.magnitude, .05f, 1f);
                material.color = tmp;
            }
        }

        /// <summary>
        /// Translate a given points from UI Rect space to Screen Space
        /// </summary>
        /// <param name="inTransform"></param>
        /// <returns></returns>
        private Rect RectTransformToScreenSpace(RectTransform inTransform)
        {
            Vector2 size = Vector2.Scale(inTransform.rect.size, inTransform.lossyScale);
            return new Rect((Vector2)inTransform.position - (size * 0.5f), size);
        }
    
        /// <summary>
        /// Get Component Reference from GameObject
        /// </summary>
        private void AssignComponents()
        {
            _cursorIcon = GetComponent<Image>();
            _inputHandler = GetComponentInParent<InputHandler>();
        }

        /// <summary>
        /// Assign methods to respective delegates
        /// </summary>
        private void AssignDelegates()
        {
            _inputHandler.mousePositionDelegate += MapCursorIcon;
        }

        /// <summary>
        /// UnAssign methods on Disable
        /// </summary>
        [SuppressMessage("ReSharper", "DelegateSubtraction")]
        private void OnDisable()
        {
            if (_inputHandler.mousePositionDelegate != null) _inputHandler.mousePositionDelegate -= MapCursorIcon;
        }

    }
}