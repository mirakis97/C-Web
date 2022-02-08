using CarShop.ViewModels.Car;
using CarShop.ViewModels.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarShop.Services
{
    public interface IValidator
    {
        ICollection<string> ValidateUserRegistration(RegisterUserFormModel model);

        ICollection<string> ValidateCarCreation(AddCarFormModel car);
    }
}
