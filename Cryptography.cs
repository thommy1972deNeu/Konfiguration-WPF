using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Konfiguration_WPF
{


    public static class Cryptography
    {
        private const string EncryptionKey = "test123456key";

        //TODO: clarify if following method is useless / duplicate
        public static string EncryptData(string strOriginal)
        {
            try
            {
                string EncryptionKey = "test123456key";
                byte[] clearBytes = Encoding.Unicode.GetBytes(strOriginal);
                using (Aes encryptor = Aes.Create())
                {
                    Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                    encryptor.Key = pdb.GetBytes(32);
                    encryptor.IV = pdb.GetBytes(16);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(clearBytes, 0, clearBytes.Length);
                            cs.Close();
                        }
                        strOriginal = Convert.ToBase64String(ms.ToArray());
                    }
                }
                return strOriginal;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        /// <summary>
        /// Encrypts given string with AES and returns it as bas64string
        /// </summary>
        /// <param name="toEncrypt">String to encrypt</param>
        /// <returns>Returns given string as encrypted base64String </returns>
        public static string Encrypt(string toEncrypt)
        {
            try
            {
                //TODO: clarify if const encryption key should be used 
                var encryptionKey = SystemInformation.Encryption_Key();
                var clearBytes = Encoding.Unicode.GetBytes(toEncrypt);

                using (var encryptionObject = Aes.Create())
                {
                    var salt = new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 };
                    var rfc2898DeriveBytes = new Rfc2898DeriveBytes(encryptionKey, salt);

                    encryptionObject.Key = rfc2898DeriveBytes.GetBytes(32);
                    encryptionObject.IV = rfc2898DeriveBytes.GetBytes(16);

                    using (var memoryStream = new MemoryStream())
                    {
                        using (var cryptoStream = new CryptoStream(memoryStream, encryptionObject.CreateEncryptor(), CryptoStreamMode.Write))
                        {
                            cryptoStream.Write(clearBytes, 0, clearBytes.Length);
                            cryptoStream.Close();
                        }

                        toEncrypt = Convert.ToBase64String(memoryStream.ToArray());
                    }
                }
                return toEncrypt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        
        /// <summary>
        /// Decrypts given encrypted base64string with AES and returns it as string
        /// </summary>
        /// <param name="toDecrypt">Base64String to decrypt</param>
        /// <returns>Returns given base64string as decrypted string</returns>
        public static string Decrypt(string toDecrypt)
        {
         
            try
            {
                var cipherBytes = Convert.FromBase64String(toDecrypt);

                using (var encryptionObject = Aes.Create())
                {
                    var salt = new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 };
                    var rfc2898DeriveBytes = new Rfc2898DeriveBytes(EncryptionKey, salt);

                    encryptionObject.Key = rfc2898DeriveBytes.GetBytes(32);
                    encryptionObject.IV = rfc2898DeriveBytes.GetBytes(16);

                    using (var memoryStream = new MemoryStream())
                    {
                        using (var cryptoStream = new CryptoStream(memoryStream, encryptionObject.CreateDecryptor(), CryptoStreamMode.Write))
                        {
                            cryptoStream.Write(cipherBytes, 0, cipherBytes.Length);
                            cryptoStream.Close();
                        }
                        toDecrypt = Encoding.Unicode.GetString(memoryStream.ToArray());
                    }
                }
                return toDecrypt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }
}
