using OTPSystem.Service;
using Xunit;

namespace TOTPSystem.Test
{
    public class OTPServiceTests
    {
        private readonly OTPService _otpService;

        public OTPServiceTests()
        {
            _otpService = new OTPService();
        }

        [Fact]
        public void GenerateOTPResponse_ShouldReturnValidOTP()
        {
            string sessionId = "1";
            var otpResponse = _otpService.GenerateOTPResponse(sessionId);
            Assert.NotNull(otpResponse);
            Assert.NotNull(otpResponse.OTP);
            Assert.Equal(6, otpResponse.OTP.Length);
            Assert.InRange(otpResponse.ExpirationTime, DateTime.UtcNow, DateTime.UtcNow.AddSeconds(60));
        }

        [Fact]
        public void ValidateOTP_ShouldAcceptValidOTP()
        {
            string sessionId = "1";
            var otpResponse = _otpService.GenerateOTPResponse(sessionId);
            bool isValid = _otpService.ValidateOTP(sessionId, otpResponse.OTP);
            Assert.True(isValid);
        }

        [Fact]
        public void ValidateOTP_ShouldRejectExpiredOTP()
        {
            string sessionId = "1";
            var otpResponse = _otpService.GenerateOTPResponse(sessionId);
            otpResponse.ExpirationTime = DateTime.UtcNow.AddSeconds(-1);
            bool isValid = _otpService.ValidateOTP(sessionId, otpResponse.OTP);
            Assert.False(isValid);
        }

        [Fact]
        public void ValidateOTP_ShouldRejectIncorrectOTP()
        {
            string sessionId = "1";
            _otpService.GenerateOTPResponse(sessionId);
            bool isValid = _otpService.ValidateOTP(sessionId, "1234QW");
            Assert.False(isValid);
        }

        [Fact]
        public void ValidateOTP_ShouldRejectNonExistentOTP()
        {
            string sessionId = "1";
            bool isValid = _otpService.ValidateOTP(sessionId, "7A3B9X");
            Assert.False(isValid);
        }

        [Fact]
        public void ValidateOTP_ShouldRejectEmptyOrNullOTP()
        {
            string sessionId = "1";
            Assert.False(_otpService.ValidateOTP(sessionId, null!));
            Assert.False(_otpService.ValidateOTP(sessionId, ""));
            Assert.False(_otpService.ValidateOTP(sessionId, "   "));
        }

        [Fact]
        public void ValidateOTP_ShouldRejectInvalidOTPFormat()
        {
            string sessionId = "1";
            Assert.False(_otpService.ValidateOTP(sessionId, "123"));
            Assert.False(_otpService.ValidateOTP(sessionId, "1234567"));
            Assert.False(_otpService.ValidateOTP(sessionId, "123abc"));
            Assert.False(_otpService.ValidateOTP(sessionId, "!@#$%^"));
        }
    }
}
