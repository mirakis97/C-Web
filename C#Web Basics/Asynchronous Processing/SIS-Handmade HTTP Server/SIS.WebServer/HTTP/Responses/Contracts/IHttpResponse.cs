﻿using SIS.WebServer.HTTP.Enums;
using SIS.WebServer.HTTP.Headers;
using SIS.WebServer.HTTP.Headers.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIS.WebServer.HTTP.Responses.Contracts
{
    public interface IHttpResponse
    {
        HttpResponseStatusCode StatusCode { get; set; }

        IHttpHeaderCollection Headers { get; }

        byte[] Content { get; set; }

        void AddHeader(HttpHeader header);

        byte[] GetBytes();
    }
}
