function validarNomeCliente() {
    var nome = document.getElementById("nomeCliente").value;
    nome = nome.trim();
    if (nome === "" || nome.length < 9 || nome.length > 50) {
        document.getElementById("erroNomeCliente").className = "visivel";

        return false;
    }
    document.getElementById("erroNomeCliente").className = "invisivel";
    return true;
}

function validarCelularCliente() {
    var celular = document.getElementById("celularCliente").value;
    celular = celular.trim();
    if (celular === "" || celular.length < 8 || celular.length > 12) {
        document.getElementById("erroCelularCliente").className = "visivel";
        return false;
    }
    document.getElementById("erroCelularCliente").className = "invisivel";
    return true;
}
function validarEmailCliente() {
    var email = document.getElementById("emailCliente").value;
    email = email.trim();
    if (email === "" || email.length < 12 || email.length > 50) {
        document.getElementById("erroEmailCliente").className = "visivel";
        return false;
    }
    document.getElementById("erroEmailCliente").className = "invisivel";
    return true;
}

function validarLogradouroCliente() {
    var celular = document.getElementById("logradouroCliente").value;
    celular = celular.trim();
    if (celular === "" || celular.length < 8 || celular.length > 100) {
        document.getElementById("erroLogradouroCliente").className = "visivel";
        return false;
    }
    document.getElementById("erroLogradouroCliente").className = "invisivel";
    return true;
}

function validarNumeroCliente() {
    var celular = document.getElementById("numeroCliente").value;
    celular = celular.trim();
    if (celular === "" || celular.length < 2 || celular.length > 5) {
        document.getElementById("erroNumeroCliente").className = "visivel";
        return false;
    }
    document.getElementById("erroNumeroCliente").className = "invisivel";
    return true;
}

function validarBairroCliente() {
    var celular = document.getElementById("bairroCliente").value;
    celular = celular.trim();
    if (celular === "" || celular.length < 8 || celular.length > 100) {
        document.getElementById("erroBairroCliente").className = "visivel";
        return false;
    }
    document.getElementById("erroBairroCliente").className = "invisivel";
    return true;
}

function validarCidadeCliente() {
    var celular = document.getElementById("cidadeCliente").value;
    celular = celular.trim();
    if (celular === "" || celular.length < 2 || celular.length > 30) {
        document.getElementById("erroCidadeCliente").className = "visivel";
        return false;
    }
    document.getElementById("erroCidadeCliente").className = "invisivel";
    return true;
}

function validarEstadoCliente() {
    var celular = document.getElementById("estadoCliente").value;
    celular = celular.trim();
    if (celular === "" || celular.length < 2 || celular.length > 20) {
        document.getElementById("erroEstadoCliente").className = "visivel";
        return false;
    }
    document.getElementById("erroEstadoCliente").className = "invisivel";
    return true;
}

function validarCliente() {
    var nomeValido = validarNomeCliente();
    var celularValido = validarCelularCliente();
    var emailValido = validarEmailCliente();
    var logradouroValido = validarLogradouroCliente();
    var numeroValido = validarNumeroCliente();
    var bairroValido = validarBairroCliente();
    var cidadeValida = validarCidadeCliente();
    var estadoValido = validarEstadoCliente();

    return nomeValido && celularValido && emailValido && logradouroValido && numeroValido && bairroValido && cidadeValida && estadoValido;
}