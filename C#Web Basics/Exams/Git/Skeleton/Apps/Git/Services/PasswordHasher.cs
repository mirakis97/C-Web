using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Git.Services
{
    public class PasswordHasher : IPasswordHasher
    {
        public string Hash(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                return string.Empty;
            }
            using var sHA256 = SHA256.Create();

            var bytes = sHA256.ComputeHash(Encoding.UTF8.GetBytes(password));

            var sb = new StringBuilder();

            for (int i = 0; i < bytes.Length; i++)
            {
                sb.Append(bytes[i].ToString("x2"));
            }
            return sb.ToString();
        }
    }
}
