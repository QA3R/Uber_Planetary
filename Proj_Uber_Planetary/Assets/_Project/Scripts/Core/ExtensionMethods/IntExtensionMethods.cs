namespace UberPlanetary.Core.ExtensionMethods
{
    public static class IntExtensionMethods
    { 
        public static int Mod( this int value, int length ) => ( value % length + length ) % length; // modulo
    }
}