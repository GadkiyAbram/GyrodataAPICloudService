using GyrodataWebService.Interfaces;
using GyrodataWebService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Web;

namespace GyrodataWebService.Business
{
    public class CodeExampleTokenBuilder : ITokenBuilder
    {
        internal static string StaticToken = "{B709CE08}";

        public string Build(Credentials cred)
        {
            if (new CodeExampleCredentialsValidator().IsValid(cred))
                return StaticToken;
            throw new AuthenticationException();
        }
    }
}