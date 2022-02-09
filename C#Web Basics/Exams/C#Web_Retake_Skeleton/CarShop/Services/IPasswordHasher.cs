using System;
using System.Collections.Generic;
using System.Text;

namespace CarShop.Services
{
    public interface IPasswordHasher
    {
        public string Hash(string password);
    }
}
