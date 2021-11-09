function validarNomeProduto() {
    var nome = document.getElementById("nomeProduto").value;
    nome = nome.trim();
    if (nome === "" || nome.length < 3 || nome.length > 100) {
        document.getElementById("erroNomeProduto").className = "visivel";

        return false;
    }
    document.getElementById("erroNomeProduto").className = "invisivel";
    return true;
}

function validarPrecoVendaProduto() {
    var preco = document.getElementById("precoVenda").value;
    preco = preco.trim();
    if (preco === "" || preco.length < 0 || preco.length > 100) {
        document.getElementById("erroPrecoVenda").className = "visivel";

        return false;
    }
    document.getElementById("erroPrecoVenda").className = "invisivel";
    return true;
}

function validarQuantidadeProduto() {
    var quantidade = document.getElementById("quantidadeProduto").value;
    quantidade = quantidade.trim();
    if (quantidade === "" || quantidade.length < 0 || quantidade.length > 100) {
        document.getElementById("erroQuantidadeProduto").className = "visivel";

        return false;
    }
    document.getElementById("erroQuantidadeProduto").className = "invisivel";
    return true;
}

function validarQuantidadeMinimaProduto() {
    var quantidade = document.getElementById("quantidadeMinimaProduto").value;
    quantidade = quantidade.trim();
    if (quantidade === "" || quantidade.length < 0 || quantidade.length > 100) {
        document.getElementById("erroQuantidadeMinimaProduto").className = "visivel";

        return false;
    }
    document.getElementById("erroQuantidadeMinimaProduto").className = "invisivel";
    return true;
}

function validarProduto() {
    var nomeValido = validarNomeProduto();
    var precoValido = validarPrecoVendaProduto();
    var quantidadeValida = validarQuantidadeProduto();
    var quantidadeMinimaValida = validarQuantidadeMinimaProduto();
    return nomeValido;
}