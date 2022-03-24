function validarNome() {
    var nome = document.getElementById("nome").value;
    nome = nome.trim();
    if (nome === "" || nome.length < 9 || nome.length > 50) {
        document.getElementById("erroNome").className = "visivel";

        return false;
    }
    document.getElementById("erroNome").className = "invisivel";
    return true;
}

function validarCelular() {
    var celular = document.getElementById("celular").value;
    celular = celular.trim();
    if (celular === "" || celular.length < 8 || celular.length > 20) {
        document.getElementById("erroCelular").className = "visivel";
        return false;
    }
    document.getElementById("erroCelular").className = "invisivel";
    return true;
}
function validarEmail() {
    var email = document.getElementById("email").value;
    email = email.trim();
    if (email === "" || email.length < 12 || email.length > 500) {
        document.getElementById("erroEmail").className = "visivel";
        return false;
    }
    document.getElementById("erroEmailr").className = "invisivel";
    return true;
}

function validarLogradouro() {
    var celular = document.getElementById("logradouro").value;
    celular = celular.trim();
    if (celular === "" || celular.length < 3 || celular.length > 100) {
        document.getElementById("erroLogradouro").className = "visivel";
        return false;
    }
    document.getElementById("erroLogradouro").className = "invisivel";
    return true;
}

function validarNumero() {
    var celular = document.getElementById("numero").value;
    celular = celular.trim();
    if (celular === "" || celular.length < 2 || celular.length > 5) {
        document.getElementById("erroNumero").className = "visivel";
        return false;
    }
    document.getElementById("erroNumero").className = "invisivel";
    return true;
}

function validarBairro() {
    var celular = document.getElementById("bairro").value;
    celular = celular.trim();
    if (celular === "" || celular.length < 3 || celular.length > 100) {
        document.getElementById("erroBairro").className = "visivel";
        return false;
    }
    document.getElementById("erroBairro").className = "invisivel";
    return true;
}

function validarCidade() {
    var celular = document.getElementById("cidade").value;
    celular = celular.trim();
    if (celular === "" || celular.length < 2 || celular.length > 30) {
        document.getElementById("erroCidade").className = "visivel";
        return false;
    }
    document.getElementById("erroCidade").className = "invisivel";
    return true;
}

function validarEstado() {
    var celular = document.getElementById("estado").value;
    celular = celular.trim();
    if (celular === "" || celular.length < 2 || celular.length > 20) {
        document.getElementById("erroEstado").className = "visivel";
        return false;
    }
    document.getElementById("erroEstado").className = "invisivel";
    return true;
}

function validarSenha() {

    var senha = document.getElementById("senha").value;
    senha = senha.trim();
    if (senha === "" || senha.length < 8 || senha.length > 25) {
        document.getElementById("erroSenha").className = "visivel";
        return false;
    }
    document.getElementById("erroSenha").className = "invisivel";
    return true;
}

function confirmarSenha() {
    var senhaConfirma = document.getElementById("senhaConfirma").value;
    senhaConfirma = senhaConfirma.trim();
    if (senhaConfirma === "" || senhaConfirma.length < 8 || senhaConfirma.length > 25) {
        document.getElementById("erroSenhaConfirma").textContent = "Insira uma confirmação de senha válida!";
        document.getElementById("erroSenhaConfirma").className = "visivel";
        return false;
    }
    var senha = document.getElementById("senha").value;
    if (senha !== senhaConfirma) {
        document.getElementById("erroSenhaConfirma").textContent = "As senhas não coincidem!";
        document.getElementById("erroSenhaConfirma").className = "visivel";
        return false;
    }
    document.getElementById("erroSenhaConfirma").className = "invisivel";
    return true;
}

function validarFuncionario() {
    var nomeValido = validarNome();
    var celularValido = validarCelular();
    var logradouroValido = validarLogradouro();
    var numeroValido = validarNumero();
    var bairroValido = validarBairro();
    var cidadeValida = validarCidade();
    var estadoValido = validarEstado();
    var emailValido = validarEmail();
    var senhaValida = validarSenha();
    var confirmaSenha = confirmarSenha();

    return nomeValido && celularValido && logradouroValido && numeroValido && bairroValido && cidadeValida && estadoValido && emailValido && senhaValida && confirmaSenha;
}

function validarLogin() {
    var emailValido = validarEmail();
    var senhaValida = validarSenha();

    return emailValido && senhaValida;
}
