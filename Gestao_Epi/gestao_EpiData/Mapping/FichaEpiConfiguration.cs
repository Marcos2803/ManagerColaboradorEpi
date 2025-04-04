using Gestao.Epi_Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace gestao.EpiData.Mapping
{
    public class FichaEpiConfiguration : IEntityTypeConfiguration<FichaColaborador>
    {
        public void Configure(EntityTypeBuilder<FichaColaborador> builder)
        {
            builder.ToTable("FichaEpi");
            builder.HasKey("Id");

            builder.HasOne(x => x.Colaboradores)
               .WithMany(a => a.FichaColaborador)
               .HasForeignKey(a => a.ColaboradoresId);

            builder.Property(x => x.DataCadastro)
             .HasColumnType("DATETIME")
             .IsRequired();

            builder.Property(x => x.StatusFicha)
             .HasColumnType("varchar(7)")
             .IsRequired();




        }
    }
}
