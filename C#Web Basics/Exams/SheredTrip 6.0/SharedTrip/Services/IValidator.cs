using SharedTrip.Models;
using SharedTrip.Models.Trips;
using SharedTrip.Models.Users;
using System.Collections.Generic;

namespace CarShop.Services
{
    public interface IValidator
    {
        (bool isValid, ICollection<ErrorViewModel> errors) ValidateRegisterModel(RegisterViewModel model);
        (bool isValid, IEnumerable<ErrorViewModel> errors) ValidateTripAddModel(TripViewModel model);
        IEnumerable<TripListViewModel> GetAllTrips();
        TripDetailsViewModel GetTripDetails(string id);
        void AddUserToTrip(string tripId, string id);
    }
}
