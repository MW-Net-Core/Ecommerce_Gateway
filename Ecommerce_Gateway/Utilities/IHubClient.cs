﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce_Gateway.Utilities
{
    public interface IHubClient
    {
        Task InformClient(string message);
    }
}
