using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace BasicServerDemo
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            const string newLine = "\r\n";
            TcpListener tcpListener = new TcpListener(IPAddress.Loopback, 12345);
            tcpListener.Start();

            while (true)
            {
                var client = tcpListener.AcceptTcpClient();
                using (var stream = client.GetStream())
                {

                    byte[] buffer = new byte[4096];
                    int byteLentght = 0;

                    var length = stream.Read(buffer, byteLentght, buffer.Length);

                    string request = Encoding.UTF8.GetString(buffer, 0, length);

                    Console.WriteLine(request);
                    string html = $"<h1>Hello from MirkoServer {DateTime.Now}</h1>";

                    string response = "HTTP/1.1 200 OK" + newLine + "Server: MirkoServer 2021" + newLine
                        + "Content-Type: text/html; charset=utf-8" + newLine
                        + "Content-Length: " + html.Length + newLine +
                        newLine + html + newLine;

                    byte[] responseByte = Encoding.UTF8.GetBytes(response);
                    stream.Write(responseByte);

                    Console.Write(new string('=', 100));
                }
            }
        }

        public static async Task ReadData()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            string url = "https://softuni.bg/";
            HttpClient httpClient = new HttpClient();
            var response = await httpClient.GetAsync(url);
            Console.WriteLine(response.StatusCode);
            Console.WriteLine(string.Join(Environment.NewLine
                , response.Headers.Select(x => x.Key + ": " + x.Value.First())));
        }
    }
}
