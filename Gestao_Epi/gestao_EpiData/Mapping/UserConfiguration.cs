using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Gestao.Epi_Domain.Entities.Account;

namespace gestao.EpiData.Mapping
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");
            builder.HasKey("Id");

            builder.Property(x => x.Matricula)
          .HasColumnType("varchar(8)")
          .IsRequired();

            builder.Property(x => x.NomeCompleto)
          .HasColumnType("varchar(50)")
          .IsRequired();
        }
            
    }
}
