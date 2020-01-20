using System.Linq;

namespace SoftServe.BookingSectors.WebAPI.BLL.Helpers
{
    /// <summary>
    /// Class for determining data during email verification 
    /// </summary>
    public static class EmailConfirmHelper
    {
        /// <summary>
        ///Get a hash string for verification
        /// </summary>
        /// <param name="id">int: user id</param>
        /// <param name="emailValid">bool: email validation state</param>
        /// <param name="email">string: email</param>
        /// <returns>hash string for verification</returns>
        public static string GetHash(int id, bool emailValid, string email)
        {
            string lineForHash = $"{id}#{emailValid.ToString()}#{email}";
            string firstHashPass = SHA256Hash.ComputeString(lineForHash);
            string reverseHash =  new string(firstHashPass.ToCharArray().Reverse().ToArray());
            string secondHashPass = SHA256Hash.ComputeString(reverseHash);

            return secondHashPass;
        }

        /// <summary>
        /// Hash Comparison
        /// </summary>
        /// <param name="inputHash">Received Hash</param>
        /// <param name="generatedHash">Generated Hash</param>
        /// <returns>True or False</returns>
        public static bool EqualHash(string inputHash, string generatedHash)
        {
            return inputHash.Equals(generatedHash);
        }

        /// <summary>
        /// Get Get confirmation email for user
        /// </summary>
        /// <param name="host">host: example: http://localhost:4200/ </param>
        /// <param name="email">user email</param>
        /// <param name="hash">hash data line</param>
        /// <returns>slink</returns>
        public static string GetLink(string host, string email, string hash)
        {
            return $"{host}/sign-in/{email}/{hash}";
        }
    }
}
