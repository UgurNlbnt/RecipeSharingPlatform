using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TarifPaylasim.Models;

namespace TarifPaylasim.Interface
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}