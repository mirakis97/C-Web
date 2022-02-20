using FootballManager.Data;
using FootballManager.Data.Models;
using FootballManager.Services.Contracts;
using FootballManager.ViewModels.Players;
using System.Collections.Generic;
using System.Linq;

namespace FootballManager.Services.Service
{
    public class PlayersService : BaseService<Player>,IPlayersService
    {
        public PlayersService(FootballManagerDbContext data, IValidatorService validator) 
            : base(data, validator)
        {
        }

        public string AddPlayer(string userId, string playerId)
        {
            string errorsModel = string.Empty;

            if (PlayerAlreadyExistInUserCollection(playerId, userId))
            {
                errorsModel = "Player with that FullName already exists";
                return errorsModel;
            }

            var userPlayer = new UserPlayer
            {
                UserId = userId,
                PlayerId = playerId,
            };

            this.Data.UserPlayers.Add(userPlayer);

            this.Data.SaveChanges();

            return errorsModel;
        }

        public List<string> CreatePlayer(PlayerViewModel model, string userId)
        {
            ICollection<string> errorsModel = this.Validator.ValidateModel(model);
            if ((string.IsNullOrEmpty(model.Speed) || string.IsNullOrWhiteSpace(model.Speed) || int.Parse(model.Speed) > 10 || int.Parse(model.Speed) < 5))
            {
                errorsModel.Add("Speed is required , and should be greater than or equal to 0");
                return errorsModel.ToList();
            }
            if (string.IsNullOrEmpty(model.Endurance) || string.IsNullOrWhiteSpace(model.Endurance) || int.Parse(model.Endurance) > 10 || int.Parse(model.Endurance) < 5)
            {
                errorsModel.Add("Endurance is required , and should be greater than or equal to 0");
            }
            if (errorsModel.ToList().Count != 0)
            {
                return errorsModel.ToList();
            }

            if (this.PlayerAlreadyExist(model.FullName))
            {
                errorsModel.Add("Player with that Fullname already Exists");
                return errorsModel.ToList();
            }

            var player = new Player
            {
                FullName = model.FullName,
                Speed = (byte)int.Parse(model.Speed),
                Endurance = (byte)int.Parse(model.Endurance),
                ImageUrl = model.ImageUrl,
                Description = model.Description,
                Position = model.Position,
            };

            this.Data.Players.Add(player);

            this.Data.SaveChanges();

            return errorsModel.ToList();
        }

        public List<AllPlayersViewModel> GetAllPlayersModel()
        {
            var players = this.GetAllPlayers()
                .Select(x => new AllPlayersViewModel
                {
                    Id = x.Id,
                    FullName = x.FullName,
                    Speed = x.Speed,
                    Endurance = x.Endurance,
                    ImageUrl = x.ImageUrl,
                    Description = x.Description,
                    Position = x.Position,
                })
                .ToList();

            return players;
        }

        public List<UserCollectionViewModel> UserCollection(string userId)
        {
            var collectionModel = this.GetPlayersByUserId(userId)
               .Select(x => new UserCollectionViewModel
               {
                   Id = x.Id,
                   FullName = x.FullName,
                   Speed = x.Speed,
                   Endurance = x.Endurance,
                   ImageUrl = x.ImageUrl,
                   Description = x.Description,
                   Position = x.Position
               })
               .ToList();

            return collectionModel;
        }

        public void RemovePlayer(string userId, string playerId )
        {
            var userCard = this.Data.UserPlayers.FirstOrDefault(x => x.UserId == userId && x.PlayerId == playerId);

            this.Data.UserPlayers.Remove(userCard);

            this.Data.SaveChanges();
        }
        public IEnumerable<Player> GetAllPlayers()
        {
            return this.Data.Players.ToList();
        }

        public IEnumerable<Player> GetPlayersByUserId(string userId)
        {
            var cards = this.Data.UserPlayers.Where(x => x.UserId == userId).Select(x => x.Player).ToList();

            return cards.ToList();
        }

        public Player GetPlayerById(string playerId)
        {
            return this.Data.Players.FirstOrDefault(x => x.Id == playerId);
        }
        public User GetUserById(string userId)
        {
            return this.Data.Users.FirstOrDefault(x => x.Id == userId);
        }

        public bool PlayerAlreadyExist(string playerName)
        {

            if (this.Data.Players.Any(x => x.FullName == playerName))
            {
                return true;
            }

            return false;
        }

        public bool PlayerAlreadyExistInUserCollection(string playerId, string userId)
        {

            if (this.Data.UserPlayers.Any(x => x.UserId == userId && x.PlayerId == playerId))
            {
                return true;
            }

            return false;
        }
    }
}
