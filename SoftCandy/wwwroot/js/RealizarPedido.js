// Array de itens de pedido
var itens = [];

/****************** VERIFICA SE O PRODUTO JÁ FOI ADICIONADO *******************/

function itensContem(produto) {
    return itens.find((i) => i.Cod_Produto === produto.Cod_Produto);
}

/********************** CALCULA O VALOR TOTAL DO PEDIDO ***********************/

function calcula() {
    const total = itens.reduce((s, v) => s + v.Preco_Pago * v.Quantidade, 0.0);
    document.getElementById("total-pedido").textContent = dinheiro(total);
}

/*********************** ADICIONA UM PRODUTO AO PEDIDO ************************/

function adicionar(produto) {
    // Se um produto já foi adicionado, retorna sem executar a função
    if (itensContem(produto)) return;

    // Cria elementos HTML de texto
    const cod = document.createTextNode(produto.Cod_Produto);
    const nome = document.createTextNode(produto.Nome_Produto);
    const preco = document.createTextNode(dinheiro(produto.Preco_Venda));
    const sub = document.createTextNode(dinheiro(produto.Preco_Venda));
    const qnt = document.createElement("input");
    qnt.type = "number";
    qnt.value = "1";
    qnt.min = "1";
    qnt.max = produto.Quantidade;

    // Adiciona um evento para alterar os valores quando mudar a quantidade
    qnt.addEventListener("input", () => {
        // "Descobre" o id da linha do input alterado acessando o elemento avô
        const id = Number(
            qnt.parentNode.parentNode.querySelector(".codigo").textContent
        );
        // Muda, no array de itens, o item com quantidade alterada
        itens = itens.map((i) => {
            return i.Cod_Produto === id ? { ...i, Quantidade: Number(qnt.value) } : i;
        });
        // Calcula e mostra novo subtotal
        sub.textContent = dinheiro(produto.Preco_Venda * qnt.value);
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
    const celulaQuantidade = linha.insertCell();
    const celulaSubtotal = linha.insertCell();

    // Adiciona os elementos de texto dentro de sua célula
    celulaCod.appendChild(cod);
    celulaNome.appendChild(nome);
    celulaPreco.appendChild(preco);
    celulaQuantidade.appendChild(qnt);
    celulaSubtotal.appendChild(sub);

    // Cria um item de pedido e adiciona ao array de itens
    const tmp = {};
    tmp.Quantidade = 1;
    tmp.Preco_Pago = produto.Preco_Venda;
    tmp.Cod_Produto = produto.Cod_Produto;
    tmp.Num_Pedido = 0;
    itens = [...itens, tmp];

    calcula();

    // Habilita o botão de envio
    $("#enviar").prop("disabled", false)
}

function enviar() {
    console.log(itens)
    const send = JSON.stringify({"itensL": itens});
    $.ajax({
        contentType: 'application/json; charset=utf-8',
        dataType: "json",
        type: 'POST',
        url: '/Pedido/Create',
        data: send,
        success: function () {
            alert("complete");
        }
    });
}