using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Vserv.Common.Utils
{
    public class EncryptedString : Dictionary<String, String>
    {
        #region Constructor

        /// <summary>
        /// Creates an empty dictionary
        /// </summary>
        public EncryptedString()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dictionary"></param>
        public EncryptedString(IDictionary<String, String> dictionary)
            : base(dictionary)
        {
            // To initialize the base class constructor.
        }

        #endregion

        #region Variables

        // Change the following keys to ensure uniqueness
        // Must be 8 bytes
        protected Byte[] KeyBytes = { 0x11, 0x12, 0x13, 0x14, 0x15, 0x16, 0x17, 0x18 };

        // Must be at least 8 characters
        protected String KeyString = "ABC12345";

        // Name for checksum value (unlikely to be used as arguments by user)
        protected String ChecksumKey = "__$$";

        #endregion

        #region Methods

        #region Public Methods

        /// <summary>
        /// Creates a dictionary from the given, encrypted String
        /// </summary>
        /// <param name="encryptedData"></param>
        public EncryptedString(String encryptedData)
        {
            // Decrypt String
            String data = Decrypt(encryptedData);

            // Parse out key/value pairs and add to dictionary
            String checksum = null;
            String[] args = data.Split('&');

            foreach (String arg in args)
            {
                Int32 counter = arg.IndexOf('=');

                if (!counter.Equals(-1))
                {
                    String key = arg.Substring(0, counter);
                    String value = arg.Substring(counter + 1);

                    if (key.Equals(ChecksumKey))
                    {
                        checksum = value;
                    }
                    else
                    {
                        Add(key: HttpUtility.UrlDecode(key), value: HttpUtility.UrlDecode(value));
                    }
                }
            }

            // Clear contents if valid checksum not found
            if (checksum == null || !checksum.Equals(ComputeChecksum()))
            {
                Clear();
            }
        }

        /// <summary>
        /// Returns an encrypted String that contains the current dictionary
        /// </summary>
        /// <returns></returns>
        public override String ToString()
        {
            // Build query String from current contents
            StringBuilder content = new StringBuilder();

            foreach (String key in Keys)
            {
                if (content.Length > 0)
                {
                    content.Append('&');
                }

                content.AppendFormat("{0}={1}", HttpUtility.UrlEncode(key),
                  HttpUtility.UrlEncode(base[key]));
            }

            // Add checksum
            if (content.Length > 0)
            {
                content.Append('&');
            }

            content.AppendFormat("{0}={1}", ChecksumKey, ComputeChecksum());
            return Encrypt(content.ToString());
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Encrypts the given text
        /// </summary>
        /// <param name="text">Text to be encrypted</param>
        /// <returns></returns>
        protected String Encrypt(String text)
        {
            try
            {
                Byte[] keyData = Encoding.UTF8.GetBytes(KeyString.Substring(0, 8));
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                Byte[] textData = Encoding.UTF8.GetBytes(text);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms,
                  des.CreateEncryptor(keyData, KeyBytes), CryptoStreamMode.Write);
                cs.Write(textData, 0, textData.Length);
                cs.FlushFinalBlock();

                return GetString(ms.ToArray());
            }
            catch (Exception)
            {
                return String.Empty;
            }
        }

        /// <summary>
        /// Decrypts the given encrypted text
        /// </summary>
        /// <param name="text">Text to be decrypted</param>
        /// <returns></returns>
        protected String Decrypt(String text)
        {
            try
            {
                Byte[] keyData = Encoding.UTF8.GetBytes(KeyString.Substring(0, 8));
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                Byte[] textData = GetBytes(text);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms,
                  des.CreateDecryptor(keyData, KeyBytes), CryptoStreamMode.Write);
                cs.Write(textData, 0, textData.Length);
                cs.FlushFinalBlock();

                return Encoding.UTF8.GetString(ms.ToArray());
            }
            catch (Exception)
            {
                return String.Empty;
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Returns a simple checksum for all keys and values in the collection
        /// </summary>
        /// <returns></returns>
        private String ComputeChecksum()
        {
            Int32 checksum = 0;

            foreach (KeyValuePair<String, String> pair in this)
            {
                checksum += pair.Key.Sum(c => c - '0');
                checksum += pair.Value.Sum(c => c - '0');
            }

            return checksum.ToString("X");
        }

        /// <summary>
        /// Converts a Byte array to a String of hex characters
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private String GetString(Byte[] data)
        {
            StringBuilder results = new StringBuilder();

            foreach (Byte b in data)
            {
                results.Append(b.ToString("X2"));
            }

            return results.ToString();
        }

        /// <summary>
        /// Converts a String of hex characters to a Byte array
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private Byte[] GetBytes(String data)
        {
            // GetString() encodes the hex-numbers with two digits
            Byte[] results = new Byte[data.Length / 2];

            for (Int32 count = 0; count < data.Length; count += 2)
                results[count / 2] = Convert.ToByte(data.Substring(count, 2), 16);

            return results;
        }

        #endregion

        #endregion
    }
}
