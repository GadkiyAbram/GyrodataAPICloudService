using GyrodataWebService.Models;

namespace GyrodataWebService.Interfaces
{
    public interface ICredentialsValidator
    {
        bool IsValid(Credentials creds);
    }
}