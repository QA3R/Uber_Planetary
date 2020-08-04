using System;
using UnityEngine;
using UnityEngine.UI;

public class CursorController : MonoBehaviour
{
    private InputHandler _inputHandler;
    private Vector3 _cursorPos;
    [SerializeField] private RectTransform allowedAreaRect;

    private Vector2 _cursorAxis = Vector2.zero;

    public Vector2 CursorAxis => _cursorAxis;

    private void Awake()
    {
        _inputHandler = GetComponentInParent<InputHandler>();
        _inputHandler.mousePositionDelegate += MapCursorIcon;
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
    }
    

    private void MapCursorIcon(Vector3 pos)
    {
        _cursorPos = pos;
        Rect boop = RectTransformToScreenSpace(allowedAreaRect);
        _cursorPos.x = Mathf.Clamp(_cursorPos.x,  boop.xMin ,boop.xMax );
        _cursorPos.y = Mathf.Clamp(_cursorPos.y, boop.yMin ,boop.yMax );
        transform.position = _cursorPos;
        
        float x = _cursorPos.x;
        float y = _cursorPos.y;
        
        x = x.Remap(boop.xMin, boop.xMax, -1f, 1f);
        y = y.Remap(boop.yMin, boop.yMax, -1f, 1f);

        _cursorAxis.x = x;
        _cursorAxis.y = y;
    }
    
    public static Rect RectTransformToScreenSpace(RectTransform transform)
    {
        Vector2 size = Vector2.Scale(transform.rect.size, transform.lossyScale);
        return new Rect((Vector2)transform.position - (size * 0.5f), size);
    }
}