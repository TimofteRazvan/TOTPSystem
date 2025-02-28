using OTPSystem.Service;
using Xunit;

namespace TOTPSystem.Test
{
    public class OTPServiceTests
    {
        private readonly OTPService _otpService;
        private const string SessionID = "1";
        private const string OTPValue = "1234QW";

        public OTPServiceTests()
        {
            _otpService = new OTPService();
        }

        [Fact]
        public void GenerateOTPResponse_ShouldReturnValidOTP()
        {
            string sessionID = SessionID;
            var otpResponse = _otpService.GenerateOTPResponse(sessionID);
            Assert.NotNull(otpResponse);
            Assert.NotNull(otpResponse.OTP);
            Assert.Equal(6, otpResponse.OTP.Length);
            Assert.InRange(otpResponse.ExpirationTime, DateTime.UtcNow, DateTime.UtcNow.AddSeconds(60));
        }

        [Fact]
        public void ValidateOTP_ShouldAcceptValidOTP()
        {
            string sessionID = SessionID;
            var otpResponse = _otpService.GenerateOTPResponse(sessionID);
            bool isValid = _otpService.ValidateOTP(sessionID, otpResponse.OTP);
            Assert.True(isValid);
        }

        [Fact]
        public void ValidateOTP_ShouldRejectExpiredOTP()
        {
            string sessionID = SessionID;
            var otpResponse = _otpService.GenerateOTPResponse(sessionID);
            otpResponse.ExpirationTime = DateTime.UtcNow.AddSeconds(-1);
            bool isValid = _otpService.ValidateOTP(sessionID, otpResponse.OTP);
            Assert.False(isValid);
        }

        [Fact]
        public void ValidateOTP_ShouldRejectIncorrectOTP()
        {
            string sessionID = SessionID;
            _otpService.GenerateOTPResponse(sessionID);
            bool isValid = _otpService.ValidateOTP(sessionID, OTPValue);
            Assert.False(isValid);
        }

        [Fact]
        public void ValidateOTP_ShouldRejectNonExistentOTP()
        {
            string sessionID = SessionID;
            bool isValid = _otpService.ValidateOTP(sessionID, OTPValue);
            Assert.False(isValid);
        }

        [Fact]
        public void ValidateOTP_ShouldRejectEmptyOrNullOTP()
        {
            string sessionID = SessionID;
            Assert.False(_otpService.ValidateOTP(sessionID, null!));
            Assert.False(_otpService.ValidateOTP(sessionID, ""));
            Assert.False(_otpService.ValidateOTP(sessionID, "   "));
        }

        [Fact]
        public void ValidateOTP_ShouldRejectInvalidOTPFormat()
        {
            string sessionID = SessionID;
            Assert.False(_otpService.ValidateOTP(sessionID, "123"));
            Assert.False(_otpService.ValidateOTP(sessionID, "1234567"));
            Assert.False(_otpService.ValidateOTP(sessionID, "123abc"));
            Assert.False(_otpService.ValidateOTP(sessionID, "!@#$%^"));
        }
    }
}
