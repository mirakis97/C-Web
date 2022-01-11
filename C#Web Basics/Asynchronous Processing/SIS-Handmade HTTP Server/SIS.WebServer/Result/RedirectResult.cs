using SIS.WebServer.HTTP.Enums;
using SIS.WebServer.HTTP.Headers;
using SIS.WebServer.HTTP.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIS.WebServer.Result
{
    public class RedirectResult : HttpResponse
    {
        public RedirectResult(string location)
           : base(HttpResponseStatusCode.SeeOther)
        {
            this.Headers.AddHeader(new HttpHeader("Location", location));
        }
    }
}
