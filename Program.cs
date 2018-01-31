using System;
using System.Security;
using System.Security.Cryptography;

namespace EncryptionTask
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            RSACryptoServiceProvider rsa  = new RSACryptoServiceProvider();
            
            string publicKeyXML = rsa.ToXmlString(false);
            string privateKeyXML = rsa.ToXmlString(true);
            

            DataCrypt dataCrypt = new DataCrypt("data4Encrypt",publicKeyXML,privateKeyXML);
            dataCrypt.EncryptData();
            Console.WriteLine("Data is decrypted: " + dataCrypt.DecryptedData());
            

        }
    }
}