var itens = [];

function itensContem(produto) {
    return itens.find((i) => i.Cod_Produto === produto.Cod_Produto);
}

function calcula() {
    const tds = document.getElementsByClassName("subtotal");
    const lista = [...tds];
    const total = lista.reduce((s, v) => s + Number(v.innerHTML), 0.0);
    document.getElementById("total-pedido").textContent = total;
}

function adicionar(produto) {
    if (itensContem(produto)) return;

    const cod = document.createTextNode(produto.Cod_Produto);
    const nome = document.createTextNode(produto.Nome_Produto);
    const preco = document.createTextNode(produto.Preco_Venda);
    const sub = document.createTextNode(produto.Preco_Venda);
    const qnt = document.createElement("input");
    qnt.type = "number";
    qnt.value = "1";
    qnt.min = "1";
    qnt.max = produto.Quantidade;
    qnt.addEventListener("input", () => {
        const id = Number(
            qnt.parentNode.parentNode.querySelector(".codigo").textContent
        );
        itens = itens.map((i) => {
            return i.Cod_Produto === id ? { ...i, Quantidade: Number(qnt.value) } : i;
        });
        sub.textContent = produto.Preco_Venda * qnt.value;
        calcula();
    });

    const tabela = document.getElementById("itens-pedido");
    const linha = tabela.insertRow();

    const celulaCod = linha.insertCell();
    celulaCod.className = "codigo";
    const celulaNome = linha.insertCell();
    const celulaPreco = linha.insertCell();
    const celulaQuantidade = linha.insertCell();
    celulaQuantidade.className = "quantidade";
    const celulaSubtotal = linha.insertCell();
    celulaSubtotal.className = "subtotal";

    celulaCod.appendChild(cod);
    celulaNome.appendChild(nome);
    celulaPreco.appendChild(preco);
    celulaQuantidade.appendChild(qnt);
    celulaSubtotal.appendChild(sub);

    const tmp = {};
    tmp.Quantidade = 1;
    tmp.Preco_Pago = produto.Preco_Venda;
    tmp.Cod_Produto = produto.Cod_Produto;
    tmp.Num_Pedido = null;

    itens = [...itens, tmp];

    calcula();
}