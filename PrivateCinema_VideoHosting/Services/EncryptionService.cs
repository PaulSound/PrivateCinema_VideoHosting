using System.Security.Cryptography;

namespace PrivateCinema_VideoHosting.Services
{
    public class EncryptionService
    {
        private static readonly string EncryptionKey = GetKey();
        public static string Encrypt(string plainText)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Convert.FromBase64String(EncryptionKey);
                aesAlg.IV = GenerateRandomIV();
                aesAlg.Padding = PaddingMode.PKCS7;
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainText);
                        }
                    }
                    return Convert.ToBase64String(aesAlg.IV.Concat(msEncrypt.ToArray()).ToArray());
                }
            }
        }
        public static string Decrypt(string cipherText)
        {
            byte[] cipherBytes = Convert.FromBase64String(cipherText);

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Convert.FromBase64String(EncryptionKey);
                aesAlg.IV = cipherBytes.Take(16).ToArray();

                aesAlg.Padding = PaddingMode.PKCS7; // Set the padding mode to PKCS7

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msDecrypt = new MemoryStream(cipherBytes, 16, cipherBytes.Length - 16))
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
        private static byte[] GenerateRandomIV()
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.GenerateIV();
                return aesAlg.IV;
            }
        }
        public static string GetKey()
        {
            // упрощенная реализация получения ключа
            return "X4+tfdFJjGJow6k7u9EDO/Tw1Lz16q3zTXl4vLSnyDY=";
        }
    }
}
