﻿using Microsoft.AspNetCore.Mvc;
using OTPSystem.Service;
using TOTPSystem.Model;

namespace TOTPSystem.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class OTPController : ControllerBase
    {
        private readonly IOTPService _otpService;

        public OTPController(IOTPService otpService)
        {
            _otpService = otpService;
        }

        /// <summary>
        /// Generates an OTP for the given session ID.
        /// </summary>
        /// <param name="sessionID">The session ID for the OTP.</param>
        /// <returns>An OTP response that contains both the OTP and its expiration time.</returns>
        // TODO: Change lifetime to last longer after finished!
        [HttpGet("generate")]
        public IActionResult GenerateOTP([FromQuery] string sessionID)
        {
            if (string.IsNullOrWhiteSpace(sessionID))
            {
                return BadRequest("Bad session. ID cannot be empty.");
            }

            var otpResponse = _otpService.GenerateOTPResponse(sessionID);
            return Ok(otpResponse);
        }

        /// <summary>
        /// Validates the provided OTP for the given session.
        /// </summary>
        /// <param name="request">The OTP validation request that contains both the session ID and OTP.</param>
        /// <returns>A response indicating whether the OTP is valid or not.</returns>
        [HttpPost("validate")]
        public IActionResult ValidateOTP([FromBody] OTPRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.SessionID) || string.IsNullOrWhiteSpace(request.OTP))
            {
                return BadRequest("Bad data. Session ID and OTP are required.");
            }

            bool isValid = _otpService.ValidateOTP(request.SessionID, request.OTP);
            if (!isValid)
            {
                return BadRequest("OTP is invalid or expired.");
            }
            return Ok("OTP is valid.");
        }
    }
}
