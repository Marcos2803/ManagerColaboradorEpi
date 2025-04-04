using Gestao.Epi_Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace gestao.EpiData.Mapping
{
    public class FichaEpiItensConfiguration : IEntityTypeConfiguration<FichaEpiItens>
    {
        public void Configure(EntityTypeBuilder<FichaEpiItens> builder)
        {
            builder.ToTable("FichaEpiItens");
            builder.HasKey("Id");

            builder.HasOne(x => x.Epis)
                .WithMany(a => a.FichaEpiItens)
                .HasForeignKey(a => a.EpisId);

            builder.HasOne(x => x.FichaColaborador)
                .WithMany(a => a.FichaEpiItens)
                .HasForeignKey(a => a.FichaId);

            builder.Property(x => x.DataEntrega)
             .HasColumnType("DATETIME")
             .IsRequired();


            builder.Property(x => x.DataDevolucao)
              .HasColumnType("DATETIME")
              .IsRequired(false);

            builder.Property(x => x.ValidadeEpi)
              .HasColumnType("DATETIME")
              .IsRequired();





        }
    }
}
