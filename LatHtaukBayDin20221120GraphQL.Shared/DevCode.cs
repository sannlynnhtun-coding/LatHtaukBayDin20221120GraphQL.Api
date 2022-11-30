using System.Security.Cryptography;
using System.Text;

namespace LatHtaukBayDin20221120GraphQL.Shared
{
    public static class DevCode
    {
        public static string ToHashPassword(this string input)
        {
            var algorithm = new SHA256CryptoServiceProvider();
            Byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            Byte[] hashedBytes = algorithm.ComputeHash(inputBytes);

            //return BitConverter.ToString(hashedBytes);
            return ToHex(hashedBytes, false);
        }

        public static string ToHex(byte[] bytes, bool upperCase)
        {
            StringBuilder result = new StringBuilder(bytes.Length * 2);
            for (int i = 0; i < bytes.Length; i++)
                result.Append(bytes[i].ToString(upperCase ? "X2" : "x2"));
            return result.ToString();
        }
    }
}