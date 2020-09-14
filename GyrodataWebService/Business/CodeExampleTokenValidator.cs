using GyrodataWebService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GyrodataWebService.Business
{
    public class CodeExampleTokenValidator : ITokenValidator
    {
        public bool IsValid(string token)
        {
            return CodeExampleTokenBuilder.StaticToken == token;
        }
    }
}