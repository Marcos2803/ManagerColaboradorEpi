

using System.ComponentModel;

namespace Gestao.Epi_Domain.Entities.Enumerables
{
    public enum StatusFichaEpiEnum
    {
        [Description("Entregue")]
        Entregue = 1,
        [Description("Devolvido")]
        Devolvido = 2,
    }
}
