using gestao.EpiData.Context;
using Gestao.Epi_Domain.Entities;
using Gestao.Epi_Domain.Entities.Enumerables;
using Gestao.Epi_Domain.Interface;
using GestaoEpiRepository.Base;
using Microsoft.EntityFrameworkCore;

namespace GestaoEpiRepository
{
    public class ColaboradorRepository : BaseRepository<Colaboradores>, IColaboradorServices
    {
        private readonly DataContext _dataContext;
        public ColaboradorRepository(DataContext dataContext) : base(dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Colaboradores> ObterColaboradorPorId(int Id)
        {
            var resultado = await _dataContext.Set<Colaboradores>()
                .FirstOrDefaultAsync();
            if (resultado != null)
            {
                return resultado;
            }
            return null;

        }

        public async Task<IEnumerable<Colaboradores>> ObterColaboradoresAtivos()
        {
            var resultado = await _dataContext.Set<Colaboradores>()
                 .Where(x => x.Status == StatusColaboradorEnum.Ativo)
                 .ToListAsync();

            if (resultado != null)
            {
                return resultado;
            }

            return null;
        }

        public async Task<Colaboradores> ObterPorMatricula(string matricula)
        {
            var resultado = await _dataContext.Set<Colaboradores>()
                .Where(x => x.Matricula == matricula)
                .FirstOrDefaultAsync();
            if (resultado != null)
            {
                return resultado;
            }
            return null;
        }

        public async Task<int> ObterColaboradoresCadastradosNaSemana()
        {
            var inicioDaSemana = DateTime.Now.Date.AddDays(-(int)DateTime.Now.DayOfWeek);
            return await _dataContext.Colaboradores.CountAsync(x => x.DataCadastro >= inicioDaSemana);
        }

        public async Task<int> ObterTotalColaboradores()
        {
            return await _dataContext.Colaboradores
       .Where(x => x.Status == StatusColaboradorEnum.Ativo)
       .CountAsync();
        }

      
    }
}
