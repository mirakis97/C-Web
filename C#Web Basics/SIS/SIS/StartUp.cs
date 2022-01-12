using HTTP;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SIS
{
    public class StartUp
    {
        static void Main()
            => new HttpServer(routes => routes
            .MapGet("/",new TextResponse("Hello from Miroslav server!"))
            .MapGet("/HTML", new HtmlResponse("<h1>HTML response</h1>"))
            .MapGet("/Redirect", new RedirectResponse("https://softuni.org/")))
            .Start();
    }
}
