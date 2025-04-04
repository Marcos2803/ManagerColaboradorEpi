

using gestao.EpiData.Context;
using Gestao.Epi_Domain.Entities;
using Gestao.Epi_Domain.Entities.Enumerables;
using Gestao.Epi_Domain.Interface;
using GestaoEpiRepository.Base;
using Microsoft.EntityFrameworkCore;

namespace GestaoEpiRepository
{
    public class EpiRepository : BaseRepository<Epis>, IEpiServices
    {
        private readonly DataContext _dataContext;
        public EpiRepository(DataContext dataContext) : base(dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Epis> ObterEpiPorId(int EpiId)
        {
            var resultado = await _dataContext.Set<Epis>()
                .Where(x => x.Id == EpiId)
                .FirstOrDefaultAsync();


            if (resultado != null)
            {
                return resultado;
            }
            return null;

        }

        public async Task<IEnumerable<Epis>> ObterEpisAtivos()
        {
            var resultado = await _dataContext.Set<Epis>()
                .Where(x => x.StatusEpi == StatusEpiEnum.Ativo)
                .ToListAsync();

            if (resultado != null)
            {
                return resultado;
            }

            return null;
        }

        public async Task<int> ObterTotalEpis()
        {
            return await _dataContext.Epis
               .Where(x => x.StatusEpi == StatusEpiEnum.Ativo)
               .CountAsync();
        }
 
    }
}
