function validarNomeVendedor() {
    var nome = document.getElementById("nomeVendedor").value;
    nome = nome.trim();
    if (nome === "" || nome.length < 9 || nome.length > 50) {
        document.getElementById("erroNomeVendedor").className = "visivel";

        return false;
    }
    document.getElementById("erroNomeVendedor").className = "invisivel";
    return true;
}

function validarCelularVendedor() {
    var celular = document.getElementById("celularVendedor").value;
    celular = celular.trim();
    if (celular === "" || celular.length < 8 || celular.length > 20) {
        document.getElementById("erroCelularVendedor").className = "visivel";
        return false;
    }
    document.getElementById("erroCelularVendedor").className = "invisivel";
    return true;
}

function validarLogradouroVendedor() {
    var celular = document.getElementById("logradouroVendedor").value;
    celular = celular.trim();
    if (celular === "" || celular.length < 2 || celular.length > 100) {
        document.getElementById("erroLogradouroVendedor").className = "visivel";
        return false;
    }
    document.getElementById("erroLogradouroVendedor").className = "invisivel";
    return true;
}

function validarNumeroVendedor() {
    var celular = document.getElementById("numeroVendedor").value;
    celular = celular.trim();
    if (celular === "" || celular.length < 2 || celular.length > 5) {
        document.getElementById("erroNumeroVendedor").className = "visivel";
        return false;
    }
    document.getElementById("erroNumeroVendedor").className = "invisivel";
    return true;
}

function validarBairroVendedor() {
    var celular = document.getElementById("bairroVendedor").value;
    celular = celular.trim();
    if (celular === "" || celular.length < 3 || celular.length > 100) {
        document.getElementById("erroBairroVendedor").className = "visivel";
        return false;
    }
    document.getElementById("erroBairroVendedor").className = "invisivel";
    return true;
}

function validarCidadeVendedor() {
    var celular = document.getElementById("cidadeVendedor").value;
    celular = celular.trim();
    if (celular === "" || celular.length < 2 || celular.length > 30) {
        document.getElementById("erroCidadeVendedor").className = "visivel";
        return false;
    }
    document.getElementById("erroCidadeVendedor").className = "invisivel";
    return true;
}

function validarEstadoVendedor() {
    var celular = document.getElementById("estadoVendedor").value;
    celular = celular.trim();
    if (celular === "" || celular.length < 2 || celular.length > 20) {
        document.getElementById("erroEstadoVendedor").className = "visivel";
        return false;
    }
    document.getElementById("erroEstadoVendedor").className = "invisivel";
    return true;
}

function validarEmailVendedor() {
    var email = document.getElementById("emailVendedor").value;
    email = email.trim();
    if (email === "" || email.length < 12 || email.length > 50) {
        document.getElementById("erroEmailVendedor").className = "visivel";
        return false;
    }
    document.getElementById("erroEmailVendedor").className = "invisivel";
    return true;
}

function validarSenhaVendedor() {

    var senha = document.getElementById("senhaVendedor").value;
    senha = senha.trim();
    if (senha === "" || senha.length < 8 || senha.length > 25) {
        document.getElementById("erroSenhaVendedor").className = "visivel";
        return false;
    }
    document.getElementById("erroSenhaVendedor").className = "invisivel";
    return true;
}

function confirmarSenhaVendedor() {
    var senhaConfirma = document.getElementById("senhaConfirmaVendedor").value;
    senhaConfirma = senhaConfirma.trim();
    if (senhaConfirma === "" || senhaConfirma.length < 8 || senhaConfirma.length > 25) {
        document.getElementById("erroSenhaConfirmaVendedor").textContent = "Insira uma confirmação de senha válida!";
        document.getElementById("erroSenhaConfirmaVendedor").className = "visivel";
        return false;
    }
    var senha = document.getElementById("senhaVendedor").value;
    if (senha !== senhaConfirma) {
        document.getElementById("erroSenhaConfirmaVendedor").textContent = "As senhas não coincidem!";
        document.getElementById("erroSenhaConfirmaVendedor").className = "visivel";
        return false;
    }
    document.getElementById("erroSenhaConfirmaVendedor").className = "invisivel";
    return true;
}

function validarVendedor() {
    var nomeValido = validarNomeVendedor();
    var celularValido = validarCelularVendedor();
    var logradouroValido = validarLogradouroVendedor();
    var numeroValido = validarNumeroVendedor();
    var bairroValido = validarBairroVendedor();
    var cidadeValida = validarCidadeVendedor();
    var estadoValido = validarEstadoVendedor();
    var emailValido = validarEmailVendedor();
    var senhaValida = validarSenhaVendedor();
    var confirmaSenha = confirmarSenhaVendedor();

    return nomeValido && celularValido && logradouroValido && numeroValido && bairroValido && cidadeValida && estadoValido && emailValido && senhaValida && confirmaSenha;
}

function validarLoginVendedor() {
    var emailValido = validarEmailVendedor();
    var senhaValida = validarSenhaVendedor();

    return emailValido && senhaValida;
}
