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
function validarEndereco() {
    var endereco = document.getElementById("endereco").value;
    endereco = endereco.trim();
    if (endereco === "" || endereco.length < 20 || endereco.length > 200) {
        document.getElementById("erroEndereco").className = "visivel";
        return false;
    }
    document.getElementById("erroEndereco").className = "invisivel";
    return true;
}
function validarEmail() {
    var email = document.getElementById("email").value;
    email = email.trim();
    if (email === "" || email.length < 12 || email.length > 50) {
        document.getElementById("erroEmail").className = "visivel";
        return false;
    }
    document.getElementById("erroEmail").className = "invisivel";
    return true;
}
function validarCelular() {
    var celular = document.getElementById("celular").value;
    celular = celular.trim();
    if (celular === "" || celular.length < 8 || celular.length > 12) {
        document.getElementById("erroCelular").className = "visivel";
        return false;
    }
    document.getElementById("erroCelular").className = "invisivel";
    return true;
}
function validarVendedor() {
    var nomeValido = validarNome();
    console.log(nomeValido)
    var celularValido = validarCelular();
    console.log(celularValido)
    var enderecoValido = validarEndereco();
    console.log(enderecoValido)
    var emailValido = validarEmail();
    console.log(emailValido)
    var senhaValida = validarSenha();
    console.log(senhaValida)
    var confirma = confirmarSenha();
    console.log(confirma)
    

    return nomeValido && celularValido && enderecoValido && emailValido && senhaValida && confirma;
}