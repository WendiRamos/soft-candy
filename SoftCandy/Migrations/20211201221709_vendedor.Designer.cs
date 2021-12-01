﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SoftCandy.Data;

namespace SoftCandy.Migrations
{
    [DbContext(typeof(SoftCandyContext))]
    [Migration("20211201221709_vendedor")]
    partial class vendedor
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.14-servicing-32113")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("SoftCandy.Models.Administrador", b =>
                {
                    b.Property<int>("IdAdministrador")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("AtivoAdministrador");

                    b.Property<string>("BairroAdministrador");

                    b.Property<string>("CelularAdministrador");

                    b.Property<string>("CidadeAdministrador");

                    b.Property<string>("EmailAdministrador");

                    b.Property<string>("EstadoAdministrador");

                    b.Property<string>("LogradouroAdministrador");

                    b.Property<string>("NomeAdministrador");

                    b.Property<string>("NumeroAdministrador");

                    b.Property<string>("SenhaAdministrador");

                    b.HasKey("IdAdministrador");

                    b.ToTable("Administrador");
                });

            modelBuilder.Entity("SoftCandy.Models.Categoria", b =>
                {
                    b.Property<int>("IdCategoria")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("AtivoCategoria");

                    b.Property<string>("NomeCategoria");

                    b.HasKey("IdCategoria");

                    b.ToTable("Categoria");
                });

            modelBuilder.Entity("SoftCandy.Models.Cliente", b =>
                {
                    b.Property<int>("IdCliente")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("AtivoCliente");

                    b.Property<string>("BairroCliente");

                    b.Property<string>("CelularCliente");

                    b.Property<string>("CidadeCliente");

                    b.Property<string>("EmailCliente");

                    b.Property<string>("EstadoCliente");

                    b.Property<string>("LogradouroCliente");

                    b.Property<string>("NomeCliente");

                    b.Property<string>("NumeroCliente");

                    b.HasKey("IdCliente");

                    b.ToTable("Cliente");
                });

            modelBuilder.Entity("SoftCandy.Models.Estoquista", b =>
                {
                    b.Property<int>("IdEstoquista")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("AtivoEstoquista");

                    b.Property<string>("BairroEstoquista");

                    b.Property<string>("CelularEstoquista");

                    b.Property<string>("CidadeEstoquista");

                    b.Property<string>("EmailEstoquista");

                    b.Property<string>("EstadoEstoquista");

                    b.Property<string>("LogradouroEstoquista");

                    b.Property<string>("NomeEstoquista");

                    b.Property<string>("NumeroEstoquista");

                    b.Property<string>("SenhaEstoquista");

                    b.HasKey("IdEstoquista");

                    b.ToTable("Estoquista");
                });

            modelBuilder.Entity("SoftCandy.Models.Fornecedor", b =>
                {
                    b.Property<int>("IdFornecedor")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("AtivoFornecedor");

                    b.Property<string>("BairroFornecedor");

                    b.Property<string>("CelularFornecedor");

                    b.Property<string>("CidadeFornecedor");

                    b.Property<string>("Cnpj");

                    b.Property<string>("EmailFornecedor");

                    b.Property<string>("EstadoFornecedor");

                    b.Property<string>("LogradouroFornecedor");

                    b.Property<string>("NomeFantasia");

                    b.Property<string>("NumeroFornecedor");

                    b.Property<string>("RazaoSocial");

                    b.HasKey("IdFornecedor");

                    b.ToTable("Fornecedor");
                });

            modelBuilder.Entity("SoftCandy.Models.ItemPedido", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("AtivoItemPedido");

                    b.Property<int>("IdPedido");

                    b.Property<int>("IdProduto");

                    b.Property<decimal>("PrecoPago");

                    b.Property<int>("QuantidadeProduto");

                    b.HasKey("Id");

                    b.HasIndex("IdPedido");

                    b.HasIndex("IdProduto");

                    b.ToTable("Item_Pedido");
                });

            modelBuilder.Entity("SoftCandy.Models.Pedido", b =>
                {
                    b.Property<int>("IdPedido")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("AtivoPedido");

                    b.Property<DateTime>("DataPedido");

                    b.Property<int>("IdCliente");

                    b.Property<int>("IdVendedor");

                    b.Property<decimal>("ValorTotalPedido");

                    b.HasKey("IdPedido");

                    b.HasIndex("IdCliente");

                    b.HasIndex("IdVendedor");

                    b.ToTable("Pedido");
                });

            modelBuilder.Entity("SoftCandy.Models.Produto", b =>
                {
                    b.Property<int>("IdProduto")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("AtivoProduto");

                    b.Property<string>("DescricaoProduto");

                    b.Property<int>("IdCategoria");

                    b.Property<int>("IdFornecedor");

                    b.Property<string>("NomeProduto");

                    b.Property<decimal>("PrecoVendaProduto");

                    b.Property<int>("QuantidadeMinimaProduto");

                    b.Property<int>("QuantidadeProduto");

                    b.HasKey("IdProduto");

                    b.HasIndex("IdCategoria");

                    b.HasIndex("IdFornecedor");

                    b.ToTable("Produto");
                });

            modelBuilder.Entity("SoftCandy.Models.Vendedor", b =>
                {
                    b.Property<int>("IdVendedor")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("AtivoVendedor");

                    b.Property<string>("BairroVendedor");

                    b.Property<string>("CelularVendedor");

                    b.Property<string>("CidadeVendedor");

                    b.Property<string>("EmailVendedor");

                    b.Property<string>("EstadoVendedor");

                    b.Property<string>("LogradouroVendedor");

                    b.Property<string>("NomeVendedor");

                    b.Property<string>("NumeroVendedor");

                    b.Property<string>("SenhaVendedor");

                    b.HasKey("IdVendedor");

                    b.ToTable("Vendedor");
                });

            modelBuilder.Entity("SoftCandy.Models.ItemPedido", b =>
                {
                    b.HasOne("SoftCandy.Models.Pedido", "Pedido")
                        .WithMany("ItensPedidos")
                        .HasForeignKey("IdPedido")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SoftCandy.Models.Produto", "Produto")
                        .WithMany("ItensPedidos")
                        .HasForeignKey("IdProduto")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SoftCandy.Models.Pedido", b =>
                {
                    b.HasOne("SoftCandy.Models.Cliente", "Cliente")
                        .WithMany("Pedidos")
                        .HasForeignKey("IdCliente")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SoftCandy.Models.Vendedor", "Vendedor")
                        .WithMany("Pedidos")
                        .HasForeignKey("IdVendedor")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SoftCandy.Models.Produto", b =>
                {
                    b.HasOne("SoftCandy.Models.Categoria", "Categoria")
                        .WithMany("Produtos")
                        .HasForeignKey("IdCategoria")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SoftCandy.Models.Fornecedor", "Fornecedor")
                        .WithMany("Produtos")
                        .HasForeignKey("IdFornecedor")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
