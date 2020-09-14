using Dapper;
using GyrodataWebService.Business;
using GyrodataWebService.Database;
using GyrodataWebService.Interfaces;
using GyrodataWebService.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Authentication;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Web;

namespace GyrodataWebService.AuthServices                                       // INSTALL DAPPER!!!!!!!
{
    public class AuthService : IAuthService
    {
        public string Authenticate(Credentials creds)
        {
            using (var dbContext = new TokenDbContext())
            {
                ICredentialsValidator validator = new DatabaseCredentialsValidator(dbContext);
                if (validator.IsValid(creds))       // pass1 = 63dc4400772b90496c831e4dc2afa4321a4c371075a21feba23300fb56b7e19c
                    return new DatabaseTokenBuilder(dbContext).Build(creds);
                throw new InvalidCredentialException();
            }
        }

        public Dictionary<string, string> sentConfig()
        {
            string host = null;
            string port = null;
            Dictionary<string, string> configData = new Dictionary<string, string>();

            if (ConfigurationManager.AppSettings.AllKeys.Contains("WCFServiceURL")) {
                host = GlobalConfig.WebSiteURL("WCFServiceURL").ToString();
            }

            if (ConfigurationManager.AppSettings.AllKeys.Contains("WCFServicePORT"))
            {
                port = GlobalConfig.WebSiteURL("WCFServicePORT").ToString();
            }

            //if (GlobalConfig.WebSiteURL("WCFServiceURL") != null) {
            //    host = GlobalConfig.WebSiteURL("WCFServiceURL").ToString();
            //}
            //if (GlobalConfig.WebSiteURL("WCFServicePORT").ToString() != null) {
            //    port = GlobalConfig.WebSiteURL("WCFServicePORT").ToString();
            //}
            
            configData.Add("host", host);
            configData.Add("port", port);

            return configData;

            //return GlobalConfig.WebSiteURL("WCFServiceURL");
        }

        public bool TestToken(string token)
        {
            //var headers = OperationContext.Current.IncomingMessageProperties["httpRequest"];
            //var token = ((HttpRequestMessageProperty)headers).Headers["Token"];

            using (var dbContext = new TokenDbContext())
            {
                ITokenValidator validator = new DatabaseTokenValidator(dbContext);
                return validator.IsValid(token) ? true : false;
            }
        }
    }
}
