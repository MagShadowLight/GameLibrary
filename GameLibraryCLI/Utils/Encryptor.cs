using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GameLibraryCLI.Utils
{
    public class Encryptor
    {
        private static readonly byte[] _key = new byte[16]
                {
                    0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08,
                    0x09, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16
                };
        private static byte[] _IV = [];
        static string Decrypt(string path)
        {
            string decrypted;
            using (FileStream fileStream = new(path, FileMode.Open))
            using (Aes aes = Aes.Create())
            {
                _IV = new byte[aes.IV.Length];
                ByteReader(fileStream, aes);                
                using (CryptoStream crypto = new(
                    fileStream,
                    aes.CreateDecryptor(_key, _IV),
                    CryptoStreamMode.Read))
                {
                    using (StreamReader reader = new(crypto))
                    {
                        decrypted = reader.ReadToEnd();
                    }

                }
            }
            return decrypted;
        }

        private static void ByteReader(FileStream fileStream, Aes aes)
        {
            int numBytesReader = aes.IV.Length;
            int numread = 0;
            while (numBytesReader > 0)
            {
                int num = fileStream.Read(_IV, numread, numBytesReader);
                if (num == 0)
                    break;

                numBytesReader -= num;
                numread += num;
            }
        }
    }
}
