$(".btn-Add").click(function () {
    var $idComanda = $("#idComanda").val();

    if (!($.isNumeric($idComanda) && $idComanda > 0)) {
        Swal.fire("Erro!", "Comanda inválida.", "error");
        return;
    }

    var $row = $(this).closest("tr");
    var $qnt = $row.find(".qnt").find("input").val();

    if (!($.isNumeric($qnt) && $qnt > 0)) {
        Swal.fire("Erro!", "Quantidade inválida.", "error");
        return;
    }

    var $idLote = $row.find(".id-lote").text();

    $.ajax({
        url: "/Comanda/Venda/",
        type: "POST",
        data: { IdComanda: $idComanda, IdLote: $idLote, Quantidade: $qnt },
        success: (data) => {
            if (data) {
                Swal.fire("Erro!", data, "error");
            }
            else {
                Swal.fire("Sucesso!", "Item lançado na comanda #" + $idComanda + ".", "success").then(() => {
                    location.href = "Venda?idComanda=" + $idComanda;
                });
            }
        }
    });
});

function procurarProdutos() {
    const termoPesquisa = $("#pesquisar").val() || "";
    const url = "/Comanda/Venda?procura=" + termoPesquisa;
    window.location.href = url;
}