using System;

namespace SoftServe.BookingSectors.WebAPI.BLL.Helpers
{
    /// <summary>
    /// Static class for random number generation
    /// </summary>
    public static class RandomNumbers
    {
        /// <summary>
        /// Generate n-digits random number
        /// </summary>
        /// <param name="length">Length of random number</param>
        /// <returns>string Random numbers</returns>
        public static string Generate(int length = 6)
        {
            Random generator = new Random();

            if (length < 1)
            {
                return generator.Next(0, 999999).ToString("D6");
            }

            string maxValue = "";
            for (int i = 0; i < length; i++)
            {
                maxValue += "9";
            }

            string numbers = generator.Next(0, Convert.ToInt32(maxValue))
                                    .ToString($"D{length}");

            return numbers;
        }
    }
}