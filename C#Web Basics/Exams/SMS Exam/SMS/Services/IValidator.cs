using SMS.Models.Users;

namespace SMS.Services
{
    public interface IValidator
    {
        (bool isValid, string errors) ValidateRegisterModel(RegisterViewFormModel model);
    }
}
