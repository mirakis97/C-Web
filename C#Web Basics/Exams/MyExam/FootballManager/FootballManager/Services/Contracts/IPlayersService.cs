using FootballManager.ViewModels.Players;
using System.Collections.Generic;

namespace FootballManager.Services.Contracts
{
    public interface IPlayersService
    {
        List<AllPlayersViewModel> GetAllPlayersModel();
        List<UserCollectionViewModel> UserCollection(string userId);
        List<string> CreatePlayer(PlayerViewModel model, string userId);
        string AddPlayer(string userId, string cardId);
        void RemovePlayer(string userId, string cardId);
    }
}
