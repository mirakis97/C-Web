using SharedTrip.Data;
using SharedTrip.Data.Models;
using SharedTrip.Models;
using SharedTrip.Models.Trips;
using SharedTrip.Models.Users;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace CarShop.Services
{
    public class Validator : IValidator
    {
        private readonly ApplicationDbContext dbContext;

        public Validator(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void AddUserToTrip(string tripId, string userId)
        {
            var user = dbContext.Users.FirstOrDefault(u => u.Id == userId);
            var trip = dbContext.Trips.FirstOrDefault(t => t.Id == tripId);

            if (user == null || trip == null)
            {
                throw new ArgumentException("User or trip not found!");
            }

            user.UserTrips.Add(new UserTrip()
            {
                TripId = tripId,
                UserId = userId,
                Trip = trip,
                User = user
            });

            dbContext.SaveChanges();
        }

        public IEnumerable<TripListViewModel> GetAllTrips()
        {
            return dbContext.Trips.Select(x => new TripListViewModel()
            {
                DepartureTime = x.DepartureTime.ToString("dd.MM.yyyy HH:mm"),
                EndPoint = x.EndPoint,
                StartPoint = x.StartPoint,
                Id= x.Id,
                Seats = x.Seats,
            });
        }

        public TripDetailsViewModel GetTripDetails(string tripId)
        {
            return dbContext.Trips
                .Where(t => t.Id == tripId)
                .Select(x => new TripDetailsViewModel()
                {
                    DepartureTime = x.DepartureTime.ToString("dd.MM.yyyy HH:mm"),
                    EndPoint = x.EndPoint,
                    StartPoint = x.StartPoint,
                    Id = x.Id,
                    Seats = x.Seats,
                    ImagePath = x.ImagePath,
                    Description = x.Description
                }).FirstOrDefault();
                
        }

        public (bool isValid, ICollection<ErrorViewModel> errors) ValidateRegisterModel(RegisterViewModel model)
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

        public (bool isValid, IEnumerable<ErrorViewModel> errors) ValidateTripAddModel(TripViewModel model)
        {
            bool isValid = true;
            List<ErrorViewModel> errors = new List<ErrorViewModel>();
            
            if (string.IsNullOrWhiteSpace(model.StartPoint))
            {
                isValid = false;
                errors.Add(new ErrorViewModel("StartPoint is required!"));
            }
            if (string.IsNullOrWhiteSpace(model.EndPoint))
            {
                isValid = false;
                errors.Add(new ErrorViewModel("EndPoint is required!"));
            }
            if (string.IsNullOrWhiteSpace(model.Description) || model.Description.Length > 80)
            {
                isValid = false;
                errors.Add(new ErrorViewModel("Description is required and must not be more than 80 charecters long!"));
            }
            if (model.Seats < 2 || model.Seats > 6)
            {
                isValid = false;
                errors.Add(new ErrorViewModel("Seats must be between 2 and 6!"));
            }
            return (isValid, errors);
        }
    }
}
