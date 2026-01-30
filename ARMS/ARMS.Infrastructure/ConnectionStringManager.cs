using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace ARMS.WebAPI.Extensions
{
    public class ConnectionStringManager
    {
        private readonly IConfiguration _configuration;
        private readonly byte[] _encryptionKey;
        private readonly byte[] _iv; // Store IV for consistency

        public ConnectionStringManager(IConfiguration configuration)
        {
            _configuration = configuration;

            // Use a fixed encryption key
            _encryptionKey = new byte[]
            {
                0x01, 0x23, 0x45, 0x67, 0x89, 0xAB, 0xCD, 0xEF,
                0xFE, 0xDC, 0xBA, 0x98, 0x76, 0x54, 0x32, 0x10,
                0x89, 0xAB, 0xCD, 0xEF, 0x67, 0x45, 0x23, 0x01,
                0xEF, 0xCD, 0xAB, 0x89, 0x67, 0x45, 0x23, 0x01
            };

            // Generate a random IV once
            _iv = GenerateIV();
        }

        private byte[] GenerateIV()
        {
            // Create a new byte array to hold the IV
            byte[] iv = new byte[16]; // AES IV is 16 bytes long

            // Use a cryptographic random number generator to fill the IV with random bytes
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(iv);
            }

            return iv;
        }

        public string EncryptConnectionString(string connectionString)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = _encryptionKey;
                aesAlg.IV = _iv; // Use the same IV for consistency

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(connectionString);
                        }
                    }
                    byte[] encryptedData = msEncrypt.ToArray();
                    byte[] result = new byte[_iv.Length + encryptedData.Length];
                    Buffer.BlockCopy(_iv, 0, result, 0, _iv.Length);
                    Buffer.BlockCopy(encryptedData, 0, result, _iv.Length, encryptedData.Length);
                    return Convert.ToBase64String(result);
                }
            }
        }

        public string DecryptConnectionString(string encryptedConnectionString)
        {
            byte[] cipherBytes = Convert.FromBase64String(encryptedConnectionString);
            byte[] iv = new byte[16]; // IV size for AES is 16 bytes.
            byte[] encryptedData = new byte[cipherBytes.Length - iv.Length];
            Buffer.BlockCopy(cipherBytes, 0, iv, 0, iv.Length);
            Buffer.BlockCopy(cipherBytes, iv.Length, encryptedData, 0, encryptedData.Length);

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = _encryptionKey;
                aesAlg.IV = iv;

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msDecrypt = new MemoryStream(encryptedData))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            return srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
        }
    }
}
