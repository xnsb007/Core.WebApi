﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YunXun.WebApi.Unit
{
    public class UserOperationException: Exception
    {
        public UserOperationException() { }
        public UserOperationException(string message) : base(message) { }
        public UserOperationException(string message, Exception innerException) : base(message, innerException) { }
    }
}
