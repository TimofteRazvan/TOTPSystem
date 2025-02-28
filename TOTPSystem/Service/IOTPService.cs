using TOTPSystem.Model;

namespace OTPSystem.Service
{
    /// <summary>
    /// The IOTPService interface defines the skeleton for OTPService.
    /// </summary>
    public interface IOTPService
    {
        OTPResponse GenerateOTPResponse(string sessionID);
        bool ValidateOTP(string sessionID, string otp);
    }
}