using BasicWebServer.Server.Attributes;
using BasicWebServer.Server.Controllers;
using BasicWebServer.Server.HTTP;
using CarShop.Services;
using SharedTrip.Data;
using SharedTrip.Data.Models;
using SharedTrip.Models;
using SharedTrip.Models.Trips;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedTrip.Controllers
{
    public class TripsController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IValidator validator;
        public TripsController(Request request, ApplicationDbContext context, IValidator validator)
            : base(request)
        {
            this.dbContext = context;
            this.validator = validator;
        }
        [Authorize]
        public Response Add() => View();
        [Authorize]
        [HttpPost]
        public Response Add(TripViewModel model)
        {
            var (isValid, errors) = validator.ValidateTripAddModel(model);

            if (!isValid)
            {
                return View(errors, "/Error");
            }

            try
            {
                Trip trip = new Trip
                {
                    Description = model.Description,
                    EndPoint = model.EndPoint,
                    StartPoint = model.StartPoint,
                    ImagePath = model.ImagePath,
                    Seats = model.Seats,
                };
                DateTime date;
                DateTime.TryParseExact(model.DepartureTime, 
                    "dd.MM.yyyy HH:mm", 
                    CultureInfo.InvariantCulture, 
                    DateTimeStyles.None, 
                    out date);

                trip.DepartureTime = date;

                dbContext.Trips.Add(trip);
                dbContext.SaveChanges();
            }
            catch (Exception)
            {

                return View(new List<ErrorViewModel>() { new ErrorViewModel("Unexpected Error") }, "/Error");
            }

            return Redirect("/Trips/All");
        }
        [Authorize]
        public Response All()
        {
            IEnumerable<TripListViewModel> trips = validator.GetAllTrips();

            return View(trips);
        }
        [Authorize]
        public Response Details(string tripId)
        {
            TripDetailsViewModel tripDetailsViewModel = validator.GetTripDetails(tripId);

            return View(tripDetailsViewModel);
        }
        public Response AddUserToTrip(string tripId)
        {
            if (dbContext.UserTrips.Any(x => x.UserId == User.Id && x.TripId == tripId))
            {
                return Redirect($"/Trips/Details?tripId={tripId}");
            }
            try
            {
                validator.AddUserToTrip(tripId, User.Id);
            }
            catch (ArgumentException ex)
            {

                return View(new List<ErrorViewModel>() { new ErrorViewModel(ex.Message) }, "/Error");
            }
            catch (Exception)
            {
                return View(new List<ErrorViewModel>() { new ErrorViewModel("Unexpected Error") }, "/Error");
            }

            return Redirect("/Trips/All");
        }
    }
}
