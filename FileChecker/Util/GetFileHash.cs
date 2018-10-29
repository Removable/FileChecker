using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace FileChecker.Util
{
    public class GetFileHash
    {
        /// <summary>
        /// 获取文件MD5值
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns></returns>
        public static string GetMd5(string filePath)
        {
            try
            {
                var file = new FileStream(filePath, FileMode.Open);
                MD5 md5 = new MD5CryptoServiceProvider();
                var retVal = md5.ComputeHash(file);
                file.Close();

                var sc = new StringBuilder();
                foreach (var t in retVal) sc.Append(t.ToString("x2"));

                return sc.ToString();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// 获取文件SHA1值
        /// </summary>
        /// <param name="filePaht">文件路径</param>
        /// <returns></returns>
        public static string GetSha1(string filePaht)
        {
            try
            {
                FileStream file = new FileStream(filePaht, FileMode.Open);
                SHA1 sha1 = new SHA1CryptoServiceProvider();
                byte[] retVal = sha1.ComputeHash(file);
                file.Close();

                StringBuilder sc = new StringBuilder();
                foreach (var t in retVal)
                {
                    sc.Append(t.ToString("x2"));
                }

                return sc.ToString();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}