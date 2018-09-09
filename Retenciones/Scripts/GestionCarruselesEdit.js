$(document).ready(function () {
    if (document.getElementById('ChkCarrusel').checked) {
        $("#Ocultar").show();
    }
    else {
        $("#Ocultar").hide();
    }
});

$(document).on("click", '#ChkCarrusel', function (e) {
    $("#Ocultar").show();
});

$(document).on("click", '#ChkNoCarrusel', function (e) {
    $("#Ocultar").hide();
});

$(document).on("change", '#TipoAplicativoAntiguo', function (e) {
    if ((this).value == "SIAC") {
        $("#DivInteraccionAntiguo").show();
        $("#DivIncidenciaAntiguo").hide();
    }
    else if ((this).value == "SGA") {
        $("#DivInteraccionAntiguo").hide();
        $("#DivIncidenciaAntiguo").show();
    }
    else {
        $("#DivInteraccionAntiguo").hide();
        $("#DivIncidenciaAntiguo").hide();
    }
});