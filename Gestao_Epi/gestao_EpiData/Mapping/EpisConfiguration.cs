using Gestao.Epi_Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace gestao.EpiData.Mapping
{
    public class EpisConfiguration : IEntityTypeConfiguration<Epis>
    {
        public void Configure(EntityTypeBuilder<Epis> builder)
        {
            builder.ToTable("Epis");
            builder.HasKey("Id");

            builder.Property(x => x.NomeEpi)
          .HasColumnType("varchar(30)")
          .IsRequired();

            builder.Property(x => x.Fabricante)
         .HasColumnType("varchar(30)")
         .IsRequired();

            builder.Property(x => x.CertificadoAprovacao)
         .HasColumnType("varchar(30)")
         .IsRequired();

        }
    }
}
