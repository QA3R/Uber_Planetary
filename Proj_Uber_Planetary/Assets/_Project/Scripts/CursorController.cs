using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using UnityEngine.UI;

namespace UberPlanetary
{
    /// <summary>
    /// Controls Cursor Icon's state based on provided input
    /// </summary>
    public class CursorController : MonoBehaviour
    {
        //Private Members
        private InputHandler _inputHandler;
        private Vector3 _cursorPos;
        private Vector2 _cursorAxis = Vector2.zero;
        private Image _cursorIcon;
        private float _remappedX, _remappedY;
    
        //Assigned Rect in inspector
        [SerializeField] private RectTransform allowedAreaRect;
    
        //Exposed Cursor position remapped to -1 to 1 range so it acts like an Axis
        public Vector2 CursorAxis => _cursorAxis;
    
        private void Awake()
        {
            AssignComponents();
            AssignDelegates();
        }
    
        /// <summary>
        /// Clamp Cursor's Position inside Provided Rect
        /// </summary>
        /// <param name="pos"></param>
        private void MapCursorIcon(Vector3 pos)
        {
            _cursorPos = pos;
            Rect boop = RectTransformToScreenSpace(allowedAreaRect);
        
            _cursorPos.x = Mathf.Clamp(_cursorPos.x,  boop.xMin ,boop.xMax );
            _cursorPos.y = Mathf.Clamp(_cursorPos.y, boop.yMin ,boop.yMax );
            _cursorPos.z = 0;
            
            transform.position = _cursorPos;
        
            TranslateCursorAxis(boop);
            SetCursorAlpha();
        }

        /// <summary>
        /// Remaps Cursor's position to be between -1 and 1
        /// </summary>
        /// <param name="boop"></param>
        private void TranslateCursorAxis(Rect boop)
        {
            _remappedX = _cursorPos.x;
            _remappedY = _cursorPos.y;

            _remappedX = _remappedX.Remap(boop.xMin, boop.xMax, -1f, 1f);
            _remappedY = _remappedY.Remap(boop.yMin, boop.yMax, -1f, 1f);

            _cursorAxis.x = _remappedX;
            _cursorAxis.y = _remappedY;
        }

        /// <summary>
        /// Set Cursor's Alpha based on the current Cursor's Axis (position 0-1)
        /// </summary>
        private void SetCursorAlpha()
        {
            var material = _cursorIcon.material;
            Color tmp = material.color;
            tmp.a = _cursorAxis.magnitude;
            material.color = tmp;
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