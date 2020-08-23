using UnityEngine;

namespace UberPlanetary.Core
{
    public static class FloatExtensionMethods
    {
        /// <summary>
        /// Returns a value mapped from the current min max to the provided min max
        /// </summary>
        /// <param name="value"></param>
        /// <param name="iMin"></param>
        /// <param name="iMax"></param>
        /// <param name="oMin"></param>
        /// <param name="oMax"></param>
        /// <returns></returns>
        public static float Remap( this float value, float iMin, float iMax, float oMin, float oMax)
        {
            float t = Mathf.InverseLerp(iMin, iMax, value);
            return Mathf.Lerp(oMin, oMax, t);
        }
        
        /// <summary>
        /// Returns true if the value is between the min and max
        /// </summary>
        /// <param name="value"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static bool IsBetween(this float value, float min, float max)
        {
            return (value >= Mathf.Min(min,max) && value <= Mathf.Max(min,max));
        }
    }

    public static class RectTransformExtensionMethods
    {
        /// <summary>
        /// Translate a given points from UI Rect space to Screen Space
        /// </summary>
        /// <param name="inTransform"></param>
        /// <returns></returns>
        public static Rect RectTransformToScreenSpace(this RectTransform inTransform)
        {
            Vector2 size = Vector2.Scale(inTransform.rect.size, inTransform.lossyScale);
            return new Rect((Vector2)inTransform.position - (size * 0.5f), size);
        }
    }
}