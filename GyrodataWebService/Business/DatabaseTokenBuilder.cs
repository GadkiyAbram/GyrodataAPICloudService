using GyrodataWebService.Database;
using GyrodataWebService.Interfaces;
using GyrodataWebService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Security.Cryptography;
using System.Web;

namespace GyrodataWebService.Business
{
    public class DatabaseTokenBuilder : ITokenBuilder
    {
        public static int TokenSize = 100;
        private readonly TokenDbContext _DbContext;

        public DatabaseTokenBuilder(TokenDbContext dbContext)
        {
            _DbContext = dbContext;
        }

        public string Build(Credentials creds)
        {
            if (!new DatabaseCredentialsValidator(_DbContext).IsValid(creds))
            {
                throw new AuthenticationException();
            }
            var token = BuildSecureToken(TokenSize);
            var user = _DbContext.Users.SingleOrDefault(u => u.Username.Equals(creds.User, StringComparison.CurrentCultureIgnoreCase));
            //_DbContext.Tokens.Add(new Token { Text = token, User = user, CraetedDate = DateTime.Now });//change to CreateDate
            _DbContext.Tokens.Add(new Token { Text = token, User = user, CraetedDate = DateTime.Now, Expiration = DateTime.Now.AddSeconds(43200) });    //change to CreateDate
            _DbContext.SaveChanges();
            return token;
        }

        private string BuildSecureToken(int length)
        {
            var buffer = new byte[length];
            using (var rngCryptoServiceProvider = new RNGCryptoServiceProvider())
            {
                rngCryptoServiceProvider.GetNonZeroBytes(buffer);
            }
            return Convert.ToBase64String(buffer);
        }
    }
}