using System;

namespace LayuiTableGenerate.Classes
{
    public static class string_extension
    {
        public static string ToFirstLetterLower(this string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return str;
            }
            else
            {
                str = str.Substring(0, 1).ToLower() + str.Substring(1);    
            }
            return str;
        }
    }
}