function validarNomeCategoria() {
    var nome = document.getElementById("NomeCategoria").value;
    nome = nome.trim();
    if (nome === "" || nome.length < 3 || nome.length > 50) {
        document.getElementById("erroNomeCategoria").className = "visivel";

        return false;
    }
    document.getElementById("erroNomeCategoria").className = "invisivel";
    return true;
}

function validarCategoria() {
    var nomeValido = validarNomeCategoria();

    return nomeValido;
}