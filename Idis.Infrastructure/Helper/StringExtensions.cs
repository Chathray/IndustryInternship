using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Idis.Infrastructure.Helper
{
    /// <summary>
    /// Helpers
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// ConcatParameters
        /// </summary>
        /// <param name="strConcat"></param>
        /// <param name="value1"></param>
        /// <returns></returns>
        public static string ConcatParameters(this string strConcat, string value1)
        {
            return string.Concat(strConcat, value1);
        }

        /// <summary>
        /// ConcatTwoParameters
        /// </summary>
        /// <param name="strConcat"></param>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns></returns>
        public static string ConcatTwoParameters(this string strConcat, string value1, string value2)
        {
            return string.Concat(strConcat, value1, value2);
        }

        /// <summary>
        /// ConcatThreeParametersSort
        /// </summary>
        /// <param name="strConcat"></param>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <param name="value3"></param>
        /// <returns></returns>
        public static string ConcatThreeParameters(this string strConcat, string value1, string value2, string value3)
        {
            return string.Concat(strConcat, value1, value2, value3);
        }

        /// <summary>
        /// ReplaceSpecialCharacter
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ReplaceSpecialCharacter(this string value)
        {
            value = value.Replace("'", "\\'");
            return string.IsNullOrEmpty(value) ? null : value;
        }
        public static bool ConvertValueStringToBool(string strBool)
        {
            switch (strBool)
            {
                case "0":
                    return false;
                case "1":
                    return true;
                default:
                    return false;
            }
        }
        public static int ConvertToInt32(string inputStr)
        {
            return string.IsNullOrEmpty(inputStr) ? 0 : Convert.ToInt32(inputStr);
        }
        public static DateTime? ConvertToDateTime(string inputStr)
        {
            if (!string.IsNullOrEmpty(inputStr))
            {
                DateTime? dt = Convert.ToDateTime(inputStr);
                return dt;
            }
            else
                return null;
        }

        /// <summary>
        /// Replace current value to new value if null
        /// </summary>
        /// <param name="currentValue"></param>
        /// <param name="newValue"></param>
        /// <returns></returns>
        public static object ReplaceIfNull(this object currentValue, object newValue)
        {
            if (newValue == null) return currentValue;

            // if currentValue is null and newValue has value
            // update currentValue to newValue

            if (currentValue == null) return newValue;

            return currentValue;
        }

        /// <summary>
        /// Convert string to float
        /// </summary>
        /// <param name="inputStr">String put string</param>
        /// <returns></returns>
        public static float? ConvertToFloat(string inputStr)
        {
            return string.IsNullOrEmpty(inputStr) ? null : (float?)Convert.ToDouble(inputStr);
        }

        /// <summary>
        /// Convert String to MemoryStream
        /// </summary>
        /// <param name="value">input string</param>
        /// <returns></returns>
        public static MemoryStream GenerateStreamFromString(string value)
        {
            return new MemoryStream(Encoding.UTF8.GetBytes(value ?? ""));
        }
    }
}
