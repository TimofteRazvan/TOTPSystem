using TOTPSystem.Model;
using TOTPSystem.Util;

namespace OTPSystem.Service
{
    public class OTPService : IOTPService
    {
        private readonly TimeSpan _otpDuration = TimeSpan.FromSeconds(5);
        private readonly Dictionary<string, OTPResponse> _otpCache = new Dictionary<string, OTPResponse>();

        /// <summary>
        /// Generates a new OTP along with its expiration time and stores it in memory.
        /// </summary>
        /// <param name="sessionID">The session ID associated with the OTP.</param>
        /// <returns>An OTPResponse containing the OTP and its expiration time.</returns>
        public OTPResponse GenerateOTPResponse(string sessionID)
        {
            var otp = OneTimePasswordGenerator.GenerateOTP();
            var expirationTime = DateTime.UtcNow.Add(_otpDuration);
            _otpCache[sessionID] = new OTPResponse(otp, expirationTime);
            return _otpCache[sessionID];
        }

        /// <summary>
        /// Validates the provided OTP against its expiration time and stored value.
        /// </summary>
        /// <param name="sessionID">The session ID associated with the OTP.</param>
        /// <param name="otp">The OTP to validate.</param>
        /// <returns>True if the OTP is valid; otherwise, false.</returns>
        public bool ValidateOTP(string sessionID, string otp)
        {
            if (string.IsNullOrWhiteSpace(otp))
            {
                return false;
            }

            if (!_otpCache.TryGetValue(sessionID, out OTPResponse? otpResponse) || otpResponse == null)
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(otpResponse.OTP))
            {
                return false;
            }

            if (DateTime.UtcNow > otpResponse.ExpirationTime)
            {
                return false;
            }

            return otp == otpResponse.OTP;
        }
    }
}