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
    document.querySelectorAll(".troco").forEach(t => t.style.display = "none");
}

function mostrar() {
    document.querySelectorAll(".troco").forEach(t => t.style.display = "");
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
    let totalPedido = Number(document.querySelector("#totalDelivery").value.replace(",", ".").replace('R$', ''));
    let dinheiro = Number(document.querySelector("#dinheiro").value);

    if (dinheiro - totalPedido < 0) {
        document.querySelector("#troco").value = formatarDinheiro(0);
    }
    else {
        document.querySelector("#troco").value = formatarDinheiro(dinheiro - totalPedido);
    }
}

function calcularTotalDelivery() {
    const totalPedido = Number(document.querySelector("#totalPedido").textContent.replace(",", "."));
    const totalFrete = Number(document.querySelector("#totalFrete").value.replace(",", "."));

    if (totalFrete == '') {
        totalFrete = 0.0;
    }


    document.querySelector("#totalDelivery").value = formatarDinheiro(totalPedido + totalFrete);
}

document.querySelector("#formaPagamento").addEventListener("change", eventoSelect);
document.querySelector("#dinheiro").addEventListener("keyup", calcularTroco);
document.querySelector("#totalFrete").addEventListener("keyup", calcularTotalDelivery);
