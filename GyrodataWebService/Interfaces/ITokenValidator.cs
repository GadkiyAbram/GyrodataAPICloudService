using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GyrodataWebService.Interfaces
{
    interface ITokenValidator
    {
        bool IsValid(string token);
    }
}
