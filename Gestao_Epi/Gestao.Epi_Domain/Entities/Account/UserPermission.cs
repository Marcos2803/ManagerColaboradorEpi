

using Gestao.Epi_Domain.Entities.Enumerables;

namespace Gestao.Epi_Domain.Entities.Account
{
    public class UserPermission
    {
        public int Id { get; set; }
        public string? UserId { get; set; }
        public string? Role { get; set; } 
        public PermissionEnum Permissao { get; set; }
    }
}
