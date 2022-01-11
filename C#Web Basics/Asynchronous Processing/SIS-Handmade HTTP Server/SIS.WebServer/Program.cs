using SIS.WebServer.Controllers;
using SIS.WebServer.HTTP.Enums;
using SIS.WebServer.Routing;
using System;

namespace SIS.WebServer
{
    public class Program
    {
        static void Main(string[] args)
        {
            IServerRoutingTable serverRoutingTable = new ServerRoutingTable();

            serverRoutingTable.Add(HttpRequestMethod.Get, "/", httpRequest =>
            {
                return new HomeController().Home(httpRequest);
            });

            Server server = new Server(8000, serverRoutingTable);
            server.Run();
        }
    }
}
