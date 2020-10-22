using UnityEngine;

namespace UberPlanetary.Core.ExtensionMethods
{
    public static class FloatExtensionMethods
    {
        /// Returns a value mapped from the current min max to the provided min max
        public static float Remap( this float value, float iMin, float iMax, float oMin, float oMax )
        {
            var t = Mathf.InverseLerp( iMin, iMax, value );
            return Mathf.Lerp( oMin, oMax, t );
        }
        
        /// Returns true if the value is between the min and max
        public static bool IsBetween( this float value, float min, float max )
        {
            return ( value >= Mathf.Min( min,max ) && value <= Mathf.Max( min,max ) );
        }
        
        public static float Repeat(this float value, float length ) => Mathf.Clamp( value - Mathf.Floor( value / length ) * length, 0.0f, length );
    }
}
