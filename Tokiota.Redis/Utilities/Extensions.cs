using System;
using System.ComponentModel;
using System.Globalization;
using System.Text;

namespace Tokiota.Redis.Utilities
{
    internal static class Extensions
    {
        public static byte[] ToByteArray(this string str)
        {
            var numArray = new byte[str.Length];
            for (var index = 0; index < str.Length; ++index)
            {
                numArray[index] = (byte)str[index];
            }

            return numArray;
        }

        public static byte[] ToByteArray(this int number)
        {
            return ToByteArray(number.ToString());
        }

        public static byte[] ToByteArray(this long number)
        {
            return ToByteArray(number.ToString());
        }

        public static byte[] ToByteArray(this double number)
        {
            var str = number.ToString((IFormatProvider)CultureInfo.InvariantCulture.NumberFormat);
            return str.ToByteArray();
        }

        public static byte[][] ToByteArrays(this string[] strs)
        {
            var bArray = new byte[strs.Length][];
            for (int index = 0; index < strs.Length; ++index)
                bArray[index] = strs[index].ToByteArray();

            return bArray;
        }

        public static string ToUtf8String(this byte[] bytes)
        {
            return Encoding.UTF8.GetString(bytes, 0, bytes.Length);
        }

        public static string[] ToUtf8Strings(this byte[][] bytes)
        {
            var strArray = new string[bytes.Length];
            for (int index = 0; index < bytes.Length; ++index)
                strArray[index] = bytes[index].ToUtf8String();

            return strArray;
        }

        public static byte[][] Merge(this byte[] cmd, params byte[][] args)
        {
            var numArray = new byte[1 + args.Length][];
            numArray[0] = cmd;
            for (int index = 0; index < args.Length; ++index)
                numArray[index + 1] = args[index];

            return numArray;
        }

        public static byte[][] Combine(this byte[] cmd, byte[][] keys, byte[][] values)
        {
            if (keys.Length != values.Length)
                throw new ArgumentException("The lenght of 'keys' and 'values' should be the same");

            var numArray = new byte[1 + keys.Length + values.Length][];
            var index = 0;
            numArray[index++] = cmd;
            for (int i = 0; i < keys.Length; ++i)
            {
                numArray[index++] = keys[i];
                numArray[index++] = values[i];
            }
                

            return numArray;
        }

        public static byte[][] Merge(this byte[] cmd, params string[] args)
        {
            var numArray = new byte[1 + args.Length][];
            numArray[0] = cmd;
            for (int index = 0; index < args.Length; ++index)
                numArray[index + 1] = args[index].ToByteArray();

            return numArray;
        }
    }
}
