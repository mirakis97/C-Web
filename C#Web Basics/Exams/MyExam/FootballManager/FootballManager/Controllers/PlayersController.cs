using FootballManager.Services.Contracts;
using FootballManager.ViewModels.Players;
using MyWebServer.Controllers;
using MyWebServer.Http;
using System.Linq;

namespace FootballManager.Controllers
{
    public class PlayersController : Controller
    {
        private readonly IPlayersService PlayersService;

        public PlayersController(IPlayersService playersService)
        {
            PlayersService = playersService;
        }

        [Authorize]
        public HttpResponse All()
        {
            if (!User.IsAuthenticated)
            {
                return Redirect("/Users/Login");
            }

            var allPlayersModel = PlayersService.GetAllPlayersModel();

            return View(allPlayersModel);
        }

        [Authorize]
        public HttpResponse Collection()
        {
            if (!User.IsAuthenticated)
            {
                return Redirect("/Users/Login");
            }

            var allUserPlayressModel = PlayersService.UserCollection(this.User.Id);

            return View(allUserPlayressModel);
        }

        [Authorize]
        public HttpResponse AddToCollection(string playerId)
        {
            if (!User.IsAuthenticated)
            {
                return Redirect("/Users/Login");
            }

            var checkForErrors = PlayersService.AddPlayer(this.User.Id, playerId);

            if (!string.IsNullOrEmpty(checkForErrors) || !string.IsNullOrWhiteSpace(checkForErrors))
            {
                return Error(checkForErrors);
            }

            return Redirect("/Players/All");
        }

        [Authorize]
        public HttpResponse RemoveFromCollection(string playerId)
        {
            if (!User.IsAuthenticated)
            {
                return Redirect("/Users/Login");
            }

            PlayersService.RemovePlayer(this.User.Id, playerId);

            return Redirect("/Players/Collection");
        }

        [Authorize]
        public HttpResponse Add()
        {
            if (!User.IsAuthenticated)
            {
                return Redirect("/Users/Login");
            }

            return View();
        }

        [Authorize]
        [HttpPost]
        public HttpResponse Add(PlayerViewModel model)
        {
            if (!User.IsAuthenticated)
            {
                return Redirect("/Users/Login");
            }

            var checkForErrors = PlayersService.CreatePlayer(model, this.User.Id);

            if (checkForErrors.Count != 0)
            {
                return Error(checkForErrors);
            }

            return Redirect("/Players/All");
        }
    }
}