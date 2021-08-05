﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SoftCandy.Data;

namespace SoftCandy.Migrations
{
    [DbContext(typeof(SoftCandyContext))]
    [Migration("20210805001309_Produto")]
    partial class Produto
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.14-servicing-32113")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("SoftCandy.Models.Cliente", b =>
                {
                    b.Property<int>("Id_Cliente")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Celular");

                    b.Property<string>("Endereco");

                    b.Property<string>("Nome");

                    b.HasKey("Id_Cliente");

                    b.ToTable("Cliente");
                });

            modelBuilder.Entity("SoftCandy.Models.Produto", b =>
                {
                    b.Property<int>("Cod_Produto")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Descricao");

                    b.Property<string>("Nome_Produto");

                    b.Property<decimal>("Preco_Venda");

                    b.Property<int>("Quantidade");

                    b.HasKey("Cod_Produto");

                    b.ToTable("Produto");
                });
#pragma warning restore 612, 618
        }
    }
}
