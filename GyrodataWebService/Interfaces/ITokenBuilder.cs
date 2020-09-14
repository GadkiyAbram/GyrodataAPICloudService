using GyrodataWebService.Models;

namespace GyrodataWebService.Interfaces
{
    interface ITokenBuilder
    {
        string Build(Credentials cred);
    }
}
