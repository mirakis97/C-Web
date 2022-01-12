﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicWebServer.Server.HTTP.Responses
{
    public class UnauthorizedResponse : Response
    {
        public UnauthorizedResponse() 
            : base(StatusCode.Unauthorized)
        {
        }
    }
}
