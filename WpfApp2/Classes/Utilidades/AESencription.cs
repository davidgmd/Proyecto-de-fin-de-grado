using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.Runtime.InteropServices;
using System.IO;
using System.Windows.Threading;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Linq;

namespace ElEscribaDelDJ.Classes
{
    class AESencription
    {
        private Byte[] aeskey;

        private Byte[] aesiv;

        public Byte[] AesIv
        {
            get { return aesiv; }
            set { aesiv = value; }
        }


        public Byte[] AesKey
        {
            get { return aeskey; }
            set { aeskey = value; }
        }


        public AESencription()
        {
        }


        public byte[] Encrypt(string file, string plainText, Byte[] Aeskey, Byte[] AesIv)
        {
            byte[] encrypted;
            // Create a new AesManaged.
 
            using (AesManaged aes = new AesManaged())
            {
                // Create encryptor    
                ICryptoTransform encryptor = aes.CreateEncryptor(aeskey, AesIv);
                // Create MemoryStream    
                using (MemoryStream ms = new MemoryStream())
                {
                    // Create crypto stream using the CryptoStream class. This class is the key to encryption    
                    // and encrypts and decrypts data from any given stream. In this case, we will pass a memory stream    
                    // to encrypt    
                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        // Create StreamWriter and write data to a stream    
                        using (StreamWriter sw = new StreamWriter(cs))
                            sw.Write(plainText);
                        encrypted = ms.ToArray();
                    }
                }
            }
            // Return encrypted data 
            return encrypted;
        }

        public string Decrypt(byte[] cipherText, Byte[] Aeskey, Byte[] AesIv)
        {
            string plaintext = null;
            // Create AesManaged    
            using (AesManaged aes = new AesManaged())
            {
                // Create a decryptor    
                ICryptoTransform decryptor = aes.CreateDecryptor(AesKey, AesIv);
                // Create the streams used for decryption.    
                using (MemoryStream ms = new MemoryStream(cipherText))
                {
                    // Create crypto stream    
                    using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                        // Read crypto stream    
                        using (StreamReader reader = new StreamReader(cs))
                            plaintext = reader.ReadToEnd();
                    }
                }
            }
            return plaintext;
        }
    }
}
