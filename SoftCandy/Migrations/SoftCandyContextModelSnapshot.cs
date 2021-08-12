﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SoftCandy.Data;

namespace SoftCandy.Migrations
{
    [DbContext(typeof(SoftCandyContext))]
    partial class SoftCandyContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.14-servicing-32113")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("SoftCandy.Models.Categoria", b =>
                {
                    b.Property<int>("CategoriaId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CategoriaNome");

                    b.HasKey("CategoriaId");

                    b.ToTable("Categoria");
                });

            modelBuilder.Entity("SoftCandy.Models.Cliente", b =>
                {
                    b.Property<int>("Id_Cliente")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Celular")
                        .IsRequired()
                        .HasMaxLength(11);

                    b.Property<string>("Endereco")
                        .IsRequired()
                        .HasMaxLength(254);

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(60);

                    b.HasKey("Id_Cliente");

                    b.ToTable("Cliente");
                });

            modelBuilder.Entity("SoftCandy.Models.Pedido", b =>
                {
                    b.Property<int>("Num_Pedido")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Data_Pedido");

                    b.Property<decimal>("Desconto");

                    b.Property<int>("ID_CLIENTE");

                    b.Property<decimal>("Valor_Total");

                    b.HasKey("Num_Pedido");

                    b.HasIndex("ID_CLIENTE");

                    b.ToTable("Pedido");
                });

            modelBuilder.Entity("SoftCandy.Models.Produto", b =>
                {
                    b.Property<int>("Cod_Produto")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Descricao")
                        .HasMaxLength(60);

                    b.Property<string>("Nome_Produto")
                        .IsRequired()
                        .HasMaxLength(60);

                    b.Property<decimal>("Preco_Venda");

                    b.Property<int>("Quantidade");

                    b.HasKey("Cod_Produto");

                    b.ToTable("Produto");
                });

            modelBuilder.Entity("SoftCandy.Models.Vendedor", b =>
                {
                    b.Property<int>("Id_Vendedor")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Celular_Vendedor");

                    b.Property<string>("Email_Vendedor");

                    b.Property<string>("Endereco_Vendedor");

                    b.Property<string>("Nome_Vendedor");

                    b.Property<string>("Senha_Vendedor");

                    b.HasKey("Id_Vendedor");

                    b.ToTable("Vendedor");
                });

            modelBuilder.Entity("SoftCandy.Models.Pedido", b =>
                {
                    b.HasOne("SoftCandy.Models.Cliente", "Cliente")
                        .WithMany("Pedidos")
                        .HasForeignKey("ID_CLIENTE")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
