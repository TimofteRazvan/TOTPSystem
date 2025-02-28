namespace TOTPSystem.Model
{
    public class OTPResponse
    {
        public string OTP { get; set; }
        public DateTime ExpirationTime { get; set; }

        public OTPResponse(string otp, DateTime expirationTime)
        {
            OTP = otp;
            ExpirationTime = expirationTime;
        }
    }
}
