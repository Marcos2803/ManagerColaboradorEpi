using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Gestao.Epi_Domain.Entities;

namespace gestao.EpiData.Mapping
{
    public class ColaboradoresConfiguration : IEntityTypeConfiguration<Colaboradores>
    {
        public void Configure(EntityTypeBuilder<Colaboradores> builder)
        {
            builder.ToTable("Colaboradores");
            builder.HasKey("Id");

            builder.Property(x => x.Matricula)
            .HasColumnType("varchar(8)")
            .IsRequired();

            builder.Property(x => x.Foto)
             .HasColumnType("VARBINARY(MAX)") 
             .IsRequired(false); 


            builder.Property(x => x.NomeCompleto)
            .HasColumnType("varchar(50)")
            .IsRequired();

            builder.Property(x => x.DataCadastro)
            .HasColumnType("date")
            .IsRequired();

            builder.Property(x => x.Status)
            .HasColumnType("varchar(7)")
            .IsRequired();

        }
    }
}