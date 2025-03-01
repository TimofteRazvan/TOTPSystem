namespace TOTPSystem.Model
{
    /// <summary>
    /// Represents a request to validate an OTP.
    /// </summary>
    public class OTPRequest
    {
        public string OTP { get; set; }

        public OTPRequest(string otp)
        {
            OTP = otp;
        }
    }
}
