using UnityEngine;

namespace UberPlanetary.Core.ExtensionMethods
{
    public static class RectExtensionMethods
    {
        /// Translate a given points from UI Rect space to Screen Space
        public static Rect RectTransformToScreenSpace( this RectTransform inTransform )
        {
            Vector2 size = Vector2.Scale(inTransform.rect.size, inTransform.lossyScale);
            return new Rect((Vector2)inTransform.position - (size * 0.5f), size);
        }
    }
}