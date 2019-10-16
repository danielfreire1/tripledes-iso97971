using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace TripleDes_padding_iso97971
{
    public static class CryptyUtil
    {
        public static string Decrypt3DSISO97971(string key, string dataDecrypt)
        {
            byte[] keyArray;
            byte[] toEncryptArray = StringHexToByteArray(dataDecrypt);
            keyArray = StringHexToByteArray(key);

            TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider();
            des.Key = keyArray;
            des.Mode = CipherMode.ECB;
            des.Padding = PaddingMode.None;
            des.BlockSize = 64;

            ICryptoTransform cTransform = des.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(
                                 toEncryptArray, 0, toEncryptArray.Length);
            des.Clear();

            return ByteArrayHexToString(resultArray).Replace("\0", string.Empty);
        }

        #region metodos auxiliares

        private static string ByteArrayHexToString(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b);
            return HextoString(hex.ToString());
        }

        private static byte[] StringHexToByteArray(String hex)
        {
            int NumberChars = hex.Length;
            byte[] bytes = new byte[NumberChars / 2];
            for (int i = 0; i < NumberChars; i += 2)
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            return bytes;
        }

        private static string HextoString(string InputText)
        {
            byte[] bb = Enumerable.Range(0, InputText.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(InputText.Substring(x, 2), 16))
                             .ToArray();
            return System.Text.Encoding.ASCII.GetString(bb);
        }

        #endregion
    }
}
