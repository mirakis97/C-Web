using System.Collections.Generic;

namespace FootballManager.Services.Contracts
{
    public interface IValidatorService
    {
        ICollection<string> ValidateModel(object model);
    }
}
