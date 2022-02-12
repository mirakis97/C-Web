using System;
using System.Collections.Generic;
using System.Text;

namespace SMS.Services
{
    public interface IPasswordHasher
    {
        public string Hash(string password);
    }
}
