using OTPSystem.Service;
class Program
{
    static void Main()
    {
        var otpService = new OTPService();
        string sessionId = "session123";
        var otpResponse = otpService.GenerateOTPResponse(sessionId);
        Console.WriteLine($"OTP: {otpResponse.OTP}, Expires: {otpResponse.ExpirationTime}");

        bool isValid = otpService.ValidateOTP(sessionId, otpResponse.OTP);
        Console.WriteLine($"Is OTP valid? {isValid}");

        bool isValid1 = otpService.ValidateOTP(sessionId, "1234XL");
        Console.WriteLine($"Is OTP valid? {isValid1}");
    }
}