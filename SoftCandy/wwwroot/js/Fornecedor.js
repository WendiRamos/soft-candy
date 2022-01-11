function validarCNPJ() {
    var cnpj = document.getElementById("cnpj").value;
    cnpj = cnpj.replace(/[^\d]+/g, '');

    if (cnpj == "") {
        document.getElementById("erroCnpj").className = "visivel";
        return false;
    }

    if (cnpj.length != 14) {
        document.getElementById("erroCnpj").className = "visivel";
        return false;
    }

    // Elimina CNPJs invalidos conhecidos
    if (cnpj == "00000000000000" ||
        cnpj == "11111111111111" ||
        cnpj == "22222222222222" ||
        cnpj == "33333333333333" ||
        cnpj == "44444444444444" ||
        cnpj == "55555555555555" ||
        cnpj == "66666666666666" ||
        cnpj == "77777777777777" ||
        cnpj == "88888888888888" ||
        cnpj == "99999999999999") {
        document.getElementById("erroCnpj").className = "visivel";
        return false;
    }

    // Valida DVs
    tamanho = cnpj.length - 2
    numeros = cnpj.substring(0, tamanho);
    digitos = cnpj.substring(tamanho);
    soma = 0;
    pos = tamanho - 7;
    for (i = tamanho; i >= 1; i--) {
        soma += numeros.charAt(tamanho - i) * pos--;
        if (pos < 2)
            pos = 9;
    }
    resultado = soma % 11 < 2 ? 0 : 11 - soma % 11;
    if (resultado != digitos.charAt(0)) {
        document.getElementById("erroCnpj").className = "visivel";
        return false;
    }

    tamanho = tamanho + 1;
    numeros = cnpj.substring(0, tamanho);
    soma = 0;
    pos = tamanho - 7;
    for (i = tamanho; i >= 1; i--) {
        soma += numeros.charAt(tamanho - i) * pos--;
        if (pos < 2)
            pos = 9;
    }
    resultado = soma % 11 < 2 ? 0 : 11 - soma % 11;
    if (resultado != digitos.charAt(1)) {
        document.getElementById("erroCnpj").className = "visivel";
        return false;
    }
    document.getElementById("erroCnpj").className = "invisivel";
    return true;
}


function validarRazaoSocial() {
    var razao = document.getElementById("razaoSocial").value;
    razao = razao.trim();
    if (razao === "" || razao.length < 10 || razao.length > 100) {
        document.getElementById("erroRazaoSocial").className = "visivel";

        return false;
    }
    document.getElementById("erroRazaoSocial").className = "invisivel";
    return true;
}

function validarNomeFantasia() {
    var nome = document.getElementById("nomeFantasia").value;
    nome = nome.trim();
    if (nome === "" || nome.length < 0 || nome.length > 50) {
        document.getElementById("erroNomeFantasia").className = "visivel";

        return false;
    }
    document.getElementById("erroNomeFantasia").className = "invisivel";
    return true;
}

function validarCelularFornecedor() {
    var celular = document.getElementById("celularFornecedor").value;
    celular = celular.trim();
    if (celular === "" || celular.length < 8 || celular.length > 20) {
        document.getElementById("erroCelularFornecedor").className = "visivel";
        return false;
    }
    document.getElementById("erroCelularFornecedor").className = "invisivel";
    return true;
}
function validarEmailFornecedor() {
    var email = document.getElementById("emailFornecedor").value;
    email = email.trim();
    if (email === "" || email.length < 12 || email.length > 50) {
        document.getElementById("erroEmailFornecedor").className = "visivel";
        return false;
    }
    document.getElementById("erroEmailFornecedor").className = "invisivel";
    return true;
}

function validarLogradouroFornecedor() {
    var Logradouro = document.getElementById("logradouroFornecedor").value;
    Logradouro = Logradouro.trim();
    if (Logradouro === "" || Logradouro.length < 2 || Logradouro.length > 100) {
        document.getElementById("erroLogradouroFornecedor").className = "visivel";
        return false;
    }
    document.getElementById("erroLogradouroFornecedor").className = "invisivel";
    return true;
}

function validarNumeroFornecedor() {
    var Numero = document.getElementById("numeroFornecedor").value;
    Numero = Numero.trim();
    if (Numero === "" || Numero.length < 2 || Numero.length > 5) {
        document.getElementById("erroNumeroFornecedor").className = "visivel";
        return false;
    }
    document.getElementById("erroNumeroFornecedor").className = "invisivel";
    return true;
}

function validarBairroFornecedor() {
    var Bairro = document.getElementById("bairroFornecedor").value;
    Bairro = Bairro.trim();
    if (Bairro === "" || Bairro.length < 3 || Bairro.length > 100) {
        document.getElementById("erroBairroFornecedor").className = "visivel";
        return false;
    }
    document.getElementById("erroBairroFornecedor").className = "invisivel";
    return true;
}

function validarCidadeFornecedor() {
    var Cidade = document.getElementById("cidadeFornecedor").value;
    Cidade = Cidade.trim();
    if (Cidade === "" || Cidade.length < 2 || Cidade.length > 30) {
        document.getElementById("erroCidadeFornecedor").className = "visivel";
        return false;
    }
    document.getElementById("erroCidadeFornecedor").className = "invisivel";
    return true;
}

function validarEstadoFornecedor() {
    var Estado = document.getElementById("estadoFornecedor").value;
    Estado = Estado.trim();
    if (Estado === "" || Estado.length < 2 || Estado.length > 20) {
        document.getElementById("erroEstadoFornecedor").className = "visivel";
        return false;
    }
    document.getElementById("erroEstadoFornecedor").className = "invisivel";
    return true;
}

function validarFornecedor() {
    var cnpjValido = validarCNPJ();
    var razaoValida = validarRazaoSocial();
    var nomeValido = validarNomeFantasia();
    var celularValido = validarCelularFornecedor();
    var emailValido = validarEmailFornecedor();
    var logradouroValido = validarLogradouroFornecedor();
    var numeroValido = validarNumeroFornecedor();
    var bairroValido = validarBairroFornecedor();
    var cidadeValida = validarCidadeFornecedor();
    var estadoValido = validarEstadoFornecedor();

    return cnpjValido && razaoValida && nomeValido && celularValido && emailValido && logradouroValido && numeroValido && bairroValido && cidadeValida && estadoValido;
}