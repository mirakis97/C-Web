using BasicWebServer.Server.HTTP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicWebServer.Server.Responses
{
    public class TaxtResponse : ContentResponse
    {
        public TaxtResponse(string text)
            : base(text, ContentType.PlainText)
        {
        }
    }
}
