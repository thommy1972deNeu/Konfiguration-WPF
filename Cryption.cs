using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Konfiguration_WPF
{


    public class Cryption
    {

        public string EncryptData(string strOriginal)
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
            catch
            {
                return null;
            }
        }

        
    }
}
