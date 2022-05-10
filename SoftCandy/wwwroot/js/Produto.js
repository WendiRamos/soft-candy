function validarNome() {
    var nome = document.getElementById("nomeProduto").value;
    nome = nome.trim();
    if (nome === "" || nome.length < 3 || nome.length > 100) {
        document.getElementById("erroNome").className = "visivel";

        return false;
    }
    document.getElementById("erroNome").className = "invisivel";
    return true;
}

function validarPreco() {
    var preco = document.getElementById("precoVenda").value;
    preco = preco.trim();
    if (preco === "" || preco.length < 0 || preco.length > 100) {
        document.getElementById("erroPreco").className = "visivel";

        return false;
    }
    document.getElementById("erroPreco").className = "invisivel";
    return true;
}

function validarQuantidadeDescartada() {
    var quantidade = document.getElementById("quantidadeProduto").value;
    quantidade = quantidade.trim();
    if (quantidade === "" || quantidade.length < 0 || quantidade.length > 100) {
        document.getElementById("erroQuantidadeDescartada").className = "visivel";

        return false;
    }
    document.getElementById("erroQuantidadeDescartada").className = "invisivel";
    return true;
}

function validarQuantidadeMinima() {
    var quantidade = document.getElementById("quantidadeMinimaProduto").value;
    quantidade = quantidade.trim();
    if (quantidade === "" || quantidade.length < 0 || quantidade.length > 100) {
        document.getElementById("erroQuantidadeMinima").className = "visivel";

        return false;
    }
    document.getElementById("erroQuantidadeMinima").className = "invisivel";
    return true;
}

function validarProduto() {
    var nomeValido = validarNome();
    var precoValido = validarPreco();
    var quantidadeValida = validarQuantidadeDescartada();
    var quantidadeMinimaValida = validarQuantidadeMinima();
    return nomeValido && precoValido && quantidadeValida && quantidadeMinimaValida;
}