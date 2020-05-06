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
            public static string Secret = "oC1yC2vE3cU1lS3f";
            public static string ClientID = "a675f7c1-0a8a-4b7d-b0fd-f3b04726";
        }

        static void Main(string[] args)
        {
            Console.WriteLine("[Encryption] program started");
            var key = System.Text.Encoding.UTF8.GetBytes(KeyConfig.ClientID);
            var iv = System.Text.Encoding.UTF8.GetBytes(KeyConfig.Secret);
            var encrypted = CryptoHelper.encrypt("Dot net rocks!!", key,iv);
            Console.WriteLine($"[Encryption] encrypted value is {encrypted}");
            var decrypted = CryptoHelper.decrypt(encrypted, key, iv);
            Console.WriteLine($"[Encryption] decrypted value is {decrypted}");
            string requestString = "{\"Request\": {\"Header\": {\"Timestamp\": \"201812121212\",\"ChannelDetails\": {\"ChannelID\": \"XYZ\",\"ChannelType\": \"WEB\",\"ChannelSubClass\": \"Retail\",\"BranchCode\": \"\",\"ChannelCusHdr\": {\"ChannelProtocol\": \"Mobile\"}},\"DeviceDetails\": {\"DeviceID\": \"Device1\",\"IMEINumber\": \"\",\"ClientIP\": \"\",\"OS\": \"\",\"BrowserType\": \"\",\"MobileNumber\": \"\",\"GeoLocation\": {\"Latitude\": \"13.072090\",\"Longitude\": \"80.201859\"}}},\"Body\": {\"UUID\": \"MULFEsE_s1662sss7\",\"AccountNumber\": \"0473054000000005\",\"Amount\": \"500\",\"LienID\": \"DC916788\",\"Remarks\":\"Remark 1\"}}}";
            //string requestString = @"{"Request":{"Header":{"Timestamp":"201812121212","ChannelDetails":{"ChannelID":"XYZ","ChannelType":"WEB","ChannelSubClass":"Retail","BranchCode":"","ChannelCusHdr":{"ChannelProtocol":"Mobile"}},"DeviceDetails":{"DeviceID":"Device1","IMEINumber":"","ClientIP":"","OS":"","BrowserType":"","MobileNumber":"","GeoLocation":{"Latitude":"13.072090","Longitude":"80.201859"}}},"Body":{"UUID":"MULFEsE_s1662sss7","AccountNumber":"0473054000000005","Amount":"500","LienID":"DC916788","Remarks":"Remark1"}}}]";
            var encryptedRequest = CryptoHelper.encrypt(requestString, key, iv);
            Console.WriteLine($"[Encryption] encrypted request value is {encryptedRequest}");
            var decryptedRequest = CryptoHelper.decrypt(encryptedRequest, key, iv);
            Console.WriteLine($"[Encryption] decrypted value is {decryptedRequest}");
            string suppliedHex = @"46bf78dac04177d03e3aaf66ee22c1053642265e276a5591b91ed1e2df3f490fdfb5d67efcc32cee0d7d060a26d243db2ca9c87ba06f5be8de74a5740d43e257a65a1c35b0649999b19be131543f170e6d3c4b89ba64e5bf85076bc70a9b9e376e59b1f9d91e2a6464e674b32835f3f4d00116b46357ad615a58dfc37195315d1088cc7d68911a7f168f2c284865a79571bd4e5e540bd215db9d9fa4f95503b005838ad4f646d0cdb9c0fc6adbe5b69c775d18eb546d0d40dd28c36c332b6d65a6c18064402fa4e6625c9a7d22d4bbdaf1a3012efd2a0802428e1b05713f956707b3c90cb95ace05cf46208731e8463000955d914e7ca79e579b78e4098b850d537bf7bc670b9ae7c3de9b4d218a0ab7c0e718ea6968d36ba9fee5976c91f474255d08620bcd57d19bf079a656d7d40123e2a4331a342891e205c33a19f00debcb746b1fa00f1ac67f8d9611d1f2d406571ad9ceb6885af8121ead9950bd8b57ee49b8e0887aa35ab733a9a6fcaac4fc59b189e5ec84905d36e1b8064152242251c02229f5a5ea77f90f3e280e9f2f1e4aecdfd9aab09a1b0953f259da396d8ccfba2eaa0e314e16a1a82287ed7fdd1598e7b29d960785b28ce8d2894687bfa30fa4d2d5bc6e80bffe1087414f9540882f44777c38ca2a02ee4a137fda887e6ff6cffdcb6e71313db095deee7657de76f0f118e8160c70108ff8c56bcebcfa36";
            Console.WriteLine($"[Encryption] the supplied hex value is {suppliedHex}");
            Console.WriteLine($"[Encryption the decrypted value is {CryptoHelper.decrypt(suppliedHex,key,iv)}]");
            var requestObject = createRequestObject();
            string jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(requestObject);
            var jsonencrypted = CryptoHelper.encrypt(jsonString, key, iv);
            Console.WriteLine($"[Encryption] encrypted value is {jsonencrypted}");
            var jsondecrypted = CryptoHelper.decrypt(jsonencrypted, key, iv);
            Console.WriteLine($"[Encryption] decrypted value is {jsondecrypted}");

            Console.WriteLine("[Encryption] program ended");
            Console.Read();
        }

        static CallObject createRequestObject()
        {
            return new CallObject()
            {
                Request = new Request()
                {
                    Header = new Header()
                    {
                        Timestamp = "201812121212",
                        ChannelDetails = new ChannelDetails()
                        {
                            ChannelCusHdr = new ChannelCusHdr()
                            {
                                ChannelProtocol = "Mobile"
                            },
                            ChannelId = "XYZ",
                            ChannelType = "WEB",
                            ChannelSubClass = "Retail",
                            BranchCode = ""
                        },
                        DeviceDetails = new DeviceDetails()
                        {
                            DeviceId = "Device1",
                            BrowserType = "",
                            ClientIp = "",
                            GeoLocation = new GeoLocation()
                            {
                                Latitude = "13.072090",
                                Longitude = "80.201859"
                            },
                            ImeiNumber = "",
                            MobileNumber = "",
                            Os = ""
                        }
                    }
                ,
                    Body = new Body()
                    {
                        Uuid = "MULFEsE_s1662sss7",
                        AccountNumber = "0473054000000005",
                        Amount = 500,
                        LienId = "DC916788",
                        Remarks = "Remark1"
                    }
                }
            };
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
