$(".btn-Descarte").click(function () {
    var $idLote = $(this).attr("loteId");

    

    Swal.fire({
        icon: "question",
        focusCancel: true,
        title: "Lote Vencido",
        text: "Certeza que deseja descartar todo o estoque do lote " + $idLote + "?",
        showCancelButton: true,
        cancelButtonText: "Cancelar",
    }).then(resposta => {
        if (resposta.isConfirmed) {
            $.ajax({ url: "/Lote/Descartar/" + $idLote, type: "POST" });
            Swal.fire("Sucesso!", "Lote descartado!", "success").then(() => {
                location.reload();
            });
        }
    });
});