using System;
using System.Collections.Generic;
using System.Text;

namespace Git.Services
{
    public interface IPasswordHasher
    {
        public string Hash(string password);
    }
}
