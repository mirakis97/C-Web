using SMS.Data;
using SMS.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SMS.Services
{
    public class Validator : IValidator
    {
        private readonly SMSDbContext dbContext;
        
        public Validator(SMSDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void AddUserToTrip(string tripId, string userId)
        {
            dbContext.SaveChanges();
        }

        //public IEnumerable<TripListViewModel> GetAllTrips()
        //{
           
        //}

        //public TripDetailsViewModel GetTripDetails(string tripId)
        //{
        //    return dbContext.Trips
        //        .Where(t => t.Id == tripId)
        //        .Select(x => new TripDetailsViewModel()
        //        {
        //            DepartureTime = x.DepartureTime.ToString("dd.MM.yyyy HH:mm"),
        //            EndPoint = x.EndPoint,
        //            StartPoint = x.StartPoint,
        //            Id = x.Id,
        //            Seats = x.Seats,
        //            ImagePath = x.ImagePath,
        //            Description = x.Description
        //        }).FirstOrDefault();
                
        //}

        public (bool isValid, string errors) ValidateRegisterModel(RegisterViewFormModel model)
        {
            bool isValid = true;
            StringBuilder errors = new StringBuilder();

            if (model == null)
            {
                return (false,"Register model is required!");
            }

            if (model.Username == null || model.Username.Length < 5 || model.Username.Length > 20)
            {
                isValid = false;
                errors.AppendLine("Username must be between 5 and 20 elements long and is required!");
            }
            if (model.Password == null || model.Password.Length < 6 || model.Password.Length > 20)
            {
                isValid = false;
                errors.AppendLine("Password length must be between 6 and 20 sybmols!");
            }
            if (model.Password != model.ConfirmPassword)
            {
                isValid = false;
                errors.AppendLine("Passwords and its confimation do not match!");
            }
            if (string.IsNullOrWhiteSpace(model.Email))
            {
                isValid = false;
                errors.AppendLine("Email is required!");
            }
            return (isValid, errors.ToString());
        }
    }
}
