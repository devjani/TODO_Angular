namespace Cryptography
{
    using Microsoft.Extensions.Configuration;
    using System;
    using System.IO;
    using System.Security.Cryptography;
    using System.Text;

    public class Cipher : ICipher
    {
        #region Private Variables

        private string password;

        #endregion


        #region Constructor

        /// <summary>Initializes a new instance of the <see cref="EmailSender"/> class.</summary>
        /// <param name="configuration">The configuration.</param>
        public Cipher(IConfiguration configuration)
        {
            var passwordSection = configuration.GetSection("PasswordSettings");
            password = passwordSection["SecretKey"];
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Encrypt a string.
        /// </summary>
        /// <param name="plainText">String to be encrypted</param>
        /// <returns>Encrypted string</returns>
        public string Encrypt(string plainText)
        {
            if (plainText == null)
            {
                return null;
            }

            if (password == null)
            {
                password = string.Empty;
            }

            // Get the bytes of the string
            var bytesToBeEncrypted = Encoding.UTF8.GetBytes(plainText);
            var passwordBytes = Encoding.UTF8.GetBytes(password);

            // Hash the password with SHA256
            passwordBytes = SHA256.Create().ComputeHash(passwordBytes);

            var bytesEncrypted = Encrypt(bytesToBeEncrypted, passwordBytes);

            return Convert.ToBase64String(bytesEncrypted);
        }

        /// <summary>
        /// Decrypt a string.
        /// </summary>
        /// <param name="encryptedText">String to be decrypted</param>
        /// <returns>Decrypted string</returns>
        /// <exception cref="FormatException">Format exception</exception>
        public string Decrypt(string encryptedText)
        {
            if (encryptedText == null)
            {
                return null;
            }

            if (password == null)
            {
                password = string.Empty;
            }

            // Get the bytes of the string
            var bytesToBeDecrypted = Convert.FromBase64String(encryptedText);
            var passwordBytes = Encoding.UTF8.GetBytes(password);

            passwordBytes = SHA256.Create().ComputeHash(passwordBytes);

            var bytesDecrypted = Decrypt(bytesToBeDecrypted, passwordBytes);

            return Encoding.UTF8.GetString(bytesDecrypted);
        }

        #endregion

        #region Private Methods

        /// <summary>Encrypts the specified bytes to with passowrd.</summary>
        /// <param name="bytesToBeEncrypted">The bytes to be encrypted.</param>
        /// <param name="passwordBytes">The password bytes.</param>
        /// <returns></returns>
        private byte[] Encrypt(byte[] bytesToBeEncrypted, byte[] passwordBytes)
        {
            byte[] encryptedBytes = null;

            // Set your salt here, change it to meet your flavor:
            // The salt bytes must be at least 8 bytes.
            var saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);

                    AES.KeySize = 256;
                    AES.BlockSize = 128;
                    AES.Key = key.GetBytes(AES.KeySize / 8);
                    AES.IV = key.GetBytes(AES.BlockSize / 8);

                    AES.Mode = CipherMode.CBC;

                    using (var cs = new CryptoStream(ms, AES.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeEncrypted, 0, bytesToBeEncrypted.Length);
                        cs.Close();
                    }

                    encryptedBytes = ms.ToArray();
                }
            }

            return encryptedBytes;
        }

        /// <summary>Decrypts the specified bytes with password.</summary>
        /// <param name="bytesToBeDecrypted">The bytes to be decrypted.</param>
        /// <param name="passwordBytes">The password bytes.</param>
        /// <returns></returns>
        private byte[] Decrypt(byte[] bytesToBeDecrypted, byte[] passwordBytes)
        {
            byte[] decryptedBytes = null;

            // Set your salt here, change it to meet your flavor:
            // The salt bytes must be at least 8 bytes.
            var saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);

                    AES.KeySize = 256;
                    AES.BlockSize = 128;
                    AES.Key = key.GetBytes(AES.KeySize / 8);
                    AES.IV = key.GetBytes(AES.BlockSize / 8);
                    AES.Mode = CipherMode.CBC;

                    using (var cs = new CryptoStream(ms, AES.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeDecrypted, 0, bytesToBeDecrypted.Length);
                        cs.Close();
                    }

                    decryptedBytes = ms.ToArray();
                }
            }

            return decryptedBytes;
        }

        #endregion
    }
}
