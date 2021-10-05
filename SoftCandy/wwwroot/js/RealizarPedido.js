// Array de itens de pedido
var itens = [];

/****************** VERIFICA SE O PRODUTO JÁ FOI ADICIONADO *******************/

function itensContem(produto) {
    return itens.find((i) => i.IdProduto === produto.IdProduto);
}

/********************** CALCULA O VALOR TOTAL DO PEDIDO ***********************/

function calcula() {
    const total = itens.reduce((s, v) => s + v.PrecoPago * v.QuantidadeProduto, 0.0);
    document.getElementById("total-pedido").textContent = dinheiro(total);
}

/*********************** ADICIONA UM PRODUTO AO PEDIDO ************************/

function adicionar(produto) {
    // Se um produto já foi adicionado, retorna sem executar a função
    if (itensContem(produto)) return;

    // Cria elementos HTML de texto
    const cod = document.createTextNode(produto.IdProduto);
    const nome = document.createTextNode(produto.NomeProduto);
    const preco = document.createTextNode(dinheiro(produto.PrecoVendaProduto));
    const sub = document.createTextNode(dinheiro(produto.PrecoVendaProduto));
    const qnt = document.createElement("input");
    qnt.className = "entrada-transparente";
    qnt.type = "number";
    qnt.value = "1";
    qnt.min = "1";
    qnt.max = produto.QuantidadeProduto;

    // Adiciona um evento para alterar os valores quando mudar a QuantidadeProduto
    qnt.addEventListener("input", () => {
        // "Descobre" o id da linha do input alterado acessando o elemento avô
        const id = Number(
            qnt.parentNode.parentNode.querySelector(".codigo").textContent
        );
        // Muda, no array de itens, o item com QuantidadeProduto alterada
        itens = itens.map((i) => {
            return i.IdProduto === id ? { ...i, QuantidadeProduto: Number(qnt.value) } : i;
        });
        // Calcula e mostra novo subtotal
        sub.textContent = dinheiro(produto.PrecoVendaProduto * qnt.value);
        calcula();
    });

    // Selecione a tabela no HTML (somente o tbody)
    const tabela = document.getElementById("itens-pedido");

    // Cria uma linha na tabela
    const linha = tabela.insertRow();

    // Cria várias células/colunas já dentro da linha criada
    const celulaCod = linha.insertCell();
    celulaCod.className = "codigo";
    const celulaNome = linha.insertCell();
    const celulaPreco = linha.insertCell();
    const celulaQuantidadeProduto = linha.insertCell();
    const celulaSubtotal = linha.insertCell();

    // Adiciona os elementos de texto dentro de sua célula
    celulaCod.appendChild(cod);
    celulaNome.appendChild(nome);
    celulaPreco.appendChild(preco);
    celulaQuantidadeProduto.appendChild(qnt);
    celulaSubtotal.appendChild(sub);

    // Cria um item de pedido e adiciona ao array de itens
    const tmp = {};
    tmp.QuantidadeProduto = 1;
    tmp.PrecoPago = produto.PrecoVendaProduto;
    tmp.IdProduto = produto.IdProduto;
    tmp.IdPedido = 0;
    itens = [...itens, tmp];

    calcula();

    // Habilita o botão de envio
    $("#enviar").prop("disabled", false)
}

function enviar() {
    itens = itens.map((i) => ({ ...i, PrecoPago: i.PrecoPago.toString().replace(".", ",") }));
    const id = $("select").val();

    $.ajax({
        url: "/Pedido/Create/",
        type: "POST",
        data: { "Itens": itens, "IdCliente": id },
        success: function (id) {
            Swal.fire({
                title: "Sucesso!",
                text: "Seu pedido foi realizado.",
                icon: "success"
            }).then(() => window.location.href = '/Pedido/Index');
        },
        error: function () {
            Swal.fire({
                title: "Ocorreu um erro!",
                text: "Tente novamente.",
                icon: "error"
            })
        }
    });
}