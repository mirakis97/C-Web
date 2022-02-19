using System.Collections.Generic;

namespace Andreys.Services.Contracts
{
    public interface IValidatorService
    {
        ICollection<string> ValidateModel(object model);
    }
}
