namespace TOTPSystem.Model
{
    /// <summary>
    /// Represents a request to validate an OTP.
    /// </summary>
    public class OTPRequest
    {
        public string SessionID { get; set; }
        public string OTP { get; set; }

        public OTPRequest(string sessionID, string otp)
        {
            SessionID = sessionID;
            OTP = otp;
        }
    }
}
