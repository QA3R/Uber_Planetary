using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UberPlanetary.Player
{
    /// Populates the list of cursor images and exposes a function to set their alpha.
    public class SetCursorAlpha : MonoBehaviour
    {
        private readonly List<Image> _childCursorImages = new List<Image>();
        
        private void Start()
        {
            PopulateList();
        }

        private void PopulateList()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                _childCursorImages.Add(transform.GetChild(i).GetComponent<Image>());
            }
        }

        /// Set Cursor's Alpha based on the current Cursor's Axis (position 0-1)
        public void SetAlpha(Vector2 val)
        {
            for (int i = 0; i < _childCursorImages.Count; i++)
            {
                var material = _childCursorImages[i];
                Color tmp = material.color;
                tmp.a = Mathf.Clamp(val.magnitude, .05f, 1f);
                material.color = tmp;
            }
        }
    }
}