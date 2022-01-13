function validarNomeEstoquista() {
    var nome = document.getElementById("nomeEstoquista").value;
    nome = nome.trim();
    if (nome === "" || nome.length < 9 || nome.length > 50) {
        document.getElementById("erroNomeEstoquista").className = "visivel";

        return false;
    }
    document.getElementById("erroNomeEstoquista").className = "invisivel";
    return true;
}

function validarCelularEstoquista() {
    var celular = document.getElementById("celularEstoquista").value;
    celular = celular.trim();
    if (celular === "" || celular.length < 8 || celular.length > 20) {
        document.getElementById("erroCelularEstoquista").className = "visivel";
        return false;
    }
    document.getElementById("erroCelularEstoquista").className = "invisivel";
    return true;
}
function validarEmailEstoquista() {
    var email = document.getElementById("emailEstoquista").value;
    email = email.trim();
    if (email === "" || email.length < 12 || email.length > 500) {
        document.getElementById("erroEmailEstoquista").className = "visivel";
        return false;
    }
    document.getElementById("erroEmailEstoquista").className = "invisivel";
    return true;
}

function validarLogradouroEstoquista() {
    var celular = document.getElementById("logradouroEstoquista").value;
    celular = celular.trim();
    if (celular === "" || celular.length < 2 || celular.length > 100) {
        document.getElementById("erroLogradouroEstoquista").className = "visivel";
        return false;
    }
    document.getElementById("erroLogradouroEstoquista").className = "invisivel";
    return true;
}

function validarNumeroEstoquista() {
    var celular = document.getElementById("numeroEstoquista").value;
    celular = celular.trim();
    if (celular === "" || celular.length < 2 || celular.length > 5) {
        document.getElementById("erroNumeroEstoquista").className = "visivel";
        return false;
    }
    document.getElementById("erroNumeroEstoquista").className = "invisivel";
    return true;
}

function validarBairroEstoquista() {
    var celular = document.getElementById("bairroEstoquista").value;
    celular = celular.trim();
    if (celular === "" || celular.length < 3 || celular.length > 100) {
        document.getElementById("erroBairroEstoquista").className = "visivel";
        return false;
    }
    document.getElementById("erroBairroEstoquista").className = "invisivel";
    return true;
}

function validarCidadeEstoquista() {
    var celular = document.getElementById("cidadeEstoquista").value;
    celular = celular.trim();
    if (celular === "" || celular.length < 2 || celular.length > 30) {
        document.getElementById("erroCidadeEstoquista").className = "visivel";
        return false;
    }
    document.getElementById("erroCidadeEstoquista").className = "invisivel";
    return true;
}

function validarEstadoEstoquista() {
    var celular = document.getElementById("estadoEstoquista").value;
    celular = celular.trim();
    if (celular === "" || celular.length < 2 || celular.length > 20) {
        document.getElementById("erroEstadoEstoquista").className = "visivel";
        return false;
    }
    document.getElementById("erroEstadoEstoquista").className = "invisivel";
    return true;
}

function validarSenhaEstoquista() {

    var senha = document.getElementById("senhaEstoquista").value;
    senha = senha.trim();
    if (senha === "" || senha.length < 8 || senha.length > 25) {
        document.getElementById("erroSenhaEstoquista").className = "visivel";
        return false;
    }
    document.getElementById("erroSenhaEstoquista").className = "invisivel";
    return true;
}

function confirmarSenhaEstoquista() {
    var senhaConfirma = document.getElementById("senhaConfirmaEstoquista").value;
    senhaConfirma = senhaConfirma.trim();
    if (senhaConfirma === "" || senhaConfirma.length < 8 || senhaConfirma.length > 25) {
        document.getElementById("erroSenhaConfirmaEstoquista").textContent = "Insira uma confirmação de senha válida!";
        document.getElementById("erroSenhaConfirmaEstoquista").className = "visivel";
        return false;
    }
    var senha = document.getElementById("senhaEstoquista").value;
    if (senha !== senhaConfirma) {
        document.getElementById("erroSenhaConfirmaEstoquista").textContent = "As senhas não coincidem!";
        document.getElementById("erroSenhaConfirmaEstoquista").className = "visivel";
        return false;
    }
    document.getElementById("erroSenhaConfirmaEstoquista").className = "invisivel";
    return true;
}

function validarEstoquista() {
    var nomeValido = validarNomeEstoquista();
    var celularValido = validarCelularEstoquista();
    var logradouroValido = validarLogradouroEstoquista();
    var numeroValido = validarNumeroEstoquista();
    var bairroValido = validarBairroEstoquista();
    var cidadeValida = validarCidadeEstoquista();
    var estadoValido = validarEstadoEstoquista();
    var emailValido = validarEmailEstoquista();
    var senhaValida = validarSenhaEstoquista();
    var confirmaSenha = confirmarSenhaEstoquista();

    return nomeValido && celularValido && logradouroValido && numeroValido && bairroValido && cidadeValida && estadoValido && emailValido && senhaValida && confirmaSenha;
}

function validarLoginEstoquista() {
    var emailValido = validarEmailEstoquista();
    var senhaValida = validarSenhaEstoquista();

    return emailValido && senhaValida;
}
