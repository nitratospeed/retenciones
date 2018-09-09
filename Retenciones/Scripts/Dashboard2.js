$(document).on("click", '#BtnBuscar', function (e) {

    $.ajax({
        type: "GET",
        url: "../Gestion/GetPartialDashboard2",
        data: { FechaInicio: $("#FechaInicio").val(), FechaFin: $("#FechaFin").val() },
        success: function (data) {
            $("#Partial").html(data);
        },
        error: function (xhr, status, error) {
            alert(error);
        }
    });
});