using TOTPSystem.Util;
class Program
{
    static void Main()
    {
        string otp = OneTimePasswordGenerator.GenerateOTP();
        Console.WriteLine($"Generated OTP: {otp}");
    }
}