using gestao.EpiData.Context;
using Gestao.Epi_Domain.Entities;
using Gestao.Epi_Domain.Entities.Account;
using Gestao.Epi_Domain.Entities.Enumerables;
using Gestao.Epi_Domain.Interface;
using GestaoEpiRepository.Base;
using Microsoft.EntityFrameworkCore;

namespace GestaoEpiRepository
{
    public class FichaColaboradorRepository : BaseRepository<FichaColaborador>, IFichaColaboradorServices
    {
        private readonly DataContext _dataContext;
        public FichaColaboradorRepository(DataContext dataContext) : base(dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<IEnumerable<FichaColaborador>> ObterFichaEpiAtivo()
        {
            var resultado = await _dataContext.Set<FichaColaborador>()
                   .Include(x => x.Colaboradores)
                   .Where(x => x.StatusFicha == StatusFichaEnum.Ativo)
                   .ToListAsync();

            if (resultado != null)
            {
                return resultado;
            }

            return null;
        }

        public async Task<FichaColaborador> ObterFichaEpiPorId(int Id)
        {
            var resultado = await _dataContext.Set<FichaColaborador>()
                .Where(x => x.Id == Id)
                .FirstOrDefaultAsync();


            if (resultado != null)
            {
                return resultado;
            }
            return null;
        }

        public async Task<FichaColaborador> ObterFichaPorColaboradorId(int Id)
        {
            var result = await _dataContext.Set<FichaColaborador>()
            .Include(x => x.Colaboradores)
               .Where(x => x.ColaboradoresId == Id)
               .FirstOrDefaultAsync();

            if (result != null)
            {
                return result;
            }
            return null;
        }
    }
}
