

using Gestao.Epi_Domain.Entities.Account;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace gestao.EpiData.Mapping
{
    public class UserPermissionConfiguration : IEntityTypeConfiguration<UserPermission>
    {
        public void Configure(EntityTypeBuilder<UserPermission> builder)
        {
            builder.ToTable("UserPerssion");
            builder.HasKey("Id");

            builder.Property(x => x.UserId)
          .HasColumnType("varchar(40)")
          .IsRequired();

            builder.Property(x => x.Role)
          .HasColumnType("varchar(40)")
          .IsRequired();
        }
    }
}
