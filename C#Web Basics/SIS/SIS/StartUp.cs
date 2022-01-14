using BasicWebServer.Server.HTTP;
using BasicWebServer.Server.HTTP.Responses;
using BasicWebServer.Server.Responses;
using HTTP;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SIS
{
    public class StartUp
    {
        private const string HtmlForm = @"<form action='/HTML' method='POST'>
   Name: <input type='text' name='Name'/>
   Age: <input type='number' name ='Age'/>
<input type='submit' value ='Save' />
</form>";

        static void Main()
            => new HttpServer(routes => routes
            .MapGet("/",new TextResponse("Hello from Miroslav server!"))
            .MapGet("/Redirect", new RedirectResponse("https://softuni.org/"))
            .MapGet("/HTML", new HtmlResponse(StartUp.HtmlForm))
            .MapPost("/HTML",new TextResponse("",StartUp.AddFormDataAction)))
            .Start();

        private static void AddFormDataAction(
            Request request , Response response)
        {
            response.Body = "";

            foreach (var (key,value) in request.Form)
            {
                response.Body += $"{key} - {value}";
                response.Body += Environment.NewLine;
            }
        }
    }
}