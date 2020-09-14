using GyrodataWebService.Database;
using GyrodataWebService.Interfaces;
using GyrodataWebService.Models;
using System;
using System.Linq;

namespace GyrodataWebService.Business
{
    public class DatabaseCredentialsValidator : ICredentialsValidator
    {
        private readonly TokenDbContext _DbContext;

        public DatabaseCredentialsValidator(TokenDbContext dbContext)
        {
            _DbContext = dbContext;
        }

        public bool IsValid(Credentials creds)
        {
            var user = _DbContext.Users.SingleOrDefault(u => u.Username.Equals(creds.User, StringComparison.CurrentCultureIgnoreCase));
            return user != null && Hash.Compare(creds.Password, user.Salt, user.Password, Hash.DefaultHashType, Hash.DefaultEncoding);
        }
    }
}