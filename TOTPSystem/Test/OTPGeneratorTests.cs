using TOTPSystem.Util;
using Xunit;

namespace TOTPSystem.Test
{
    public class OneTimePasswordGeneratorTests
    {
        [Fact]
        public void GenerateOTP_GeneratesUniqueOTPs()
        {
            var otp1 = OneTimePasswordGenerator.GenerateOTP();
            var otp2 = OneTimePasswordGenerator.GenerateOTP();
            Assert.NotEqual(otp1, otp2);
        }

        [Fact]
        public void GenerateOTP_ReturnsCorrectLength()
        {
            var otp = OneTimePasswordGenerator.GenerateOTP();
            Assert.Equal(6, otp.Length);
        }

        [Fact]
        public void GenerateOTP_ContainsOnlyValidCharacters()
        {
            const string validChars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var otp = OneTimePasswordGenerator.GenerateOTP();
            foreach (var c in otp)
            {
                Assert.Contains(c, validChars);
            }
        }
    }
}
