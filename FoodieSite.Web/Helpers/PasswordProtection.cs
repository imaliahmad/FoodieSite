using System.Security.Cryptography;
using System.Text;
using System;

namespace FoodieSite.Web.Helpers
{
    public static class PasswordProtection
    {
        public static string EncryptPassword(string password)
        {
            string encryptPassword = "";
            string hash = "FoodieSite1234;MyProject1234;";

            byte[] data = UTF8Encoding.UTF8.GetBytes(password);
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                byte[] keys = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
                using (TripleDESCryptoServiceProvider tripDes = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                {
                    ICryptoTransform transform = tripDes.CreateEncryptor();
                    byte[] results = transform.TransformFinalBlock(data, 0, data.Length);
                    encryptPassword = Convert.ToBase64String(results, 0, results.Length);
                }
            }

            return encryptPassword;
        }
        public static string DecryptPassword(string value)
        {
            string decryptPassword = "";
            string hash = "FoodieSite1234;MyProject1234;";

            byte[] data = Convert.FromBase64String(value); // decrypt the incrypted text
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                byte[] keys = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
                using (TripleDESCryptoServiceProvider tripDes = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                {
                    ICryptoTransform transform = tripDes.CreateDecryptor();
                    byte[] results = transform.TransformFinalBlock(data, 0, data.Length);
                    decryptPassword = UTF8Encoding.UTF8.GetString(results);
                }
            }



            return decryptPassword;
        }
    }
}
