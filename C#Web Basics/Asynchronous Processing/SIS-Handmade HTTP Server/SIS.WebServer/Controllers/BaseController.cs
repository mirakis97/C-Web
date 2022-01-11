using SIS.WebServer.HTTP.Enums;
using SIS.WebServer.HTTP.Responses.Contracts;
using SIS.WebServer.Result;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SIS.WebServer.Controllers
{
    public abstract class BaseController
    {
        public IHttpResponse View([CallerMemberName] string view = null)
        {
            string controllerName = this.GetType().Name.Replace("Controller", string.Empty);
            string viewName = view;

            string viewContent = File.ReadAllText(@"C:\Users\Miroslav Vasilev\source\repos\SIS-Handmade HTTP Server\SIS.WebServer\Views\Home.html");

            return new HtmlResult(viewContent, HttpResponseStatusCode.Ok);
        }
    }
}
