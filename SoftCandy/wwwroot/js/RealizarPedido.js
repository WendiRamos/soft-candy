let produtos = [];
let itensPedido = [];
let valorTotalPedido = 0.0;

const botaoEnviar = $("#enviar");
const tabelaProdutos = $("#tabelaProdutos");
const tabelaItensPedido = $("#itens-pedido");
const campoValorTotalPedido = $("#total-pedido");

/****************** VERIFICA SE O PRODUTO JÁ FOI ADICIONADO *******************/

function itensContem(idProdAdd) {
    return itensPedido.some((i) => i.idProduto === idProdAdd);
}

/************************ ADICIONAR PRODUTO AO PEDIDO *************************/

function adicionarProdutoAoPedido(idProdAdd) {
    if (itensContem(idProdAdd)) return;

    const item = produtos.find((p) => p.idProduto === idProdAdd);

    if (item !== null) {
        const novoItem = {
            ...item,
            quantidade: 1,
            subtotal: item.precoVendaProduto,
        };

        itensPedido = [...itensPedido, novoItem];
        atualizarTabelaItensPedido();
    }
}

/******************************************************************************/

function removerProdutoDoPedido(idProdRem) {
    itensPedido = itensPedido.filter((i) => i.idProduto !== idProdRem);
    atualizarTabelaItensPedido();
}

/******************************************************************************/

function atualizarValorTotalPedido() {
    valorTotalPedido = itensPedido.reduce(
        (s, i) => s + i.precoVendaProduto * i.quantidade,
        0.0
    );
    campoValorTotalPedido.html(dinheiro(valorTotalPedido));
}

/******************************************************************************/

function adicionarLinhaTabelaItensPedido(item) {
    const id = document.createTextNode(item.idProduto);
    const nome = document.createTextNode(item.nomeProduto);
    const preco = document.createTextNode(dinheiro(item.precoVendaProduto));
    const sub = document.createTextNode(dinheiro(item.subtotal));
    const qnt = document.createElement("input");
    qnt.className = "entrada-transparente tira-borda";
    qnt.min = "1";
    qnt.type = "number";
    qnt.value = item.quantidade;
    qnt.max = item.quantidadeProduto;
    qnt.onkeydown = () => { return false };

    qnt.addEventListener("input", () => {
        // Atualiza vetor de itens de pedido
        itensPedido = itensPedido.map((i) => {
            return i.idProduto !== item.idProduto
                ? i
                : {
                    ...i,
                    quantidade: Number(qnt.value),
                    subtotal: Number(qnt.value) * i.precoVendaProduto
                }
        });
        atualizarTabelaItensPedido();
    });

    const tabela = document.getElementById("itens-pedido");

    // Cria uma linha na tabela
    const linha = tabela.insertRow();

    // Cria várias células/colunas já dentro da linha criada
    const celulaId = linha.insertCell();
    celulaId.className = "codigo";
    const celulaNome = linha.insertCell();
    const celulaPreco = linha.insertCell();
    const celulaQuantidadeProduto = linha.insertCell();
    const celulaSubtotal = linha.insertCell();
    const celulaBotao = linha.insertCell();
    const botaoRemover = document.createElement("button");
    botaoRemover.innerText = "Remover";
    botaoRemover.className = "btn btn-secondary";
    botaoRemover.addEventListener("click", () =>
        removerProdutoDoPedido(item.idProduto)
    );

    // Adiciona os elementos de texto dentro de sua célula
    celulaId.appendChild(id);
    celulaNome.appendChild(nome);
    celulaPreco.appendChild(preco);
    celulaQuantidadeProduto.appendChild(qnt);
    celulaSubtotal.appendChild(sub);
    celulaBotao.appendChild(botaoRemover);
}

/******************************************************************************/

function controlarBotaoDePedido() {
    botaoEnviar.prop("disabled", !itensPedido.length);
}

/******************************************************************************/

function atualizarTabelaItensPedido() {
    tabelaItensPedido.empty();
    itensPedido.forEach(adicionarLinhaTabelaItensPedido);
    controlarBotaoDePedido();
    atualizarValorTotalPedido();
}

/******************************************************************************/

function adicionarLinhaTabelaProdutos(produto) {
    tabelaProdutos.append(
        $("<tr>")
            .append($("<td>").append(produto.idProduto))
            .append($("<td>").append(produto.nomeProduto))
            .append($("<td>").append(produto.quantidadeProduto))
            .append($("<td>").append(dinheiro(produto.precoVendaProduto)))
            .append(
                $("<td>").append(
                    $("<button>")
                        .append("Adicionar")
                        .click(() => adicionarProdutoAoPedido(produto.idProduto))
                        .addClass("btn btn-secondary")
                )
            )
    );
}

/******************************************************************************/

function procurarProdutos() {
    const termoPesquisa = $("#pesquisar").val() || "";

    tabelaProdutos.empty();

    $.ajax({
        url: "/Pedido/BuscarProdutoPorNomeTop5/?TermoProcurado=" + termoPesquisa,
        success: function (listaProdutos) {
            produtos = [ ...listaProdutos ];
            listaProdutos.forEach(adicionarLinhaTabelaProdutos);
        },
    });
}

/******************************************************************************/

function enviarPedido() {
    const id = $("select").val();
    const itens = itensPedido.map((i) => ({
        Quantidade: i.quantidade,
        IdProduto: i.idProduto,
    }));

    $.ajax({
        url: "/Pedido/Create/",
        type: "POST",
        data: { Itens: itens, IdCliente: id },
        success: (idPedido) => {
            if (idPedido) {
                Swal.fire("Sucesso!", "Seu pedido foi realizado.", "success").then(
                    () => (window.location.href = "/Pedido/Details/" + idPedido)
                );
            } else {
                Swal.fire("Ocorreu um erro!", "Tente novamente.", "error");
            }
        },
    });
}

/******************************************************************************/

procurarProdutos();
controlarBotaoDePedido();
atualizarValorTotalPedido();
