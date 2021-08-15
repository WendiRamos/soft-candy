function dinheiro(num) {
    return (
        "R$ " +
        num.toLocaleString("pt-BR", {
            minimumFractionDigits: 2,
            maximumFractionDigits: 2,
        })
    );
}
