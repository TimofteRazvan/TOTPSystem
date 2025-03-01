using Microsoft.AspNetCore.Mvc;
using Moq;
using OTPSystem.Service;
using System.Text;
using TOTPSystem.Controller;
using TOTPSystem.Model;
using TOTPSystem.Test.Mocks;
using Xunit;

namespace TOTPSystem.Test
{
    public class OTPControllerTests
    {
        private readonly Mock<IOTPService> _otpServiceMock;
        private readonly OTPController _controller;
        private readonly DefaultHttpContext _httpContext;
        private readonly MockSession _mockSession;
        private const string SessionID = "1";
        private const string OTPValue = "1234QW";
        private const string InvalidOTPValue = "INVALID";

        public OTPControllerTests()
        {
            _otpServiceMock = new Mock<IOTPService>();
            _mockSession = new MockSession();
            _httpContext = new DefaultHttpContext();
            _httpContext.Session = _mockSession;

            _controller = new OTPController(_otpServiceMock.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = _httpContext
                }
            };
        }

        [Fact]
        public void GenerateOTP_ShouldReturnOTPResponse()
        {
            var expectedOtpResponse = new OTPResponse(OTPValue, DateTime.UtcNow.AddMinutes(5));
            _otpServiceMock.Setup(s => s.GenerateOTPResponse(It.IsAny<string>())).Returns(expectedOtpResponse);
            var result = _controller.GenerateOTP();
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<OTPResponse>(okResult.Value);

            Assert.Equal(OTPValue, response.OTP);
            var sessionID = _mockSession.GetString("SessionID");
            Assert.False(string.IsNullOrEmpty(sessionID));
        }

        [Fact]
        public void ValidateOTP_WithValidOTP_ShouldReturnOk()
        {
            _mockSession.SetString("SessionID", SessionID);
            var request = new OTPRequest(OTPValue);
            _otpServiceMock.Setup(s => s.ValidateOTP(SessionID, OTPValue)).Returns(true);
            var result = _controller.ValidateOTP(request);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = okResult.Value as dynamic;
            Assert.Equal("OTP is valid.", response?.message);
        }

        [Fact]
        public void ValidateOTP_WithInvalidOTP_ShouldReturnBadRequest()
        {
            _mockSession.SetString("SessionID", SessionID);
            var request = new OTPRequest(InvalidOTPValue);
            _otpServiceMock.Setup(s => s.ValidateOTP(SessionID, InvalidOTPValue)).Returns(false);
            var result = _controller.ValidateOTP(request);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var response = badRequestResult.Value as dynamic;
            Assert.Equal("OTP is invalid or expired.", response?.error);
        }

        [Fact]
        public void ValidateOTP_NoSession_ShouldReturnBadRequest()
        {
            var request = new OTPRequest(OTPValue);
            var result = _controller.ValidateOTP(request);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var response = badRequestResult.Value as dynamic;
            Assert.Equal("No active session found.", response?.error);
        }

        [Fact]
        public void ValidateOTP_ExpiredOTP_ShouldReturnBadRequest1()
        {
            _mockSession.SetString("SessionID", SessionID);
            var expiredOtpResponse = new OTPResponse(OTPValue, DateTime.UtcNow.AddMinutes(-1));
            _otpServiceMock.Setup(s => s.ValidateOTP(SessionID, OTPValue)).Returns(false);
            var result = _controller.ValidateOTP(new OTPRequest(OTPValue));

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var response = badRequestResult.Value as dynamic;
            Assert.Equal("OTP is invalid or expired.", response?.error);
        }
    }
}


