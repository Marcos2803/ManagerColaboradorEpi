using gestao.EpiData.Context;
using Gestao.Epi_Domain.Entities;
using Gestao.Epi_Domain.Entities.Enumerables;
using Gestao.Epi_Domain.Interface;
using GestaoEpiRepository.Base;
using Microsoft.EntityFrameworkCore;


namespace GestaoEpiRepository
{
    public class FichaEpiItensRepository : BaseRepository<FichaEpiItens>, IFichaEpiItensServices
    {
        private readonly DataContext _dataContext;
        public FichaEpiItensRepository(DataContext dataContext) : base(dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<FichaEpiItens> ObterFichaEpiItensPorId(int Id)
        {
            var resultado = await _dataContext.Set<FichaEpiItens>()
                .Where(x => x.Id == Id)
                .FirstOrDefaultAsync();


            if (resultado != null)
            {
                return resultado;
            }
            return null;
        }



        public async Task<IEnumerable<FichaEpiItens>> ObterFichaEpiItensEntregue(int fichaId)
        {
            var resultado = await _dataContext.Set<FichaEpiItens>()
                .Include(x => x.FichaColaborador)
                .ThenInclude(x => x.Colaboradores)
                .Where(x => x.FichaId == fichaId && x.StatusFichaEpi == StatusFichaEpiEnum.Entregue)
                .Select(x => new FichaEpiItens
                  {
                      Id = x.Id,
                      EpisId = x.EpisId,
                      Epis = x.Epis,
                      FichaId = x.FichaId,
                      FichaColaborador = x.FichaColaborador,
                      DataEntrega = x.DataEntrega,
                      AssinaturaEntrega = x.AssinaturaEntrega,
                      ValidadeEpi = x.ValidadeEpi,
                      DataDevolucao = x.DataDevolucao ?? null, 
                      AssinaturaDevolucao = x.AssinaturaDevolucao,
                      StatusFichaEpi = x.StatusFichaEpi
                  })
                .ToListAsync();



            if (resultado != null)
            {
                return resultado;
            }

            return null;
        }

        public async Task<IEnumerable<FichaEpiItens>> ObterEpisAVencerEmTresMesesAsync()
        {
            var dataLimite = DateTime.UtcNow.AddMonths(3);

            var resultado = await _dataContext.Set<FichaEpiItens>()
                 .Include(x => x.FichaColaborador)
                 .ThenInclude(x => x.Colaboradores)
                 .Include(x => x.Epis)
                 .Where(x =>
                    x.ValidadeEpi <= dataLimite &&
                    x.ValidadeEpi >= DateTime.UtcNow &&
                    x.StatusFichaEpi == StatusFichaEpiEnum.Entregue )
                    
                 .Select(x => new FichaEpiItens
                 {
                     
                     Epis = x.Epis,
                     FichaColaborador = x.FichaColaborador,
                     DataEntrega = x.DataEntrega,
                     AssinaturaEntrega = x.AssinaturaEntrega,
                     ValidadeEpi = x.ValidadeEpi,
                     DataDevolucao = x.DataDevolucao ?? null,
                     AssinaturaDevolucao = x.AssinaturaDevolucao,
                     StatusFichaEpi = x.StatusFichaEpi
                 })
     .ToListAsync();

            if (resultado != null)
            {
                return resultado;
            }

            return null;
        }


        public async Task<int> ObterTotalEpiAVence()
        {
            var dataAtual = DateTime.UtcNow.Date;
            var dataLimite = dataAtual.AddMonths(3);

            return await _dataContext.FichaEpiItensEpis
                .CountAsync(x => x.ValidadeEpi != null && x.ValidadeEpi >= dataAtual && x.ValidadeEpi <= dataLimite);
        }


        public async Task<int> ObterEpiAVenceMes()
        {
            var primeiroDiaDoMes = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, 1);
            var ultimoDiaDoMes = primeiroDiaDoMes.AddMonths(1).AddDays(-1);

            return await _dataContext.FichaEpiItensEpis
                .CountAsync(e => e.ValidadeEpi.HasValue &&
                                 e.ValidadeEpi.Value >= primeiroDiaDoMes &&
                                 e.ValidadeEpi.Value <= ultimoDiaDoMes);
        }

        public async Task<IEnumerable<FichaEpiItens>> ObterEpisPorColaborador(int fichaId, string nome)
        {
            var resultado = await _dataContext.FichaEpiItensEpis
                .Include(e => e.Epis)  
                .Include(e => e.FichaColaborador) 
                .ThenInclude(fc => fc.Colaboradores)
                .Where(e => e.FichaColaborador.Id == fichaId &&
                            (string.IsNullOrEmpty(nome) || e.Epis.NomeEpi.Contains(nome)))
                .Select(x => new FichaEpiItens
                {
                    Id = x.Id,
                    EpisId = x.EpisId,
                    Epis = x.Epis,
                    FichaId = x.FichaId,
                    FichaColaborador = x.FichaColaborador,
                    DataEntrega = x.DataEntrega,
                    AssinaturaEntrega = x.AssinaturaEntrega,
                    ValidadeEpi = x.ValidadeEpi,
                    DataDevolucao = x.DataDevolucao,
                    AssinaturaDevolucao = x.AssinaturaDevolucao,
                    StatusFichaEpi = x.StatusFichaEpi
                })
                .ToListAsync();

            return resultado ?? new List<FichaEpiItens>();
        }


    }
}
