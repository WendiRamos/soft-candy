$(".btn-Remove").click(function () {
    var $idComanda = $("#idComanda").text();
    var idItem = $(this).attr("id-item");

    $.ajax({
        url: "/Comanda/RemoverItem/",
        type: "DELETE",
        data: { IdComanda: $idComanda, IdItem: idItem },
        success: (data) => {
            if (data) {
                Swal.fire("Erro!", data, "error");
            }
            else {
                Swal.fire("Sucesso!", "Item removido!", "success").then(() => {
                    location.reload();
                });
            }
            console.log(data);
        }
    });
});
