$(".btn-Add").click(function () {
    var $row = $(this).closest("tr");
    var $qnt = $row.find(".qnt").find("input").val();

    if (!($.isNumeric($qnt) && $qnt > 0)) {
        Swal.fire("Erro!", "Quantidade inválida.", "error");
        return;
    }

    var $idLote = $row.find(".id-lote").text();

    $.ajax({
        url: "/Delivery/EscolherItens/",
        type: "POST",
        data: { IdLote: $idLote, Quantidade: $qnt },
        success: (data) => {
            if (data) {
                Swal.fire("Erro!", data, "error");
            }
            else {
                Swal.fire("Sucesso!", "Item lançado no carrinho!", "success").then(() => {
                    location.reload();
                });
            }
            console.log(data);
        }
    });
});

function procurarProdutos() {
    const termoPesquisa = $("#pesquisar").val() || "";
    const url = "/Delivery/EscolherItens?procura=" + termoPesquisa;
    window.location.href = url;
}