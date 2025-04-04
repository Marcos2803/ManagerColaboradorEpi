using Gestao.Epi_Domain.Entities.Enumerables;
using System.ComponentModel.DataAnnotations;

namespace Gestao_Epi.Api.Model.Auth
{
    public class UserPermissionViewModels
    {
        [Display(Name = "UserId")]
        [Required(ErrorMessage = "Campo obrigário")]
        public string? UserId { get; set; }

        [Display(Name = "RoleId")]
        [Required(ErrorMessage = "Campo obrigário")]
        public string? RoleId { get; set; }

    }
}
