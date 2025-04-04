using Gestao.Epi_Domain.Entities.Enumerables;
using Microsoft.AspNetCore.Identity;

namespace Gestao.Epi_Domain.Entities.Account
{
    public class User : IdentityUser
    {
        public string? NomeCompleto { get; set; }
        public string? Matricula { get; set; }
        public DateTime CreatedDate { get; set; }

        public StatusUserEnum StatusUser { get; set; }
    }
}
