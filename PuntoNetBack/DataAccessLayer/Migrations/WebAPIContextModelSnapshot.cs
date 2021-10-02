﻿// <auto-generated />
using System;
<<<<<<< HEAD:PuntoNetBack/Migrations/CommanderContextModelSnapshot.cs
=======
using DataAccessLayer;
>>>>>>> Pablo:PuntoNetBack/DataAccessLayer/Migrations/WebAPIContextModelSnapshot.cs
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DataAccessLayer.Migrations
{
    [DbContext(typeof(WebAPIContext))]
    partial class WebAPIContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.10")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

<<<<<<< HEAD:PuntoNetBack/Migrations/CommanderContextModelSnapshot.cs
            modelBuilder.Entity("ProyectoNET.Models.Institucion", b =>
=======
            modelBuilder.Entity("Shared.ModeloDeDominio.Institucion", b =>
>>>>>>> Pablo:PuntoNetBack/DataAccessLayer/Migrations/WebAPIContextModelSnapshot.cs
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Direccion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("Telefono")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Instituciones");
                });

<<<<<<< HEAD:PuntoNetBack/Migrations/CommanderContextModelSnapshot.cs
            modelBuilder.Entity("ProyectoNET.Models.Usuario", b =>
=======
            modelBuilder.Entity("Shared.ModeloDeDominio.Usuario", b =>
>>>>>>> Pablo:PuntoNetBack/DataAccessLayer/Migrations/WebAPIContextModelSnapshot.cs
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("Password")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .HasColumnType("varbinary(max)");

                    b.HasKey("Id");

                    b.ToTable("Usuarios");
                });
#pragma warning restore 612, 618
        }
    }
}
