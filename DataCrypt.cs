using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Text;

namespace EncryptionTask
{
    public class DataCrypt
    {
        private byte[] _dataToEncrypt;
        private readonly SecureString _publicKeyXML;
        private readonly SecureString _privateKeyXML;
        private byte[] _decryptedData;
        private byte[] _encryptedData;
        private static string _containerName = "SecretContainer";
        private CspParameters _csp = new CspParameters { KeyContainerName = _containerName };

        public DataCrypt(string dataToEncrypt, SecureString publicKeyXml, SecureString privateKeyXml)
        {
            _dataToEncrypt = new UnicodeEncoding().GetBytes(dataToEncrypt);
            _publicKeyXML = publicKeyXml;
            _privateKeyXML = privateKeyXml;
        }

        public void EncryptData()
        {
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(_csp))
            {
                rsa.FromXmlString(SecureStringToString(_publicKeyXML));
                _encryptedData = rsa.Encrypt(_dataToEncrypt, false);
                Console.WriteLine("Data is encrypted: " + new UnicodeEncoding().GetString(_encryptedData));
            }
        }


        public string DecryptedData()
        {
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(_csp))
            {
                rsa.FromXmlString(SecureStringToString(_privateKeyXML));
                _decryptedData = rsa.Decrypt(_encryptedData, false);
            }

            return new UnicodeEncoding().GetString(_decryptedData);
        }
        
        
        String SecureStringToString(SecureString value) {
            IntPtr valuePtr = IntPtr.Zero;
            try {
                valuePtr = Marshal.SecureStringToGlobalAllocUnicode(value);
                return Marshal.PtrToStringUni(valuePtr);
            } finally {
                Marshal.ZeroFreeGlobalAllocUnicode(valuePtr);
            }
        }
        
        
    }
}