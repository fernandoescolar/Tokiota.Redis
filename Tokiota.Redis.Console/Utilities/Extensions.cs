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
    }
}
