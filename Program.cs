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
            
            SecureString publicKeyXML = new SecureString();
            SecureString privateKeyXML = new SecureString();
            foreach (var keyChar in rsa.ToXmlString(false))
            {
                publicKeyXML.AppendChar(keyChar);
            }
            foreach (var keyChar in rsa.ToXmlString(true))
            {
                publicKeyXML.AppendChar(keyChar);
            }

            DataCrypt dataCrypt = new DataCrypt("data4Encrypt",publicKeyXML,privateKeyXML);
            dataCrypt.EncryptData();
            Console.WriteLine("Data is decrypted: " + dataCrypt.DecryptedData());
            

        }
    }
}