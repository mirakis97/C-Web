using SharedTrip.Models;
using SharedTrip.Models.Users;
using System.Collections.Generic;

namespace CarShop.Services
{
    public class UserService : IUserService
    {
        public (bool isValid, ICollection<ErrorViewModel> errors) ValidateModel(RegisterViewModel model)
        {
            bool isValid = true;
            List<ErrorViewModel> errors = new List<ErrorViewModel>();

            if (model.Username == null || model.Username.Length < 5 || model.Username.Length > 20)
            {
                isValid = false;
                errors.Add(new ErrorViewModel("Username must be between 5 and 20 elements long and is required!"));
            }
            if (model.Password == null || model.Password.Length < 6 || model.Password.Length > 20)
            {
                isValid = false;
                errors.Add(new ErrorViewModel("Password length must be between 6 and 20 sybmols!"));
            }
            if (model.Password != model.ConfirmPassword)
            {
                isValid = false;
                errors.Add(new ErrorViewModel("Passwords and its confimation do not match!"));
            }
            if (string.IsNullOrWhiteSpace(model.Email))
            {
                isValid = false;
                errors.Add(new ErrorViewModel("Email is required!"));
            }
            return (isValid, errors);
        }
    }
}
