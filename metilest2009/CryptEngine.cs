/*
  Copyright 2009-2015 Alessandro Stanga
  This file is part of metilest2.

   metilest2 is free software: you can redistribute it and/or modify
   it under the terms of the GNU General Public License as published by
   the Free Software Foundation, either version 3 of the License, or
   (at your option) any later version.

   metilest2 is distributed in the hope that it will be useful,
   but WITHOUT ANY WARRANTY; without even the implied warranty of
   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
   GNU General Public License for more details.

   You should have received a copy of the GNU General Public License
   along with metilest2.  If not, see <http://www.gnu.org/licenses/>.
*/

using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

namespace metilest2009
{
    public class CryptEngine
    {
        /// <summary>
        /// Encrypt a string using dual encryption method. Return a encrypted cipher Text
        /// </summary>
        /// <param name="toEncrypt">string to be encrypted</param>
        /// <param name="useHashing">use hashing? send to for extra secirity</param>
        /// <returns></returns>
		
		//costanti contenenti le chiavi
		const string KEY1 = "27/07/1982 Orzinuovi (BS)";
		const string KEY2 = "05/06/1984 Napoli";
		
        public static string Encrypt(string toEncrypt, bool useHashing, int vKeyNumber)
        {
            byte[] keyArray;
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);
			int KeyNumber;
            string key; //chiave runtime

			KeyNumber = vKeyNumber;
			
			key = WhichKey(KeyNumber);
			
            //System.Configuration.AppSettingsReader settingsReader = new AppSettingsReader();
            // Get the key from config file
            //string key = (string)settingsReader.GetValue("SecurityKey", typeof(String));
            //System.Windows.Forms.MessageBox.Show(key);
			
            if (useHashing)
            {
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                hashmd5.Clear();
            }
            else
                keyArray = UTF8Encoding.UTF8.GetBytes(key);

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            tdes.Clear();
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }
        /// <summary>
        /// DeCrypt a string using dual encryption method. Return a DeCrypted clear string
        /// </summary>
        /// <param name="cipherString">encrypted string</param>
        /// <param name="useHashing">Did you use hashing to encrypt this data? pass true is yes</param>
        /// <returns></returns>
        public static string Decrypt(string cipherString, bool useHashing, int vKeyNumber)
        {
            byte[] keyArray;
            byte[] toEncryptArray = Convert.FromBase64String(cipherString);
			int KeyNumber;
            string key; //chiave runtime

			KeyNumber = vKeyNumber;
			
			key=WhichKey(KeyNumber);
			
            //System.Configuration.AppSettingsReader settingsReader = new AppSettingsReader();
            //Get your key from config file to open the lock!
            //string key = (string)settingsReader.GetValue("SecurityKey", typeof(String));

            if (useHashing)
            {
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                hashmd5.Clear();
            }
            else
                keyArray = UTF8Encoding.UTF8.GetBytes(key);

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
                        
            tdes.Clear();
            return UTF8Encoding.UTF8.GetString(resultArray);
        }
		
		//funzione per impostare la chiave in uso
		private static string WhichKey(int vKeyNumber){
            string tmpKey;
			switch (vKeyNumber){
			case 1:
                tmpKey = KEY1;
                break;
			case 2:
                tmpKey = KEY2;
				break;
			default:
                tmpKey = KEY1;
				break;
			}
            return tmpKey;
		}
    }
}