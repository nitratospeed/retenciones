$(document).on("click", '.BtnEdit', function (e) {

    document.getElementById("Id").value = $(this).closest("tr").find(".TdId").text();
    document.getElementById("Nombres").value = $(this).closest("tr").find(".TdNombres").text();
    document.getElementById("Usuario").value = $(this).closest("tr").find(".TdUsuario").text();
    document.getElementById("Perfil").value = $(this).closest("tr").find(".TdPerfil").text();

    var estado = $(this).closest("tr").find(".TdEstado").text();

    if (estado == "INACTIVO") {
        document.getElementById("Estado").checked = false;
    }

    else {
        document.getElementById("Estado").checked = true;
    }
    $('#EditModal').modal();
});

$(document).on("click", '#Reseteo', function (e) {
    if (document.getElementById("Reseteo").checked) {
        $("#DivPassword").show();
    }

    else {
        $("#DivPassword").hide();
    }
});

$(document).on("click", '#BtnActualizar', function (e) {
    $.ajax({
        type: "GET",
        url: "./Account/EditUser",
        dataType: "json",
        data: {
            Id: document.getElementById("Id").value,
            Usuario: document.getElementById("Usuario").value,
            Nombres: document.getElementById("Nombres").value,
            Perfil: document.getElementById("Perfil").value,
            Estado: document.getElementById("Estado").checked,
            Reseteo: document.getElementById("Reseteo").checked,
        },
        success: function (data) {

            if (data == "1") {
                alert("Usuario Actualizado");
                location.reload();
            }

            else {
                alert("Error al actualizar usuario.");
            }           
        },
        error: function (xhr, status, error) {
            alert("Error al actualizar usuario.");
        }
    });
});