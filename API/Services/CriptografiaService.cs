using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using API.Dominio.Services;

namespace API.Services
{
    public class CriptografiaService : ICriptografiaService
    {
        public string Criptografar(string senha)
        {
            try
            {
                string textToEncrypt = senha;
                string ToReturn = "";

                string publickey = "12345678";
                string secretkey = "87654321";

                byte[] secretkeyByte = Encoding.UTF8.GetBytes(secretkey);
                byte[] publickeybyte = Encoding.UTF8.GetBytes(publickey);

                MemoryStream ms;
                CryptoStream cs;

                byte[] inputbyteArray = Encoding.UTF8.GetBytes(textToEncrypt);
                using (DESCryptoServiceProvider des = new())
                {
                    ms = new MemoryStream();
                    cs = new CryptoStream(ms, des.CreateEncryptor(publickeybyte, secretkeyByte), CryptoStreamMode.Write);
                    cs.Write(inputbyteArray, 0, inputbyteArray.Length);
                    cs.FlushFinalBlock();
                    ToReturn = Convert.ToBase64String(ms.ToArray());
                }
                return ToReturn;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public string Descriptografar(string senha)
        {
            try
            {
                string textToDecrypt = senha;
                string ToReturn = "";

                string publickey = "12345678";
                string secretkey = "87654321";

                byte[] privateKeyByte = Encoding.UTF8.GetBytes(secretkey);
                byte[] publicKeyByte = Encoding.UTF8.GetBytes(publickey);

                MemoryStream ms;
                CryptoStream cs;

                byte[] inputByteArray = Convert.FromBase64String(textToDecrypt.Replace(" ", "+"));
                using (DESCryptoServiceProvider des = new())
                {
                    ms = new MemoryStream();
                    cs = new CryptoStream(ms, des.CreateDecryptor(publicKeyByte, privateKeyByte), CryptoStreamMode.Write);
                    cs.Write(inputByteArray, 0, inputByteArray.Length);
                    cs.FlushFinalBlock();
                    Encoding encoding = Encoding.UTF8;
                    ToReturn = encoding.GetString(ms.ToArray());
                }
                return ToReturn;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }

        }
    }
}
