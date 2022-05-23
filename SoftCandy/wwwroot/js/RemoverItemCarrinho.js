$(".btn-Remove").click(function () {
    var idItem = $(this).attr("id-item");

    $.ajax({
        url: "/Delivery/RemoverItem/",
        type: "DELETE",
        data: { IdItem: idItem },
        success: (data) => {
            if (data) {
                Swal.fire("Erro!", data, "error");
            }
            else {
                Swal.fire("Sucesso!", "Item removido!", "success").then(() => {
                    location.reload();
                });
            }
        }
    });
});
