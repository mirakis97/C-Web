using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIS.WebServer.HTTP.Common
{
    public class GlobalConstants
    {
        public const string HttpOneProtocolFragment = "HTTP/1.1";

        public const string HostHeaderKey = "Host";

        public const string HttpNewLine = "\r\n";

        public const string UnsupportedHttpMethodExceptionMessage = "This HTTP method - {0} is not supported";
    }
}