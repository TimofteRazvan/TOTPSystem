using System.Security.Cryptography;

namespace TOTPSystem.Util
{
    class OneTimePasswordGenerator
    {
        private const int Length = 6;
        private const string ValidChars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        /// <summary>
        /// Generates a cryptographically secure one-time password (OTP).
        /// </summary>
        /// <returns>A randomly generated OTP.</returns>
        public static string GenerateOTP()
        {
            var passwordChars = new char[Length];
            var randomBytes = new byte[Length];
            RandomNumberGenerator.Fill(randomBytes);
            for (int i = 0; i < Length; i++)
            {
                passwordChars[i] = ValidChars[randomBytes[i] % ValidChars.Length];
            }
            return new string(passwordChars);
        }
    }
}
