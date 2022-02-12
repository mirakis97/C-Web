namespace SMS
{
    using BasicWebServer.Server;
    using BasicWebServer.Server.Routing;
    using SMS.Data;
    using SMS.Services;
    using System.Threading.Tasks;

    public class StartUp
    {
        public static async Task Main()
        {
            var server = new HttpServer(routes => routes
               .MapControllers()
               .MapStaticFiles());

            server.ServiceCollection
                .Add<SMSDbContext>()
                .Add<IValidator, Validator>()
                .Add<IPasswordHasher,PasswordHasher>();

            await server.Start();
        }
    }
}