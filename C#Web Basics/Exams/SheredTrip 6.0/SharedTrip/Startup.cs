namespace SharedTrip
{
    using BasicWebServer.Server;
    using BasicWebServer.Server.Routing;
    using CarShop.Services;
    using SharedTrip.Data;
    using System.Threading.Tasks;

    public class Startup
    {
        public static async Task Main()
        {
            var server = new HttpServer(routes => routes
               .MapControllers()
               .MapStaticFiles());

            server.ServiceCollection
                .Add<IUserService,UserService>()
                .Add<IPasswordHasher,PasswordHasher>()
                .Add<IValidator,Validator>()
                .Add<ApplicationDbContext>();

            await server.Start();
        }
    }
}
