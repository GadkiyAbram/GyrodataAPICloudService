using GyrodataWebService.Database;
using GyrodataWebService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GyrodataWebService.Business
{
    public class DatabaseTokenValidator : ITokenValidator
    {
        //public static double DefaultSecondsUntilTokenExpires = 1800;        // Token given for 1800 sec = 30 mins
        public static double DefaultSecondsUntilTokenExpires = 43200;        // Token given for 43200 sec = 12 hrs
        private readonly TokenDbContext _DbContext;

        public DatabaseTokenValidator(TokenDbContext dbContext)
        {
            _DbContext = dbContext;
        }

        public bool IsValid(string tokentext)
        {
            var token = _DbContext.Tokens.SingleOrDefault(t => t.Text == tokentext);
            return token != null && !IsExpired(token);
        }

        internal bool IsExpired(Token token)
        {
            var span = DateTime.Now - token.CraetedDate;
            return span.TotalSeconds > DefaultSecondsUntilTokenExpires;
        }
    }
}