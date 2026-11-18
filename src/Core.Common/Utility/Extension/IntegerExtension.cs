namespace Core.Common.Utility.Extension
{
    public static class IntegerExtension
    {

        public static string ToPadEightZero(this int source)
        {
            return source.ToString().PadLeft(8, '0');
        }
         
    }
}