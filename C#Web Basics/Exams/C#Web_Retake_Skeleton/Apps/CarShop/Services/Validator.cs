using CarShop.ViewModels.Car;
using CarShop.ViewModels.Users;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace CarShop.Services
{
    public class Validator : IValidator
    {
        public ICollection<string> ValidateCarCreation(AddCarFormModel model)
        {
            var errors = new List<string>();
            if (model.Model.Length < 5 || model.Model.Length > 20)
            {
                errors.Add("Username must be between 4 and 20 elements long!");
            }
            if (model.Year < 1900 || model.Year > 2100)
            {
                errors.Add("Year must be between 1900 and 2100!");
            }
            if (!Regex.IsMatch(model.PlateNumber, @"[A-Z]{2}[0-9]{4}[A-Z]{2}"))
            {
                errors.Add("Plate number must in format 'AA0000AA'!");
            }

            return errors;
        }

        public ICollection<string> ValidateUserRegistration(RegisterUserFormModel model)
        {
            var errors = new List<string>();
            if (model.Username.Length < 4 || model.Username.Length > 20 )
            {
                errors.Add("Car model must be between 5 and 20 elements long!");
            }
            if(model.Password.Length < 5 || model.Password.Length > 20)
            {
                errors.Add("Password length must be between 5 and 20 sybmols!");
            }
            if (model.Password != model.ConfirmPassword)
            {
                errors.Add("Passwords and its confimation do not match!");
            }
            if (model.UserType != "Mechanic" && model.UserType != "Client")
            {
                errors.Add("User should be 'Mechanic' or 'Client' !");
            }

            return errors;
        }
    }
}
