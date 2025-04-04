﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using gestao.EpiData.Context;

#nullable disable

namespace gestao.EpiData.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Gestao.Epi_Domain.Entities.Account.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Matricula")
                        .IsRequired()
                        .HasColumnType("varchar(8)");

                    b.Property<string>("NomeCompleto")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StatusUser")
                        .HasColumnType("int");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Gestao.Epi_Domain.Entities.Account.UserPermission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Permissao")
                        .HasColumnType("int");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("varchar(40)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(40)");

                    b.HasKey("Id");

                    b.ToTable("UserPerssion", (string)null);
                });

            modelBuilder.Entity("Gestao.Epi_Domain.Entities.Colaboradores", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DataCadastro")
                        .HasColumnType("date");

                    b.Property<byte[]>("Foto")
                        .HasColumnType("VARBINARY(MAX)");

                    b.Property<string>("Matricula")
                        .IsRequired()
                        .HasColumnType("varchar(8)");

                    b.Property<string>("NomeCompleto")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("varchar(7)");

                    b.HasKey("Id");

                    b.ToTable("Colaboradores", (string)null);
                });

            modelBuilder.Entity("Gestao.Epi_Domain.Entities.Epis", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CertificadoAprovacao")
                        .IsRequired()
                        .HasColumnType("varchar(30)");

                    b.Property<string>("Fabricante")
                        .IsRequired()
                        .HasColumnType("varchar(30)");

                    b.Property<string>("NomeEpi")
                        .IsRequired()
                        .HasColumnType("varchar(30)");

                    b.Property<int?>("StatusEpi")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Epis", (string)null);
                });

            modelBuilder.Entity("Gestao.Epi_Domain.Entities.Estoque", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("EpisId")
                        .HasColumnType("int");

                    b.Property<int>("EstoqueMinimo")
                        .HasColumnType("int");

                    b.Property<int>("Quantidade")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EpisId");

                    b.ToTable("Estoque", (string)null);
                });

            modelBuilder.Entity("Gestao.Epi_Domain.Entities.FichaColaborador", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ColaboradoresId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DataCadastro")
                        .HasColumnType("DATETIME");

                    b.Property<string>("StatusFicha")
                        .IsRequired()
                        .HasColumnType("varchar(7)");

                    b.HasKey("Id");

                    b.HasIndex("ColaboradoresId");

                    b.ToTable("FichaEpi", (string)null);
                });

            modelBuilder.Entity("Gestao.Epi_Domain.Entities.FichaEpiItens", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AssinaturaDevolucao")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AssinaturaEntrega")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DataDevolucao")
                        .HasColumnType("DATETIME");

                    b.Property<DateTime?>("DataEntrega")
                        .IsRequired()
                        .HasColumnType("DATETIME");

                    b.Property<int>("EpisId")
                        .HasColumnType("int");

                    b.Property<int?>("FichaId")
                        .HasColumnType("int");

                    b.Property<int?>("StatusFichaEpi")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ValidadeEpi")
                        .IsRequired()
                        .HasColumnType("DATETIME");

                    b.HasKey("Id");

                    b.HasIndex("EpisId");

                    b.HasIndex("FichaId");

                    b.ToTable("FichaEpiItens", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Gestao.Epi_Domain.Entities.Estoque", b =>
                {
                    b.HasOne("Gestao.Epi_Domain.Entities.Epis", "Epis")
                        .WithMany("Estoque")
                        .HasForeignKey("EpisId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Epis");
                });

            modelBuilder.Entity("Gestao.Epi_Domain.Entities.FichaColaborador", b =>
                {
                    b.HasOne("Gestao.Epi_Domain.Entities.Colaboradores", "Colaboradores")
                        .WithMany("FichaColaborador")
                        .HasForeignKey("ColaboradoresId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Colaboradores");
                });

            modelBuilder.Entity("Gestao.Epi_Domain.Entities.FichaEpiItens", b =>
                {
                    b.HasOne("Gestao.Epi_Domain.Entities.Epis", "Epis")
                        .WithMany("FichaEpiItens")
                        .HasForeignKey("EpisId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Gestao.Epi_Domain.Entities.FichaColaborador", "FichaColaborador")
                        .WithMany("FichaEpiItens")
                        .HasForeignKey("FichaId");

                    b.Navigation("Epis");

                    b.Navigation("FichaColaborador");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Gestao.Epi_Domain.Entities.Account.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Gestao.Epi_Domain.Entities.Account.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Gestao.Epi_Domain.Entities.Account.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Gestao.Epi_Domain.Entities.Account.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Gestao.Epi_Domain.Entities.Colaboradores", b =>
                {
                    b.Navigation("FichaColaborador");
                });

            modelBuilder.Entity("Gestao.Epi_Domain.Entities.Epis", b =>
                {
                    b.Navigation("Estoque");

                    b.Navigation("FichaEpiItens");
                });

            modelBuilder.Entity("Gestao.Epi_Domain.Entities.FichaColaborador", b =>
                {
                    b.Navigation("FichaEpiItens");
                });
#pragma warning restore 612, 618
        }
    }
}
