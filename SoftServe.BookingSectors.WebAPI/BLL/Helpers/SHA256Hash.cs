using System.Text;
using System.Security.Cryptography;

namespace SoftServe.BookingSectors.WebAPI.BLL.Helpers
{
    /// <summary>
    /// Class for getting hash of string
    /// </summary>
    public static class SHA256Hash
    {
        /// <summary>
        /// Compute Hash, return byte[]
        /// </summary>
        /// <param name="value">value for hashing</param>
        /// <returns>hash in byte[]</returns>
        public static byte[] Compute(string value)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] hash = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(value));
                return hash;
            }
        }

        /// <summary>
        /// Compute Hash, return string value
        /// </summary>
        /// <param name="value">value for hashing</param>
        /// <returns>hash in string</returns>
        public static string ComputeString(string value)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytesHash = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(value));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytesHash.Length; i++)
                {
                    builder.Append(bytesHash[i].ToString("x2"));
                }

                return builder.ToString();
            }
        }
    }
}