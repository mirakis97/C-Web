using SIS.WebServer.HTTP.Common;
using SIS.WebServer.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SIS.WebServer
{
    public class Server
    {
        private const string ServerName = "127.0.0.1";

        private readonly int port;

        private readonly TcpListener tcpListener;

        private IServerRoutingTable serverRoutingTable;

        private bool isRuning;

        public Server(int port,IServerRoutingTable serverRoutingTable)
        {
            CoreValidator.ThrowIfNull(serverRoutingTable, nameof(serverRoutingTable));
            this.port = port;
            this.serverRoutingTable = serverRoutingTable;

            this.tcpListener = new TcpListener(IPAddress.Parse(ServerName), port);
        }
        public void Run()
        {
            this.tcpListener.Start();
            this.isRuning = true;

            Console.WriteLine($"Server started ont http://{ServerName}:{port}");

            while (this.isRuning)
            {
                Console.WriteLine("Waiting for client...");

                var client = this.tcpListener.AcceptSocketAsync().GetAwaiter().GetResult();

                Task.Run(() => this.Listen(client));
            }
        }

        public async Task Listen(Socket client)
        {
            var connecntionHandler = new ConnectionHandler(client, this.serverRoutingTable);
            await connecntionHandler.ProcessRequestAsync();
        }
    }
}
