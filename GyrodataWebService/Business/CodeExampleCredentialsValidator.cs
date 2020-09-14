using GyrodataWebService.Interfaces;
using GyrodataWebService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GyrodataWebService.Business
{
    public class CodeExampleCredentialsValidator : ICredentialsValidator
    {
        public bool IsValid(Credentials creds)
        {
            if (creds.User == "user1" && Hash.Get(creds.Password, Hash.HashType.SHA256) == Hash.Get("pass1", Hash.HashType.SHA256))
            {
                return true;

            }
            return false;
        }
    }
}