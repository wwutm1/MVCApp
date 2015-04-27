using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;

namespace WebApplication1.Models
{
    public static class ParagraphManipulation
    {
        public static string reverseParagraph(string input)
        {
            string[] inputArray = input.Split(' ');
            Array.Reverse(inputArray);
            return string.Join(" ", inputArray);
        }

        public static string reverseParagraphWords(string input)
        {
            return string.Join(" ", input.Split(' ').Select(x => new String(x.Reverse().ToArray())));
        }

        public static string sortParagraphWords(string input)
        {
            string[] inputArray = input.Split(' ');
            Array.Sort(inputArray);
            return string.Join(" ", inputArray);
        }

        public static string hashParagraph(string input)
        {
            //get random salt value of size  = 16
            int saltSize = 16;
            Random random = new Random();
            byte[] saltBytes = new byte[saltSize];
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            rng.GetNonZeroBytes(saltBytes);

            //convert text to byte array
            byte[] inputByteArray = new byte[input.Length * sizeof(char)];
            System.Buffer.BlockCopy(input.ToCharArray(), 0, inputByteArray, 0, inputByteArray.Length);

            byte[] plainTextWithSaltBytes = new byte[inputByteArray.Length + saltBytes.Length];

            for (int i = 0; i < inputByteArray.Length; i++)
                plainTextWithSaltBytes[i] = inputByteArray[i];
            for (int i = 0; i < saltBytes.Length; i++)
                plainTextWithSaltBytes[inputByteArray.Length + i] = saltBytes[i];

            HashAlgorithm hashAlgorithm = new SHA384Managed();

            byte[] hashBytes = hashAlgorithm.ComputeHash(plainTextWithSaltBytes);
            byte[] hashWithSaltBytes = new byte[hashBytes.Length + saltBytes.Length];

            for (int i = 0; i < hashBytes.Length; i++)
                hashWithSaltBytes[i] = hashBytes[i];
            for (int i = 0; i < saltBytes.Length; i++)
                hashWithSaltBytes[hashBytes.Length + i] = saltBytes[i];

            return Convert.ToBase64String(hashWithSaltBytes);
        }
    }
}