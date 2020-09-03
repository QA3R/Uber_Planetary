using UnityEngine;

namespace UberPlanetary.Core
{
    public static class FloatExtensionMethods
    {
        /// Returns a value mapped from the current min max to the provided min max
        public static float Remap( this float value, float iMin, float iMax, float oMin, float oMax)
        {
            var t = Mathf.InverseLerp(iMin, iMax, value);
            return Mathf.Lerp(oMin, oMax, t);
        }
        
        /// Returns true if the value is between the min and max
        public static bool IsBetween(this float value, float min, float max)
        {
            return (value >= Mathf.Min(min,max) && value <= Mathf.Max(min,max));
        }
    }

    public static class IntExtensionMethods
    {
        public static int Mod( this int value, int length ) => ( value % length + length ) % length; // modulo
    }

    public static class RectExtensionMethods
    {
        /// Translate a given points from UI Rect space to Screen Space
        public static Rect RectTransformToScreenSpace(this RectTransform inTransform)
        {
            Vector2 size = Vector2.Scale(inTransform.rect.size, inTransform.lossyScale);
            return new Rect((Vector2)inTransform.position - (size * 0.5f), size);
        }
    }
}