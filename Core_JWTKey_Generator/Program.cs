using System;
using System.Security.Cryptography;

namespace Core_JWTKey_Generator
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var rNGCryptoServiceProvider = new RNGCryptoServiceProvider())
            {
                var SigningSecretKey = new byte[64];
                rNGCryptoServiceProvider.GetBytes(SigningSecretKey);
                Console.WriteLine($"Secret Key is {Convert.ToBase64String(SigningSecretKey)}");
            }
            Console.ReadLine();
        }
    }
}
