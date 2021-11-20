function validarNomeAdministrador() {
    var nome = document.getElementById("nomeAdministrador").value;
    nome = nome.trim();
    if (nome === "" || nome.length < 9 || nome.length > 50) {
        document.getElementById("erroNomeAdministrador").className = "visivel";

        return false;
    }
    document.getElementById("erroNomeAdministrador").className = "invisivel";
    return true;
}

function validarCelularAdministrador() {
    var celular = document.getElementById("celularAdministrador").value;
    celular = celular.trim();
    if (celular === "" || celular.length < 8 || celular.length > 12) {
        document.getElementById("erroCelularAdministrador").className = "visivel";
        return false;
    }
    document.getElementById("erroCelularAdministrador").className = "invisivel";
    return true;
}
function validarEmailAdministrador() {
    var email = document.getElementById("emailAdministrador").value;
    email = email.trim();
    if (email === "" || email.length < 12 || email.length > 50) {
        document.getElementById("erroEmailAdministrador").className = "visivel";
        return false;
    }
    document.getElementById("erroEmailAdministrador").className = "invisivel";
    return true;
}

function validarLogradouroAdministrador() {
    var celular = document.getElementById("logradouroAdministrador").value;
    celular = celular.trim();
    if (celular === "" || celular.length < 8 || celular.length > 100) {
        document.getElementById("erroLogradouroAdministrador").className = "visivel";
        return false;
    }
    document.getElementById("erroLogradouroAdministrador").className = "invisivel";
    return true;
}

function validarNumeroAdministrador() {
    var celular = document.getElementById("numeroAdministrador").value;
    celular = celular.trim();
    if (celular === "" || celular.length < 2 || celular.length > 5) {
        document.getElementById("erroNumeroAdministrador").className = "visivel";
        return false;
    }
    document.getElementById("erroNumeroAdministrador").className = "invisivel";
    return true;
}

function validarBairroAdministrador() {
    var celular = document.getElementById("bairroAdministrador").value;
    celular = celular.trim();
    if (celular === "" || celular.length < 8 || celular.length > 100) {
        document.getElementById("erroBairroAdministrador").className = "visivel";
        return false;
    }
    document.getElementById("erroBairroAdministrador").className = "invisivel";
    return true;
}

function validarCidadeAdministrador() {
    var celular = document.getElementById("cidadeAdministrador").value;
    celular = celular.trim();
    if (celular === "" || celular.length < 2 || celular.length > 30) {
        document.getElementById("erroCidadeAdministrador").className = "visivel";
        return false;
    }
    document.getElementById("erroCidadeAdministrador").className = "invisivel";
    return true;
}

function validarEstadoAdministrador() {
    var celular = document.getElementById("estadoAdministrador").value;
    celular = celular.trim();
    if (celular === "" || celular.length < 2 || celular.length > 20) {
        document.getElementById("erroEstadoAdministrador").className = "visivel";
        return false;
    }
    document.getElementById("erroEstadoAdministrador").className = "invisivel";
    return true;
}

function validarSenhaAdministrador() {

    var senha = document.getElementById("senhaAdministrador").value;
    senha = senha.trim();
    if (senha === "" || senha.length < 8 || senha.length > 25) {
        document.getElementById("erroSenhaAdministrador").className = "visivel";
        return false;
    }
    document.getElementById("erroSenhaAdministrador").className = "invisivel";
    return true;
}

function confirmarSenhaAdministrador() {
    var senhaConfirma = document.getElementById("senhaConfirmaAdministrador").value;
    senhaConfirma = senhaConfirma.trim();
    if (senhaConfirma === "" || senhaConfirma.length < 8 || senhaConfirma.length > 25) {
        document.getElementById("erroSenhaConfirmaAdministrador").textContent = "Insira uma confirmação de senha válida!";
        document.getElementById("erroSenhaConfirmaAdministrador").className = "visivel";
        return false;
    }
    var senha = document.getElementById("senhaAdministrador").value;
    if (senha !== senhaConfirma) {
        document.getElementById("erroSenhaConfirmaAdministrador").textContent = "As senhas não coincidem!";
        document.getElementById("erroSenhaConfirmaAdministrador").className = "visivel";
        return false;
    }
    document.getElementById("erroSenhaConfirmaAdministrador").className = "invisivel";
    return true;
}

function validarAdministrador() {
    var nomeValido = validarNomeAdministrador();
    var celularValido = validarCelularAdministrador();
    var logradouroValido = validarLogradouroAdministrador();
    var numeroValido = validarNumeroAdministrador();
    var bairroValido = validarBairroAdministrador();
    var cidadeValida = validarCidadeAdministrador();
    var estadoValido = validarEstadoAdministrador();
    var emailValido = validarEmailAdministrador();
    var senhaValida = validarSenhaAdministrador();
    var confirmaSenha = confirmarSenhaAdministrador();

    return nomeValido && celularValido && logradouroValido && numeroValido && bairroValido && cidadeValida && estadoValido && emailValido && senhaValida && confirmaSenha;
}

function validarLoginAdministrador() {
    var emailValido = validarEmailAdministrador();
    var senhaValida = validarSenhaAdministrador();

    return emailValido && senhaValida;
}
