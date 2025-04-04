

using Gestao.Epi_Domain.Entities.Account;

namespace Gestao.Epi_Domain.Interface
{
    public  interface IAuthenticationJwtServices
    {
        string CreateToken(User user);
    }
}
