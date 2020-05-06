using QuickType;
using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;

namespace NodeCryptoLike
{
    class Program
    {
         internal static class KeyConfig
        {
            public static string IV = "aBcDeFgHiJkLmNoP";
            public static string Key = "375f85a0-ae67-4efb-be8d-538ff3db";
        }

        static void Main(string[] args)
        {
            Console.WriteLine("[Encryption] program started");
            Console.WriteLine(Guid.NewGuid().ToString());
            var key = System.Text.Encoding.UTF8.GetBytes(KeyConfig.Key);
            var iv = System.Text.Encoding.UTF8.GetBytes(KeyConfig.IV);
            var encrypted = CryptoHelper.encrypt("Dot net rocks!!", key,iv);
            Console.WriteLine($"[Encryption] encrypted value is {encrypted}");
            var decrypted = CryptoHelper.decrypt(encrypted, key, iv);
            Console.WriteLine($"[Encryption] decrypted value is {decrypted}");            
            Console.WriteLine("[Encryption] program ended");
            Console.Read();
        }
      
        internal static class CryptoHelper
        {
            /// <summary>
            /// uses rinjdael encryption to encypt plain text value into hex o/p
            /// Mode - CBC
            /// Padding - PKCS7
            /// </summary>
            /// <param name="text"></param>
            /// <param name="key"></param>
            /// <param name="iv"></param>
            /// <returns></returns>
            public static string encrypt(string text, byte[] key, byte[] iv)
            {
                string encrypted;
                using (var rijndael = new RijndaelManaged()
                {
                    Key = key,
                    IV = iv,
                    Mode = CipherMode.CBC,
                    // Padding = PaddingMode.PKCS7
                })
                {
                    rijndael.BlockSize = 128;
                    rijndael.KeySize = 256;

                    ICryptoTransform cryptoTransform = rijndael.CreateEncryptor(key, iv);
                    using (var memStream = new MemoryStream())
                    {
                        using (CryptoStream csEncrypt = new CryptoStream(memStream, cryptoTransform, CryptoStreamMode.Write))
                        {
                            using (StreamWriter sw = new StreamWriter(csEncrypt))
                            {
                                sw.Write(text);
                            }
                            encrypted = byteArrayToHex(memStream.ToArray());
                            Console.WriteLine($"[Encryption] The hex value of the encrypted string is {encrypted}");
                        }
                    }
                }
                return encrypted;
            }

            static byte[] hexToByteArray(string hex)
            {
                return Enumerable.Range(0, hex.Length)
                    .Where(x => x % 2 == 0)
                    .Select(x => Convert.ToByte(hex.Substring(x, 2), 16)).ToArray();
            }

            static string byteArrayToHex(byte[] bytes)
            {
                return BitConverter.ToString(bytes).Replace("-", string.Empty);
            }

            /// <summary>
            /// uses rinjdael to decypt a hex string to plain text
            /// Mode - CBC
            /// Padding - PKCS7
            /// </summary>
            /// <param name="encrypted"></param>
            /// <param name="key"></param>
            /// <param name="iv"></param>
            /// <returns></returns>
            public static string decrypt(string encrypted, byte[] key, byte[] iv)
            {
                string plaintext;
                // convert back from hex values to byte array
                byte[] ba = hexToByteArray(encrypted);
                using (var rijndael = new RijndaelManaged()
                {
                    Key = key,
                    IV = iv,
                    Padding = PaddingMode.PKCS7,
                    Mode = CipherMode.CBC
                })
                {
                    ICryptoTransform decryptor = rijndael.CreateDecryptor(key, iv);
                    using (var memStream = new MemoryStream(ba))
                    {
                        using (CryptoStream csDecrypt = new CryptoStream(memStream, decryptor, CryptoStreamMode.Read))
                        {
                            using (StreamReader sw = new StreamReader(csDecrypt))
                            {
                                plaintext = sw.ReadToEnd();
                            }
                        }
                    }

                }

                return plaintext;
            }
        }
    }    
}
