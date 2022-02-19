namespace Andreys.App
{
    using Andreys.Services.Contracts;
    using Andreys.Services.Service;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using MyWebServer;
    using MyWebServer.Controllers;
    using MyWebServer.Results.Views;
    using System.Threading.Tasks;

    public class Startup
    {
        public static async Task Main()
           => await HttpServer
               .WithRoutes(routes => routes
                   .MapStaticFiles()
                   .MapControllers())
               .WithServices(services => services
               .Add<AndreysDbContext>()
               .Add<IValidatorService, ValidatorService>()
               .Add<IPasswordHasher, PasswordHasher>()
               .Add<IUserService, UserService>()
               .Add<IProductService, ProductService>()
               .Add<IViewEngine, CompilationViewEngine>())
               .WithConfiguration<AndreysDbContext>(context => context
                   .Database.Migrate())
               .Start();
    }
}
