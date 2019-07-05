using System.Text;
using System.Security.Cryptography;

namespace CryptocurrencyRateWebApp.Services {
    public class PasswordHasher {
        public string HashPassword(string password) {
            byte[] passwordBytes;
            byte[] hashBytes;

            passwordBytes = ASCIIEncoding.ASCII.GetBytes(password);
            hashBytes = new MD5CryptoServiceProvider().ComputeHash(passwordBytes);
            string passwordHash = ByteArrayToString(hashBytes);

            return passwordHash;
        }

        private string ByteArrayToString(byte[] arrInput) {
            StringBuilder sOutput = new StringBuilder(arrInput.Length);
            for (int i = 0; i < arrInput.Length; i++) {
                sOutput.Append(arrInput[i].ToString("X2"));
            }
            return sOutput.ToString();
        }
    }
}