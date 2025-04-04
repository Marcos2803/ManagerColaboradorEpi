using gestao.EpiData.Mapping;
using Gestao.Epi_Domain.Entities;
using Gestao.Epi_Domain.Entities.Account;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


namespace gestao.EpiData.Context
{
    public class DataContext : IdentityDbContext<User>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        #region Tabelas
        public DbSet<User> Usuarios { get; set; }
        public DbSet<UserPermission> UserPermission { get; set; }
        public DbSet<Colaboradores> Colaboradores { get; set; }
        public DbSet<FichaColaborador> FichaColaborador { get; set; }
        public DbSet<Epis> Epis { get; set; }
        public DbSet<FichaEpiItens> FichaEpiItensEpis { get; set; }
        public DbSet<Estoque> Estoque { get; set; }





        #endregion
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>(new UserConfiguration().Configure);
            builder.Entity<UserPermission>(new UserPermissionConfiguration().Configure);
            builder.Entity<Colaboradores>(new ColaboradoresConfiguration().Configure);
            builder.Entity<FichaColaborador>(new FichaEpiConfiguration().Configure);
            builder.Entity<Epis>(new EpisConfiguration().Configure);
            builder.Entity<FichaEpiItens>(new FichaEpiItensConfiguration().Configure);
            builder.Entity<Estoque>(new EstoqueConfiguration().Configure);
            base.OnModelCreating(builder);
        }
    }
}