using Gestao.Epi_Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace gestao.EpiData.Mapping
{
    public class EstoqueConfiguration : IEntityTypeConfiguration<Estoque>
    {
        public void Configure(EntityTypeBuilder<Estoque> builder)
        {
            builder.ToTable("Estoque");
            builder.HasKey("Id");

            builder.HasOne(x => x.Epis)
                .WithMany(a => a.Estoque)
                .HasForeignKey(a => a.EpisId);

            builder.Property(x => x.Quantidade)
             .HasColumnType("int")
             .IsRequired();

            builder.Property(x => x.EstoqueMinimo)
             .HasColumnType("int")
             .IsRequired();



        }
    }
}

