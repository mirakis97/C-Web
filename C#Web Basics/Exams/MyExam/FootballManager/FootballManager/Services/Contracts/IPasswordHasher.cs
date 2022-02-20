namespace FootballManager.Services.Contracts
{
    public interface IPasswordHasher
    {
        string HashPasword(string password);
    }
}
