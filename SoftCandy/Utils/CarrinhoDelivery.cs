using SoftCandy.Enums;
using SoftCandy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftCandy.Utils
{
    public sealed class CarrinhoDelivery
    {
        static CarrinhoDelivery _instancia;

        public int Id { get; set; } = 1;

        public string NomeCliente { get; set; }

        public decimal ValorTotal { get; set; }
        public decimal ValorFrete { get; set; }
        public DateTime DataHoraCriacao { get; set; }

        public string EnderecoEntrega { get; set; }

        public int IdMotoboy { get; set; }

        public FormasPagamentoEnum FormaPagamento { get; set; }

        public ICollection<ItemDelivery> ItensDelivery { get; set; } = new List<ItemDelivery>();

        public static CarrinhoDelivery Instancia
        {
            get { return _instancia ?? (_instancia = new CarrinhoDelivery()); }
        }

        private CarrinhoDelivery() { }

        public void CalcularTotal() {
            ValorTotal = ItensDelivery.Select(i => i.Lote.PrecoVenda * i.Quantidade).Sum() + ValorFrete;
        }

        public void AdicionarItem(ItemDelivery item)
        {
            ItensDelivery.Add(item);
            Id++;
            CalcularTotal();
        }

        public void RemoverItem(ItemDelivery item)
        {
            ItensDelivery.Remove(item);
            CalcularTotal();
        }

        public void LimparItens()
        {
            ItensDelivery.Clear();
        }

        public bool LoteIdJaEstaNoCarrinho(int loteId)
        {
            return ItensDelivery.Any(i => i.Lote.Id == loteId);
        }
    }
}
