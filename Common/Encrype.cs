using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Web;
using System.IO;

namespace Encryption
{
    /// <summary>
    /// 用于常见的加解密
    /// </summary>
    public static class Encrype
    {
        private const string KEY_64 = "zexdzexd";
        private const string IV_64 = "zexdzexd";

        /// <summary>
        /// 字符串MD5加密
        /// </summary>
        /// <param name="myString">加密原文</param>
        /// <returns>密文</returns>
        public static string GetMD5(string myString)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = System.Text.Encoding.Unicode.GetBytes(myString);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;
            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x");
            }
            return byte2String;
        }

        /// <summary>
        /// 字符串MD5加密（UTF8_16位）
        /// </summary>
        /// <param name="ConvertString">加密原文</param>
        /// <returns>密文</returns>
        public static string GetMD5_16(string ConvertString)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            string t2 = BitConverter.ToString(md5.ComputeHash(UTF8Encoding.Default.GetBytes(ConvertString)), 4, 8);
            t2 = t2.Replace("-", "");
            return t2;
        }

        /// <summary>
        /// 可逆加密（64位）
        /// </summary>
        /// <param name="data">加密原文</param>
        /// <returns>密文</returns>
        public static string Encode(string data)
        {
            byte[] byKey = System.Text.ASCIIEncoding.ASCII.GetBytes(KEY_64);
            byte[] byIV = System.Text.ASCIIEncoding.ASCII.GetBytes(IV_64);
            DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
            int i = cryptoProvider.KeySize;
            MemoryStream ms = new MemoryStream();
            CryptoStream cst = new CryptoStream(ms, cryptoProvider.CreateEncryptor(byKey, byIV), CryptoStreamMode.Write);
            StreamWriter sw = new StreamWriter(cst);
            sw.Write(data);
            sw.Flush();
            cst.FlushFinalBlock();
            sw.Flush();
            return Convert.ToBase64String(ms.GetBuffer(), 0, (int)ms.Length);
        }

        /// <summary>
        /// 可逆解密（64位）
        /// </summary>
        /// <param name="data">密文</param>
        /// <returns>原文</returns>
        public static string Decode(string data)
        {
            byte[] byKey = System.Text.ASCIIEncoding.ASCII.GetBytes(KEY_64);
            byte[] byIV = System.Text.ASCIIEncoding.ASCII.GetBytes(IV_64);
            byte[] byEnc;
            try
            {
                byEnc = Convert.FromBase64String(data);
            }
            catch
            {
                return null;
            }
            DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
            MemoryStream ms = new MemoryStream(byEnc);
            CryptoStream cst = new CryptoStream(ms, cryptoProvider.CreateDecryptor(byKey, byIV), CryptoStreamMode.Read);
            StreamReader sr = new StreamReader(cst);
            return sr.ReadToEnd();
        }

        /// <summary>
        /// 可逆加密（64位）
        /// </summary>
        /// <param name="data">原文</param>
        /// <param name="KEY">密钥（8字符64位）</param>
        /// <param name="IV">初始化向量（8字符64位）</param>
        /// <returns>密文</returns>
        public static string Encode(string data, string KEY, string IV)
        {
            byte[] byKey = System.Text.ASCIIEncoding.ASCII.GetBytes(KEY);
            byte[] byIV = System.Text.ASCIIEncoding.ASCII.GetBytes(IV);
            DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
            int i = cryptoProvider.KeySize;
            MemoryStream ms = new MemoryStream();
            CryptoStream cst = new CryptoStream(ms, cryptoProvider.CreateEncryptor(byKey, byIV), CryptoStreamMode.Write);
            StreamWriter sw = new StreamWriter(cst);
            sw.Write(data);
            sw.Flush();
            cst.FlushFinalBlock();
            sw.Flush();
            return Convert.ToBase64String(ms.GetBuffer(), 0, (int)ms.Length);
        }

        /// <summary>
        /// 可逆解密（64位）
        /// </summary>
        /// <param name="data">密文</param>
        /// <param name="KEY">密钥（8字符64位）</param>
        /// <param name="IV">初始化向量（8字符64位）</param>
        /// <returns>原文</returns>
        public static string Decode(string data, string KEY, string IV)
        {
            byte[] byKey = System.Text.ASCIIEncoding.ASCII.GetBytes(KEY);
            byte[] byIV = System.Text.ASCIIEncoding.ASCII.GetBytes(IV);
            byte[] byEnc;
            try
            {
                byEnc = Convert.FromBase64String(data);
            }
            catch
            {
                return null;
            }
            DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
            MemoryStream ms = new MemoryStream(byEnc);
            CryptoStream cst = new CryptoStream(ms, cryptoProvider.CreateDecryptor(byKey, byIV), CryptoStreamMode.Read);
            StreamReader sr = new StreamReader(cst);
            return sr.ReadToEnd();
        }

        /// <summary>
        /// 可逆加密（64位）
        /// </summary>
        /// <param name="data">原文</param>
        /// <param name="KEY">密钥（8字符64位）</param>
        /// <returns>密文</returns>
        public static string Encode(string data, string KEY)
        {
            byte[] byKey = System.Text.ASCIIEncoding.ASCII.GetBytes(KEY);
            byte[] byIV = System.Text.ASCIIEncoding.ASCII.GetBytes(IV_64);
            DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
            int i = cryptoProvider.KeySize;
            MemoryStream ms = new MemoryStream();
            CryptoStream cst = new CryptoStream(ms, cryptoProvider.CreateEncryptor(byKey, byIV), CryptoStreamMode.Write);
            StreamWriter sw = new StreamWriter(cst);
            sw.Write(data);
            sw.Flush();
            cst.FlushFinalBlock();
            sw.Flush();
            return Convert.ToBase64String(ms.GetBuffer(), 0, (int)ms.Length);
        }

        /// <summary>
        /// 可逆解密（64位）
        /// </summary>
        /// <param name="data">密文</param>
        /// <param name="KEY">密钥（8字符64位）</param>
        /// <returns>原文</returns>
        public static string Decode(string data, string KEY)
        {
            byte[] byKey = System.Text.ASCIIEncoding.ASCII.GetBytes(KEY);
            byte[] byIV = System.Text.ASCIIEncoding.ASCII.GetBytes(IV_64);
            byte[] byEnc;
            try
            {
                byEnc = Convert.FromBase64String(data);
            }
            catch
            {
                return null;
            }
            DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
            MemoryStream ms = new MemoryStream(byEnc);
            CryptoStream cst = new CryptoStream(ms, cryptoProvider.CreateDecryptor(byKey, byIV), CryptoStreamMode.Read);
            StreamReader sr = new StreamReader(cst);
            return sr.ReadToEnd();
        }
    }
}
