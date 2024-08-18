﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverflow.Domain.Exceptions
{
    public class DuplicateUserNameException : Exception
    {
        public DuplicateUserNameException() : base("UserName already taken") { }
    }
}
