using SharedTrip.Models;
using SharedTrip.Models.Users;
using System.Collections.Generic;

namespace CarShop.Services
{
    public interface IUserService
    {
        (bool isValid, ICollection<ErrorViewModel> errors) ValidateModel(RegisterViewModel model);
    }
}