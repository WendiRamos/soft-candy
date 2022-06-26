function formatarDinheiro(num) {
    return (
        "R$ " +
        num.toLocaleString("pt-BR", {
            minimumFractionDigits: 2,
            maximumFractionDigits: 2,
        })
    );
}

function ocultar() {
    document.querySelector(".troco").style.display = "none"
}

function mostrar() {
    document.querySelector(".troco").style.display = ""
}

function eventoSelect(evento) {
    let valor = evento.target.value;
    if (valor === "1") {
        mostrar();
        resetarDinheiroEntrado();
    } else {
        ocultar();
    }
}

function resetarDinheiroEntrado() {
    document.querySelector("#dinheiro").value = "";
}

function calcularTroco() {
    let totalPedido = document.querySelector("#totalPedido").textContent.replace(",", ".");
    let dinheiro = document.querySelector("#dinheiro").value;

    if (dinheiro - totalPedido < 0) {
        document.querySelector("#troco").textContent = formatarDinheiro(0);
    }
    else {
        document.querySelector("#troco").textContent = formatarDinheiro(dinheiro - totalPedido);
    }
}

document.querySelector("select").addEventListener("change", eventoSelect);
document.querySelector("#dinheiro").addEventListener("keyup", calcularTroco);

