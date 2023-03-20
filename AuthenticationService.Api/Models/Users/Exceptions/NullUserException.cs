﻿using Xeptions;

namespace AuthenticationService.Api.Models.Users.Exceptions
{
    public class NullUserException : Xeption
    {
        public NullUserException()
            : base(message: "User is null, please fix it and try again.")
        { }
    }
}
