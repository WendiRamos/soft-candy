using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using SoftCandy.Enums;
using SoftCandy.Models;

namespace SoftCandy.Utils
{
    public static class Validacao
    {
        //Apenas funcionario ativos
        public static bool IsFuncionarioAtivo(Funcionario funcionario)
        {
            return funcionario.Ativo;
        }

        //Apenas funcionarios com seu cargo
        public static bool IsAdministrador(Funcionario funcionario)
        {
            return funcionario.Cargo == (int)CargosEnum.ADMINISTRADOR;
        }

        public static bool IsEstoquista(Funcionario funcionario)
        {
            return funcionario.Cargo == (int)CargosEnum.ESTOQUISTA;
        }

        public static bool IsVendedor(Funcionario funcionario)
        {
            return funcionario.Cargo == (int)CargosEnum.VENDEDOR;
        }

        //Apenas Funcionarios com seus cargos e ativo

        public static bool IsAdministradorAtivo(Funcionario funcionario)
        {
            return IsAdministrador(funcionario) && IsFuncionarioAtivo(funcionario);
        }

        public static bool IsEstoquistaAtivo(Funcionario funcionario)
        {
            return IsEstoquista(funcionario) && IsFuncionarioAtivo(funcionario);
        }

        public static bool IsVendedorAtivo(Funcionario funcionario)
        {
            return IsVendedor(funcionario) && IsFuncionarioAtivo(funcionario);
        }

        //Apenas Funcionarios com seus cargos e inativos
        public static bool IsAdministradorInativo(Funcionario funcionario)
        {
            return IsAdministrador(funcionario) && !IsFuncionarioAtivo(funcionario);
        }

        public static bool IsEstoquistaInativo(Funcionario funcionario)
        {
            return IsEstoquista(funcionario) && !IsFuncionarioAtivo(funcionario);
        }
        public static bool IsVendedorInativo(Funcionario funcionario)
        {
            return IsVendedor(funcionario) && !IsFuncionarioAtivo(funcionario);
        }

        //Produtos
        public static bool IsProdutoAtivo(Produto produto)
        {
            return produto.Ativo;
        }

        public static bool IsProdutoInativo(Produto produto)
        {
            return !IsProdutoAtivo(produto);
        }

        //Fornecedor
        public static bool IsFornecedorAtivo(Fornecedor fornecedor)
        {
            return fornecedor.AtivoFornecedor;
        }

        public static bool IsFornecedorInativo(Fornecedor fornecedor)
        {
            return !IsFornecedorAtivo(fornecedor);
        }


        //Categoria
        public static bool IsCategoriaAtivo(Categoria categoria)
        {
            return categoria.AtivoCategoria;
        }

        public static bool IsCategoriaInativo(Categoria categoria)
        {
            return !IsCategoriaAtivo(categoria);
        }
    }
}
