using Microsoft.AspNetCore.Mvc;
using Moq;
using OTPSystem.Service;
using TOTPSystem.Controller;
using TOTPSystem.Model;
using Xunit;

namespace TOTPSystem.Test
{
    public class OTPControllerTests
    {
        private readonly Mock<IOTPService> _mockOTPService;
        private readonly OTPController _otpController;
        private const string SessionID = "1";
        private const string OTPValue = "1234QW";

        public OTPControllerTests()
        {
            _mockOTPService = new Mock<IOTPService>();
            _otpController = new OTPController(_mockOTPService.Object);
        }

        [Fact]
        public void GenerateOTP_ShouldReturnOTPResponse()
        {
            string sessionID = SessionID;
            var otpResponse = new OTPResponse(OTPValue, DateTime.UtcNow.AddMinutes(1));
            _mockOTPService.Setup(x => x.GenerateOTPResponse(sessionID)).Returns(otpResponse);
            var result = _otpController.GenerateOTP(sessionID);
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(otpResponse, okResult.Value);
        }

        [Fact]
        public void ValidateOTP_ShouldReturnOk_ForValidOTP()
        {
            var request = new OTPRequest(SessionID, OTPValue);
            _mockOTPService.Setup(x => x.ValidateOTP(request.SessionID, request.OTP)).Returns(true);
            var result = _otpController.ValidateOTP(request);
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("OTP is valid.", okResult.Value);
        }

        [Fact]
        public void ValidateOTP_ShouldReturnBadRequest_ForInvalidOTP()
        {
            var request = new OTPRequest(SessionID, OTPValue);
            _mockOTPService.Setup(x => x.ValidateOTP(request.SessionID, request.OTP)).Returns(false);
            var result = _otpController.ValidateOTP(request);
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("OTP is invalid or expired.", badRequestResult.Value);
        }
    }
}
