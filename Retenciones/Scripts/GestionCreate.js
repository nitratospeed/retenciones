$(document).ready(function () {

    $("#SeDerivaId option[value='2']").remove();
    $("#SeDerivaId option[value='3']").remove();

    $("#Tra").hide();
    $("#Trb").hide();

    for (var i = 0; i < 10; i++) {
        $("#Tr" + i).hide();
    }

    $("#ChkSGA").prop('checked', true);

    $("#CustomerID").attr('readonly', 'readonly');

    $("#Contrato").attr('readonly', 'readonly');

    $("#Email").attr('readonly', 'readonly');

    $("#PlanOfrecido").attr('readonly', 'readonly');

    $('#CodigoSGA').attr('data-val', 'true');
    $('#CodigoSGA').attr('data-val-required', 'Requerido*');

    $('#TieneEmail').attr('data-val', 'true');
    $('#TieneEmail').attr('data-val-required', 'Requerido*');

    $('#NombreCliente').attr('data-val', 'true');
    $('#NombreCliente').attr('data-val-required', 'Requerido*');

    $('#TelefonoContacto1').attr('data-val', 'true');
    $('#TelefonoContacto1').attr('data-val-required', 'Requerido*');

    $('#TieneOfertaOtro').attr('data-val', 'true');
    $('#TieneOfertaOtro').attr('data-val-required', 'Requerido*');

    validation();

    $('#OfrecimientoId').multiselect({
        maxHeight: 200,
        buttonWidth: '250px',
        buttonText: function (options) {
            if (options.length == 0) {
                return "SELECCIONE";
            } else {
                var selected = 0;
                options.each(function () {
                    selected += 1;
                });
                return selected + " SELECCIONADOS";
            }
        },
        onChange: function () {
            console.log($('#OfrecimientoId').val());
            $('#HiddenOfrecimientoId').val($('#OfrecimientoId').val());
        }
    });

});

function validation() {
    $('#frm').removeData('unobtrusiveValidation');
    $('#frm').removeData('validator');
    $.validator.unobtrusive.parse('#frm');
}

$('#frm').submit(function (e) {
    if ($(this).valid()) {
        $(this).find(':submit').attr('disabled', 'disabled');
    }
    else {
        var validator = $('#frm').validate();
        var array_valid = new Array();
        var i = 0;
        $.each(validator.errorMap, function (index, value)
        {
            i++;
            array_valid[i] = index;
        });

        alert("Los siguientes campos son obligatorios, favor de llenarlos: " + "\n" + array_valid.join("\n"));
    }
});

$(document).on("click", '#ChkSGA', function (e) {

    if ($("#ResultadoRetencion").val() == "POR LO OFRECIDO" || $("#ResultadoRetencion").val() == "LLAMADA VICIO")
    {
        $('#CodigoSGA').removeAttr('data-val-required');
        $("#CodigoSGA").val("");
    }

    else {
        $("#CodigoSGA").removeAttr('readonly');
        $('#CodigoSGA').attr('data-val', 'true');
        $('#CodigoSGA').attr('data-val-required', 'Requerido*');
    }

    $('#CustomerID').removeAttr('data-val-required');
    $("#CustomerID").val("");
    $("#CustomerID").attr('readonly', 'readonly');

    $('#Contrato').removeAttr('data-val-required');
    $("#Contrato").val("");
    $("#Contrato").attr('readonly', 'readonly');

    $("#CambioPlanMigracion").removeAttr('readonly');

    $("#Decos").removeAttr('readonly');

    $("#Canales").removeAttr('readonly');

    $("#NuevoPrecio").removeAttr('readonly');

    validation();
});

$(document).on("click", '#ChkSIAC', function (e) {

    if ($("#ResultadoRetencion").val() == "POR LO OFRECIDO" || $("#ResultadoRetencion").val() == "LLAMADA VICIO")
    {
        $('#CustomerID').removeAttr('data-val-required');
        $("#CustomerID").val("");

        $('#Contrato').removeAttr('data-val-required');
        $("#Contrato").val("");
    }

    else {
        $("#CustomerID").removeAttr('readonly');
        $('#CustomerID').attr('data-val', 'true');
        $('#CustomerID').attr('data-val-required', 'Requerido*');

        $("#Contrato").removeAttr('readonly');
        $('#Contrato').attr('data-val', 'true');
        $('#Contrato').attr('data-val-required', 'Requerido*');
    }

    $('#CodigoSGA').removeAttr('data-val-required');
    $("#CodigoSGA").val("");
    $("#CodigoSGA").attr('readonly', 'readonly');

    $("#CambioPlanMigracion").val("");
    $("#CambioPlanMigracion").attr('readonly', 'readonly');

    $("#Decos").val("");
    $("#Decos").attr('readonly', 'readonly');

    $("#Canales").val("");
    $("#Canales").attr('readonly', 'readonly');

    $("#NuevoPrecio").val("");
    $("#NuevoPrecio").attr('readonly', 'readonly');

    validation();
});

$(document).on("change", '#MotivoInicialId', function (e) {
    $.ajax({
        type: "GET",
        url: "../Gestion/GetJsonMotivoFinal",
        dataType: "json",
        data: { id: $("#MotivoInicialId").val() },
        success: function (data) {
            var items = "";
            items = "<option value=''>SELECCIONE</option>";
            $.each(data, function (i, item) {
                items += "<option value=\"" + item.Value + "\">" + item.Text + "</option>";
            });
            $("#MotivoFinalId").html(items);
        },
        error: function (xhr, status, error) {
            alert("Error al cargar la lista de Motivos Finales.");
        }
    });
});

$(document).on("change", '#SeDerivaId', function (e) {
    $.ajax({
        type: "GET",
        url: "../Gestion/GetJsonUsuarioDeriva",
        dataType: "json",
        data: { id: $("#SeDerivaId").val() },
        success: function (data) {
            var items = "";
            items = "<option value=''>SELECCIONE</option>";
            $.each(data, function (i, item) {
                items += "<option value=\"" + item.Value + "\">" + item.Text + "</option>";
            });
            $("#UsuarioDerivaId").html(items);
        },
        error: function (xhr, status, error) {
            alert("Error al cargar la lista de Usuarios Herencia.");
        }
    });
});

$(document).on("change", '#FueRetenido', function (e) {
    if ($("#FueRetenido").val() == "SI") {
        $("#EscojaDiv").show();

        $("#Porque").val("");
        $("#PorqueDiv").hide();
    }
    else if ($("#FueRetenido").val() == "NO") {
        $("#EscojaDiv").hide();
        $("#Escoja").val("");

        $("#PorqueDiv").show();
    }
});

$(document).on("change", '#TieneEmail', function (e) {
    if ($("#TieneEmail").val() == "SI TIENE") {
        $("#Email").removeAttr('readonly');
        $('#Email').attr('data-val', 'true');
        $('#Email').attr('data-val-required', 'Requerido*');
    }

    else {
        $("#Email").attr('readonly', 'readonly');
        $("#Email").val("");
        $('#Email').removeAttr('data-val-required');
    }
    validation();
});

$(document).on("change", '#TieneOfertaOtro', function (e) {
    if ($("#TieneOfertaOtro").val() == "SI") {
        $("#PlanOfrecido").removeAttr('readonly');
    }

    else {
        $("#PlanOfrecido").attr('readonly', 'readonly');
        $("#PlanOfrecido").val("");
    }
});

$(document).on("change", '#ResultadoRetencion', function (e) {

    $("#Ocultar").show();
    $("#DivTipoBaja").hide();

    $('#TieneEmail').attr('data-val', 'true');
    $('#TieneEmail').attr('data-val-required', 'Requerido*');

    $('#NombreCliente').attr('data-val', 'true');
    $('#NombreCliente').attr('data-val-required', 'Requerido*');

    $('#TelefonoContacto1').attr('data-val', 'true');
    $('#TelefonoContacto1').attr('data-val-required', 'Requerido*');

    $("#TipoBaja").val("");
    $('#TipoBaja').removeAttr('data-val-required');

    $('#MotivoInicialId').attr('data-val', 'true');
    $('#MotivoInicialId').attr('data-val-required', 'Requerido*');

    $('#MotivoFinalId').attr('data-val', 'true');
    $('#MotivoFinalId').attr('data-val-required', 'Requerido*');

    $('#TipoSolicitud').attr('data-val', 'true');
    $('#TipoSolicitud').attr('data-val-required', 'Requerido*');

    $('#FueRetenido').removeAttr('data-val-required');

    $('#OfrecimientoId').attr('data-val', 'true');
    $('#OfrecimientoId').attr('data-val-required', 'Requerido*');

    if ($("#ResultadoRetencion").val() == "BAJA") {

        $("#DivTipoBaja").show();      

        $('#TipoBaja').attr('data-val', 'true');
        $('#TipoBaja').attr('data-val-required', 'Requerido*');       

        $('#FueRetenido').attr('data-val', 'true');
        $('#FueRetenido').attr('data-val-required', 'Requerido*');
    }

    else if ($("#ResultadoRetencion").val() == "POR LO OFRECIDO" || $("#ResultadoRetencion").val() == "LLAMADA VICIO") {
        $("#Ocultar").hide();
        $("#DivTipoBaja").hide();

        $('#CodigoSGA').removeAttr('data-val-required');
        $("#CodigoSGA").val("");

        $('#CustomerID').removeAttr('data-val-required');
        $("#CustomerID").val("");

        $('#Contrato').removeAttr('data-val-required');
        $("#Contrato").val("");

        $('#TieneEmail').removeAttr('data-val-required');
        $("#TieneEmail").val("");

        $('#NombreCliente').removeAttr('data-val-required');
        $("#NombreCliente").val("");

        $('#TelefonoContacto1').removeAttr('data-val-required');
        $("#TelefonoContacto1").val("");

        $("#TipoBaja").val("");
        $('#TipoBaja').removeAttr('data-val-required');

        $('#MotivoInicialId').removeAttr('data-val-required');
        $("#MotivoInicialId").val("");

        $('#MotivoFinalId').removeAttr('data-val-required');
        $("#MotivoFinalId").val("");

        $('#TipoSolicitud').removeAttr('data-val-required');
        $("#TipoSolicitud").val("");

        $('#FueRetenido').removeAttr('data-val-required');

        $('#OfrecimientoId').removeAttr('data-val-required');
    }
    validation();
});

$(document).on("change", '#TipoBaja', function (e) {
    if ($("#TipoBaja").val() == "HERENCIA") {
        $('#SeDerivaId').attr('data-val', 'true');
        $('#SeDerivaId').attr('data-val-required', 'Requerido*');

        $('#UsuarioDerivaId').attr('data-val', 'true');
        $('#UsuarioDerivaId').attr('data-val-required', 'Requerido*');
    }
    else {
        $('#SeDerivaId').removeAttr('data-val-required');
        $("#SeDerivaId").val("");

        $('#UsuarioDerivaId').removeAttr('data-val-required');
        $("#UsuarioDerivaId").val("");
    }
    validation();
});

function GetOfrecimiento2(id, id2) {
    if (id == "") {
        //$("#Accion" + id2).text("");
        $("#IdeaFuerza" + id2).text("");
        $("#Speech" + id2).attr('data-content', "");
    }

    else {
        $.ajax({
            type: "GET",
            url: "../Gestion/GetOfrecimiento2",
            dataType: "json",
            data: { id: id },
            success: function (data) {
                //$("#Accion" + id2).text(data.AccionTomar);
                $("#IdeaFuerza" + id2).text(data.IdeaFuerza);
                $("#Speech" + id2).attr('data-content', data.Speech);
                $("#Speech" + id2).popover({ placement: 'bottom' });
            },
            error: function (xhr, status, error) {
                alert("Error al cargar la lista de Ofrecimientos 2.");
            }
        });
    }
}

$(document).on("change", '#MotivoBajaOfre', function (e) {

    $("#Speech7").popover({ placement: 'bottom' });
    $("#Speech8").popover({ placement: 'bottom' });
    $("#Speech9").popover({ placement: 'bottom' });

    for (var i = 0; i < 10; i++) {
        $("#Tr" + i).show();
    }

    var order = ['Tr1', 'Tr2', 'Tr3', 'Tr4', 'Tr5', 'Tr6', 'Tr7', 'Tr8', 'Tr9'];
    var $table = $('#TableOfrecimientos2');

    if ($("#MotivoBajaOfre").val() == "TÉCNICO") {

        $("#Tr3").hide();
        $("#Tr7").hide();
        $("#Tr8").hide();
        $("#Tr9").hide();

        var order = ['Tr2', 'Tr1', 'Tr4', 'Tr5', 'Tr6'];
    }  

    else if ($("#MotivoBajaOfre").val() == "ECONÓMICOS") {

        $("#Tr2").hide();
        $("#Tr3").hide();
        $("#Tr9").hide();

        var order = ['Tr1', 'Tr5', 'Tr6', 'Tr4', 'Tr7', 'Tr8'];
    }

    else if ($("#MotivoBajaOfre").val() == "ADMINISTRATIVO") {

        $("#Tr2").hide();
        $("#Tr7").hide();
        $("#Tr8").hide();
        $("#Tr9").hide();

        var order = ['Tr3', 'Tr5', 'Tr1', 'Tr6', 'Tr4'];
    }

    else if ($("#MotivoBajaOfre").val() == "COMPETENCIA (VIAJE/NO USO/MUDANZA SIN COBERTURA)") {

        $("#Tr2").hide();
        $("#Tr3").hide();
        $("#Tr8").hide();
        $("#Tr9").hide();

        var order = ['Tr1', 'Tr4', 'Tr6', 'Tr5', 'Tr7'];
    }

    else if ($("#MotivoBajaOfre").val() == "MUDANZA CON COBERTURA") {

        $("#Tr2").hide();
        $("#Tr3").hide();
        $("#Tr5").hide();
        $("#Tr7").hide();

        var order = ['Tr9', 'Tr1', 'Tr4', 'Tr6', 'Tr8'];
    }

    else if ($("#MotivoBajaOfre").val() == "CARRUSEL") {

        $("#Tr2").hide();
        $("#Tr3").hide();
        $("#Tr5").hide();
        $("#Tr7").hide();
        $("#Tr9").hide();

        var order = ['Tr1', 'Tr4', 'Tr6', 'Tr8'];
    }

    else if ($("#MotivoBajaOfre").val() == "MALA VENTA") {

        $("#Tr2").hide();
        $("#Tr4").hide();
        $("#Tr5").hide();
        $("#Tr8").hide();
        $("#Tr9").hide();

        var order = ['Tr1', 'Tr3', 'Tr6', 'Tr7'];
    }

    else {
        for (var i = 0; i < 10; i++) {
            $("#Tr" + i).hide();
        }
    }

    for (var i = order.length; --i >= 0;) {
        $table.prepend($table.find('#' + order[i]));
    }
});

//$('#BtnCerrar').on('click', function (e) {
//    if ($("#ResultadoRetencion").val() == "RETENIDO" || $("#ResultadoRetencion").val() == "BAJA") {
//        if (document.querySelector('input[name="AntiguedadOfre"]:checked') == null || document.querySelector('input[name="EstadoCuentaOfre"]:checked') == null || document.querySelector('input[name="DsctoRetencionesOfre"]:checked') == null || document.querySelector('input[name="ProblemasTecOfre"]:checked') == null || document.querySelector('input[name="ReportesFactOfre"]:checked') == null || document.querySelector('input[name="ServiciosOfre"]:checked') == null) {
//            alert("Debe llenar todos los campos, ya que es una baja/retenido");
//        }
//        else {
//            $('#OfrecimientosDiv').modal('toggle');
//        }
//    }
//    else {
//        $('#OfrecimientosDiv').modal('toggle');
//    }
//});

$(document).on("change", 'input[name=AntiguedadOfre]:checked', function (e) {
    GetOfrecimiento2($(this).val(),1);
});

$(document).on("change", 'input[name=ProblemasTecOfre]:checked', function (e) {
    GetOfrecimiento2($(this).val(),2);
});

$(document).on("change", 'input[name=ReportesFactOfre]:checked', function (e) {
    GetOfrecimiento2($(this).val(),3);
});

$(document).on("change", 'input[name=ServiciosOfre]:checked', function (e) {
    GetOfrecimiento2($(this).val(),4);
});

$(document).on("change", 'input[name=DsctoRetencionesOfre]:checked', function (e) {
    GetOfrecimiento2($(this).val(),5);
});

$(document).on("change", 'input[name=EstadoCuentaOfre]:checked', function (e) {
    GetOfrecimiento2($(this).val(),6);
});

$(document).on("click", '#PlantillaMigracionBtn', function (e) {

    if ($("#ChkSGA").prop('checked')) {
        $("#PlantillaMigracionDiv1").show();
        $("#PlantillaMigracionDiv2").hide();
        var maincontent = document.getElementById("PlantillaMigracion");
        maincontent.innerHTML =
            "Código de Cliente: " + $("#CodigoSGA").val() + "\n" +
            "TC: " + $("#TelefonoContacto1").val() + "\n" +
            "Nombre Cliente: " + $("#NombreCliente").val() + "\n" +
            "Nuevo Paquete: " + $("#CambioPlanMigracion").val() + "\n" +
            "Nuevo Precio: " + $("#NuevoPrecio").val() + "\n" +
            "Decos Adicionales: " + $("#Decos").val() + "\n" +
            "Bam: " + "SE DESACTIVARÁ" + "\n" +
            "Observación: " + "" + "\n" +
            "Asesor: " + $("#Fullname").val() + "\n";
    }
    else if ($("#ChkSIAC").prop('checked')) {
        $("#PlantillaMigracionDiv1").hide();
        $("#PlantillaMigracionDiv2").show();
        var maincontent = document.getElementById("PlantillaMigracion");
        maincontent.innerHTML = "";
    }
});

$(document).on("click", '#PlantillaVisitaTecnicaBtn', function (e) {
    var maincontent = document.getElementById("PlantillaVisitaTecnica");
    maincontent.innerHTML =
        "Problema Reportado: " + "" + "\n" +
        "Descartes Realizados: " + "SERVICIO ACTIVO SIN SOT DE CORTE" + "\n" +
        "Numero de Contacto: " + $("#TelefonoContacto1").val() + "\n" +
        "Fecha Disposición: " + "" + "\n";
});

$(document).on("click", '#PlantillaTrasladoExternoSGABtn', function (e) {
    if ($("#ChkSGA").prop('checked')) {
        $("#PlantillaTrasladoExternoSGADiv1").show();
        $("#PlantillaTrasladoExternoSGADiv2").show();
        var maincontent = document.getElementById("PlantillaTrasladoExternoSGA");
        maincontent.innerHTML =
            "Departamento: " + "" + "\n" +
            "Provincia: " + "" + "\n" +
            "Distrito: " + "" + "\n" +
            "Nueva Dirección: " + "" + "\n" +
            "Referencia: " + "" + "\n" +
            "Plano: " + "" + "\n" +
            "TC: " + $("#TelefonoContacto1").val() + "\n";
    }
    else if ($("#ChkSIAC").prop('checked')) {
        $("#PlantillaTrasladoExternoSGADiv1").hide();
        $("#PlantillaTrasladoExternoSGADiv2").show();
        var maincontent = document.getElementById("PlantillaTrasladoExternoSGA");
        maincontent.innerHTML = "";
    }
});

$(document).on("click", '#PlantillaPromocionSGABtn', function (e) {
    var maincontent = document.getElementById("PlantillaPromocionSGA");
    maincontent.innerHTML =
        "Promoción Ofrecida: " + "" + "\n" +
        "Proyecto: " + "" + "\n" +
        "Ciclo de Facturación: " + "" + "\n" +
        "Fecha de Inicio de Servicios: " + "" + "\n" +
        "Última Promoción Aplicada (Mes): " + "" + "\n" +
        "Fecha de Vencimiento (Facturas a aplicar): " + "" + "\n" +
        "Servicio aplicar promoción: " + "" + "\n";
});

$(document).on("click", '#PlantillaSuspensionTemporalBtn', function (e) {
    var maincontent = document.getElementById("PlantillaSuspensionTemporal");
    maincontent.innerHTML =
        "Cliente: " + $("#NombreCliente").val() + "\n" +
        "Motivo: " + $("#MotivoInicialId").find('option:selected').text() + "\n" +
        "Proyecto: " + "" + "\n" +
        "Contrato: " + $("#Contrato").val() + "\n" +
        "Fecha de Inicio: " + "dd/mm/aa" + "\n" +
        "Fecha de Término: " + "dd/mm/aa" + "\n" +
        "Caso de Suspensión: " + "" + "\n" +
        "Con Costo (SI/NO): " + "" + "\n" +
        "Observación: " + "" + "\n";
});

$(document).on("click", '#PlantillaRegistroRetenidosBtn', function (e) {
    if ($("#ChkSGA").prop('checked')) {
        var tc = "SGA";
    }
    else if ($("#ChkSIAC").prop('checked')) {
        var tc = "SIAC";
    }
    var maincontent = document.getElementById("PlantillaRegistroRetenidos");
    maincontent.innerHTML =
        "DNI: " + $("#DNI").val() + "\n" +
        "Código: " + $("#CodigoSGA").val() + "\n" +
        "Motivo: " + $("#MotivoInicialId").find('option:selected').text() + "\n" +
        "Oferta de Competencia (plan/precio): " + "" + "\n" +
        "Nombre Cliente: " + $("#NombreCliente").val() + "\n" +
        "Teléfono: " + $("#TelefonoContacto1").val() + "\n" +
        "Email: " + $("#Email").val() + "\n" +
        "Disponibilidad para Llamarlo: " + "TODO EL DÍA" + "\n" +
        "Plataforma SGA/SIAC: " + tc + "\n" +
        "Customer ID: " + $("#CustomerID").val() + "\n" +
        "Contrato: " + $("#Contrato").val() + "\n" +
        "Resultado de Retención: " + $("#ResultadoRetencion").val() + "\n" +
        "Ofrecimiento: " + "" + "\n" +
        "Observaciones: " + $("#Observaciones").val() + "\n" +
        "AS: " + $("#Fullname").val() + "\n" +
        "Caso pertenece a RAC: " + $("#UsuarioDerivaId").find('option:selected').text();
});

$(document).on("click", '#PlantillaSeguimientoSOTBtn', function (e) {
    var maincontent = document.getElementById("PlantillaSeguimientoSOT");
    maincontent.innerHTML =
        "SOT: " + "" + "\n" +
        "Persona de Contacto: " + $("#NombreCliente").val() + "\n" +
        "Número de Contacto: " + $("#TelefonoContacto1").val() + "\n" +
        "Observaciones: " + "" + "\n";
});

$(document).on("click", '#BtnToInteraccion', function (e) {
    if ($("#ResultadoRetencion").val() == "RETENIDO" || $("#ResultadoRetencion").val() == "BAJA") {
        if ($("#MotivoBajaOfre").val() == "") {
            alert("No ha escogido un motivo de baja de la ficha de Ofrecimientos, podrá regresar a seleccionar las opciones o seguir.");
            $('#InteraccionModal').modal('show');
        }
        else {
            $('#InteraccionModal').modal('show');
        }
    }
    else {
        $('#InteraccionModal').modal('show');
    }
});

//PARTIAL
function plantillasiac() {
    var month = new Array();
    month[0] = "";
    month[1] = "ENERO";
    month[2] = "FEBRERO";
    month[3] = "MARZO";
    month[4] = "ABRIL";
    month[5] = "MAYO";
    month[6] = "JUNIO";
    month[7] = "JULIO";
    month[8] = "AGOSTO";
    month[9] = "SETIEMBRE";
    month[10] = "OCTUBRE";
    month[11] = "NOVIEMBRE";
    month[12] = "DICIEMBRE";
    month[13] = "ENERO";
    month[14] = "FEBRERO";
    month[15] = "MARZO";
    month[16] = "ABRIL";
    month[17] = "MAYO";
    month[18] = "JUNIO";

    var d = new Date();
    var z = d.getDate();

    var c = $("#CicloCalc").val();

    if ($("#PromocionAdelantadaBtn").prop('checked')) {
        var meses = $("#MesAplicarCalc").val() - 1;
        var mesespaq = $("#MesPaq").val();

        if (c > z) {
            var x = d.getMonth() + 1;
        }

        else if (c <= z) {
            var x = d.getMonth() + 2;
        }

    }
    else {
        var meses = $("#MesAplicarCalc").val();
        var mesespaq = $("#MesPaq").val();

        if (c > z) {
            var x = d.getMonth() + 1;
        }

        else if (c <= z) {
            var x = d.getMonth() + 2;
        }
    }

    //CALCULO DE MESES A APLICAR
    var n = month[x];
    var nspeech = $("#CicloCalc").val() + " " + month[x];

    if (meses > mesespaq) {
        var cantidadmeses = meses;
    }
    else {
        var cantidadmeses = mesespaq;
    }


    for (var i = 1; i < meses; i++) {
        n = n + " - " + month[x + i];
    }
    //n = n + " - " + month[x + i];

    for (var i = 1; i < meses; i++) {
        nspeech = nspeech + " - " + $("#CicloCalc").val() + " " + month[x + i];
    }

    //LÓGICA FECHAS

    var date1 = new Date();
    date1.setDate(c);
    var date2 = new Date();

    var date3 = new Date(date2.getFullYear(), date2.getMonth(), 0);

    var date4 = date3.getDate();

    if (c < z) {
        date1.setMonth(date2.getMonth() + 1);
    }

    var timeDiff = Math.abs(date2.getTime() - date1.getTime());
    var diffDays = Math.ceil(timeDiff / (1000 * 3600 * 24));

    //CUADRO / PLANTILLA

    if ($("#PaquetesBtn").prop('checked')) {
        //CUADRO
        var cuadro1 = month[x]
            + " S/. "
            + (+(($("#CargoFijoCalc").val() * $("#DescuentoCalc").val()) / 1.18).toFixed(2)
                + +((+$("#CostoPaq1").val() + +$("#CostoPaq2").val() + +$("#CostoPaq3").val()) / 1.18).toFixed(2)
                + +((((+$("#CostoPaq1").val() + +$("#CostoPaq2").val() + +$("#CostoPaq3").val()) * diffDays) / date4) / 1.18).toFixed(2)).toFixed(2) //OCC

            + " (Incluye " + $("#DescuentoCalc").find('option:selected').text() + " al CF: S/. "
            + (($("#CargoFijoCalc").val() * $("#DescuentoCalc").val()) / 1.18).toFixed(2) //CF

            + "), (" + $("#PaquetesPaq1").val() + "+" + $("#PaquetesPaq2").val() + "+" + $("#PaquetesPaq3").val() + ") al 100% dscto S/."
            + ((+$("#CostoPaq1").val() + +$("#CostoPaq2").val() + +$("#CostoPaq3").val()) / 1.18).toFixed(2) //PAQUETES

            + " + Prorrateo (" + diffDays + ") días S./"
            + ((((+$("#CostoPaq1").val() + +$("#CostoPaq2").val() + +$("#CostoPaq3").val()) * diffDays) / date4) / 1.18).toFixed(2) //PRORRATEO

            + "\n";

        if (mesespaq < meses) {
            for (var i = 1; i < cantidadmeses; i++) {

                if (i < mesespaq) {
                    cuadro1 = cuadro1 + " - " + month[x + i]
                        + " S/. "
                        + (+(($("#CargoFijoCalc").val() * $("#DescuentoCalc").val()) / 1.18).toFixed(2)
                            + +((+$("#CostoPaq1").val() + +$("#CostoPaq2").val() + +$("#CostoPaq3").val()) / 1.18).toFixed(2)).toFixed(2) //OCC

                        + " (Incluye " + $("#DescuentoCalc").find('option:selected').text() + " al CF: S/. "
                        + (($("#CargoFijoCalc").val() * $("#DescuentoCalc").val()) / 1.18).toFixed(2) //CF

                        + "), (" + $("#PaquetesPaq1").val() + "+" + $("#PaquetesPaq2").val() + "+" + $("#PaquetesPaq3").val() + ") al 100% dscto S/."
                        + ((+$("#CostoPaq1").val() + +$("#CostoPaq2").val() + +$("#CostoPaq3").val()) / 1.18).toFixed(2) //PAQUETES 

                        + "\n";
                }
                else {
                    cuadro1 = cuadro1 + " - " + month[x + i]
                        + " S/. "
                        + (($("#CargoFijoCalc").val() * $("#DescuentoCalc").val()) / 1.18).toFixed(2) //OCC

                        + " (Incluye " + $("#DescuentoCalc").find('option:selected').text() + " al CF: S/. "
                        + (($("#CargoFijoCalc").val() * $("#DescuentoCalc").val()) / 1.18).toFixed(2) //CF
                        + ")\n";
                }
            }
        }

        else {
            for (var i = 1; i < cantidadmeses; i++) {

                if (i < meses) {
                    cuadro1 = cuadro1 + " - " + month[x + i]
                        + " S/. "
                        + (+(($("#CargoFijoCalc").val() * $("#DescuentoCalc").val()) / 1.18).toFixed(2)
                            + +((+$("#CostoPaq1").val() + +$("#CostoPaq2").val() + +$("#CostoPaq3").val()) / 1.18).toFixed(2)).toFixed(2) //OCC

                        + " (Incluye " + $("#DescuentoCalc").find('option:selected').text() + " al CF: S/. "
                        + (($("#CargoFijoCalc").val() * $("#DescuentoCalc").val()) / 1.18).toFixed(2) //CF

                        + "), (" + $("#PaquetesPaq1").val() + "+" + $("#PaquetesPaq2").val() + "+" + $("#PaquetesPaq3").val() + ") al 100% dscto S/."
                        + ((+$("#CostoPaq1").val() + +$("#CostoPaq2").val() + +$("#CostoPaq3").val()) / 1.18).toFixed(2) //PAQUETES 

                        + "\n";
                }
                else {
                    cuadro1 = cuadro1 + " - " + month[x + i]
                        + " S/. "
                        + ((+$("#CostoPaq1").val() + +$("#CostoPaq2").val() + +$("#CostoPaq3").val()) / 1.18).toFixed(2) //OCC

                        + " (Incluye " + $("#PaquetesPaq1").val() + "+" + $("#PaquetesPaq2").val() + "+" + $("#PaquetesPaq3").val() + ") al 100% dscto S/."
                        + ((+$("#CostoPaq1").val() + +$("#CostoPaq2").val() + +$("#CostoPaq3").val()) / 1.18).toFixed(2) //SOLO PAQUETES

                        + "\n";
                }
            }
        }


        //PLANTILLA
        var maincontent = document.getElementById("PlantillaPromocionSIAC");
        maincontent.innerHTML =
            "Cliente: " + $("#NombreCliente").val() + "\n" +
            "DNI: " + $("#DNI").val() + "\n" +
            "Customer ID: " + $("#CustomerID").val() + "\n" +
            "Nro Contrato: " + $("#Contrato").val() + "\n" +
            "Concepto: " + $("#ConceptoCalc").val() + " - " + $("#ServicioCalc").val() + "\n" +
            "Cargo Fijo: " + $("#CargoFijoCalc").val() + "\n" +
            "Promoción Ofrecida: " + $("#DescuentoCalc").find('option:selected').text() + "\n" +
            "Cantidad de meses de Promoción: " + meses + "\n" +
            "Meses a Aplicar: " + n + "\n" +
            "Ciclo de Facturación: " + $("#CicloCalc").val() + "\n" +
            "Paquete Premium Ofrecido: " + $("#PaquetesPaq1").val() + "," + $("#PaquetesPaq2").val() + "," + $("#PaquetesPaq3").val() + "\n" +
            "Meses de Paquete: " + $("#MesPaq").val() + "\n" +
            "Importes a Aplicar sin IGV: " + "\n - " + cuadro1 + "\n";

        //CUADRO PARA SPEECH
        var cuadrosp = month[x]
            + " S/. "
            + (($("#CargoFijoCalc").val() * $("#DescuentoCalc").val())
                + (+$("#CostoPaq1").val() + +$("#CostoPaq2").val() + +$("#CostoPaq3").val())
                + (((+$("#CostoPaq1").val() + +$("#CostoPaq2").val() + +$("#CostoPaq3").val()) * diffDays) / date4)).toFixed(2)

            + " (Incluye " + $("#DescuentoCalc").find('option:selected').text() + " al CF: S/. " + ($("#CargoFijoCalc").val() * $("#DescuentoCalc").val())

            + " (" + $("#PaquetesPaq1").val() + "+" + $("#PaquetesPaq2").val() + "+" + $("#PaquetesPaq3").val() + ") al 100% dscto S/."

            + (+$("#CostoPaq1").val() + +$("#CostoPaq2").val() + +$("#CostoPaq3").val())

            + " + Prorrateo (" + diffDays + ") días S./" + (((+$("#CostoPaq1").val() + +$("#CostoPaq2").val() + +$("#CostoPaq3").val()) * diffDays) / date4).toFixed(2) + "\n";

        if (mesespaq < meses) {
            for (var i = 1; i < cantidadmeses; i++) {

                if (i < mesespaq) {
                    cuadrosp = cuadrosp + " - " + month[x + i]
                        + " S/. "
                        + (($("#CargoFijoCalc").val() * $("#DescuentoCalc").val()) + (+$("#CostoPaq1").val() + +$("#CostoPaq2").val() + +$("#CostoPaq3").val())).toFixed(2)

                        + " (Incluye " + $("#DescuentoCalc").find('option:selected').text() + " al CF: S/. " + ($("#CargoFijoCalc").val() * $("#DescuentoCalc").val())

                        + ") (" + $("#PaquetesPaq1").val() + "+" + $("#PaquetesPaq2").val() + "+" + $("#PaquetesPaq3").val() + ") al 100% dscto S/."

                        + (+$("#CostoPaq1").val() + +$("#CostoPaq2").val() + +$("#CostoPaq3").val()) + "\n";
                }
                else {
                    cuadrosp = cuadrosp + " - " + month[x + i]
                        + " S/. "
                        + ($("#CargoFijoCalc").val() * $("#DescuentoCalc").val()).toFixed(2)

                        + " (Incluye " + $("#DescuentoCalc").find('option:selected').text() + " al CF: S/. " + ($("#CargoFijoCalc").val() * $("#DescuentoCalc").val()) + ")\n";
                }
            }
        }

        else {
            for (var i = 1; i < cantidadmeses; i++) {

                if (i < meses) {
                    cuadrosp = cuadrosp + " - " + month[x + i]
                        + " S/. "
                        + (($("#CargoFijoCalc").val() * $("#DescuentoCalc").val()) + (+$("#CostoPaq1").val() + +$("#CostoPaq2").val() + +$("#CostoPaq3").val())).toFixed(2)

                        + " (Incluye " + $("#DescuentoCalc").find('option:selected').text() + " al CF: S/. " + ($("#CargoFijoCalc").val() * $("#DescuentoCalc").val())

                        + ") (" + $("#PaquetesPaq1").val() + "+" + $("#PaquetesPaq2").val() + "+" + $("#PaquetesPaq3").val() + ") al 100% dscto S/."

                        + (+$("#CostoPaq1").val() + +$("#CostoPaq2").val() + +$("#CostoPaq3").val()) + "\n";
                }
                else {
                    cuadrosp = cuadrosp + " - " + month[x + i]
                        + " S/. "
                        + (+$("#CostoPaq1").val() + +$("#CostoPaq2").val() + +$("#CostoPaq3").val())
                        + " (Incluye " + $("#PaquetesPaq1").val() + "+" + $("#PaquetesPaq2").val() + "+" + $("#PaquetesPaq3").val() + ") al 100% dscto S/."
                        + (+$("#CostoPaq1").val() + +$("#CostoPaq2").val() + +$("#CostoPaq3").val()) + "\n";
                }
            }
        }

    }

    else {
        //CUADRO
        var cuadro1 = month[x]
            + " S/. "
            + (($("#CargoFijoCalc").val() * $("#DescuentoCalc").val()) / 1.18).toFixed(2) //OCC

            + " (Incluye " + $("#DescuentoCalc").find('option:selected').text() + " al CF: S/. "
            + (($("#CargoFijoCalc").val() * $("#DescuentoCalc").val()) / 1.18).toFixed(2) //CF

            + ")\n";

        for (var i = 1; i < cantidadmeses; i++) {
            cuadro1 = cuadro1 + " - " + month[x + i]
                + " S/. "
                + (($("#CargoFijoCalc").val() * $("#DescuentoCalc").val()) / 1.18).toFixed(2) //OCC

                + " (Incluye " + $("#DescuentoCalc").find('option:selected').text() + " al CF: S/. "
                + (($("#CargoFijoCalc").val() * $("#DescuentoCalc").val()) / 1.18).toFixed(2) //CF

                + ")\n";
        }

        //PLANTILLA
        var maincontent = document.getElementById("PlantillaPromocionSIAC");
        maincontent.innerHTML =
            "Cliente: " + $("#NombreCliente").val() + "\n" +
            "DNI: " + $("#DNI").val() + "\n" +
            "Customer ID: " + $("#CustomerID").val() + "\n" +
            "Nro Contrato: " + $("#Contrato").val() + "\n" +
            "Concepto: " + $("#ConceptoCalc").val() + " - " + $("#ServicioCalc").val() + "\n" +
            "Cargo Fijo: " + $("#CargoFijoCalc").val() + "\n" +
            "Promoción Ofrecida: " + $("#DescuentoCalc").find('option:selected').text() + "\n" +
            "Cantidad de meses de Promoción: " + meses + "\n" +
            "Meses a Aplicar: " + n + "\n" +
            "Ciclo de Facturación: " + $("#CicloCalc").val() + "\n" +
            "Importes a Aplicar sin IGV: " + "\n - " + cuadro1 + "\n";

        //CUADRO PARA SPEECH
        var cuadrosp = month[x]
            + " S/. "
            + ($("#CargoFijoCalc").val() * $("#DescuentoCalc").val())
            + " (Incluye " + $("#DescuentoCalc").find('option:selected').text() + " al CF: S/. " + ($("#CargoFijoCalc").val() * $("#DescuentoCalc").val()).toFixed(2) + ")\n";

        for (var i = 1; i < cantidadmeses; i++) {
            cuadrosp = cuadrosp + " - " + month[x + i]
                + " S/. "
                + ($("#CargoFijoCalc").val() * $("#DescuentoCalc").val())
                + " (Incluye " + $("#DescuentoCalc").find('option:selected').text() + " al CF: S/. " + ($("#CargoFijoCalc").val() * $("#DescuentoCalc").val()).toFixed(2) + ")\n";
        }

    }

    //SPEECH

    var if1 = "";
    var if2 = "";

    //paquetes

    if ($("#PaquetesPaq1").val() == "") {
        pq1 = "";
    }

    else {
        pq1 = $("#PaquetesPaq1").find('option:selected').text();
    }

    if ($("#PaquetesPaq2").val() == "") {
        pq2 = "";
    }

    else {
        pq2 = ", " + $("#PaquetesPaq2").find('option:selected').text();
    }

    if ($("#PaquetesPaq3").val() == "") {
        pq3 = "";
    }

    else {
        pq3 = ", " + $("#PaquetesPaq3").find('option:selected').text();
    }

    //
    if ($("#PromocionAdelantadaBtn").prop('checked')) {
        if1 = "Adicional a ello, en la factura del mes de " + month[x - 1] + ", le brindaremos una promoción de manera adelantada, es decir para la factura N° " + $("#NroFacturaProm").val() + " pendiente de pago. ";
    }

    if ($("#PaquetesBtn").prop('checked')) {
        if2 = "También, mencionarle los paquetes brindados y detallados incluyen los siguientes canales: " + pq1 + pq2 + pq3 +
            " por los proximos " + $("#MesPaq").val() + " meses.";
    }

    var speechtext = document.getElementById("SpeechText");
    speechtext.innerHTML =
        "Estimado Sr(a):" + "\n" +
        "En estos momentos se procederá a ingresar la promoción del " + $("#DescuentoCalc").find('option:selected').text() + " de descuento, " +
        "para " + meses + " meses, con fecha de emisión " + nspeech + ", \n" +
        "donde se le descontará:" + "\n- " + cuadrosp + " incluido IGV. " + "\n" +
        if1 +
        "Cabe resaltar que para que la promoción se aplique en los recibos en mención durante ese periodo, " +
        "el servicio se deberá encontrar activo, usted deberá encontrarse al día en sus pagos, " +
        "finalmente durante esos periodos no debe realizar cambio alguno en el servicio ya que de lo contrario esta promoción no podrá aplicarse. " + "\n" +
        if2;

}

function plantillaadelantada() {
    if ($("#RealizoCambioPlanBtn").prop('checked')) {
        var cargofijo = $("#CFCamb").val();
    }
    else {
        var cargofijo = $("#CargoFijoCalc").val();
    }

    if ($("#DescuentoCalc").val() == "") {
        var dsctocal = 0;
    }
    else {
        dsctocal = $("#DescuentoCalc").val();
    }

    var promoadeltext = document.getElementById("PlantillaPromocionAdelantadaNC");
    promoadeltext.innerHTML =
        "Contrato: " + $("#Contrato").val() + "\n" +
        "Código: " + $("#CodigoSGA").val() + "\n" +
        "ID: " + $("#CustomerID").val() + "\n" +
        "Nombre de Cliente: " + $("#NombreCliente").val() + "\n" +
        "Motivo: " + $("#MotivoProm").val() + " - " + $("#DetalleMotivoProm").val() + "\n" +
        "Cargo Fijo Mensual: " + cargofijo + "\n" +
        "Promoción a Aplicar: " + $("#DescuentoCalc").find('option:selected').text() + "\n" +
        "Servicios Afectados: " + $("#ConceptoCalc").val() + " - " + $("#ServicioCalc").val() + "\n" +
        "Nro de Recibo Afectado: " + $("#NroFacturaProm").val() + "\n" +
        "Monto de la N/C (Sin IGV): " + ((cargofijo * dsctocal) / 1.18).toFixed(2) + "\n";
}

function getprice(id1, id2) {
    if ($(id1).val() == "FOX+") {
        $(id2).val("35");
    }
    else if ($(id1).val() == "HBO") {
        $(id2).val("35");
    }
    else if ($(id1).val() == "HD MAX") {
        $(id2).val("35");
    }
    else if ($(id1).val() == "GOLDEN PREMIER") {
        $(id2).val("10");
    }
    else if ($(id1).val() == "HOTPACK HD") {
        $(id2).val("30");
    }
    else if ($(id1).val() == "NHK") {
        $(id2).val("20");
    }
    else if ($(id1).val() == "FIGHTING SPORTS NETWORK") {
        $(id2).val("17");
    }
    else if ($(id1).val() == "UPGRADE DE HOGAR A CINE HD DIGITAL") {
        $(id2).val("35");
    }
    else {
        $(id2).val("0");
    }
}

$(document).on("click", '#PlantillaPromocionSIACBtn', function (e) {
    plantillasiac();
});

$(document).on("click", '#ActualizarSIAC', function (e) {
    plantillasiac();
});

$(document).on("click", '#ActualizarAdelantada', function (e) {
    plantillaadelantada();
});

$(document).on("click", '#PromocionAdelantadaBtn', function (e) {
    plantillaadelantada();
});

$(document).on("change", '#ConceptoCalc', function (e) {
    $("#ServicioCalc").val("");
    if ($("#ConceptoCalc").val() == "1PLAY") {
        $("#ServicioCalc option[value='INTERNET']").show();
        $("#ServicioCalc option[value='TV']").show();
        $("#ServicioCalc option[value='TELEFONÍA']").show();
        $("#ServicioCalc option[value='INT+TLF']").hide();
        $("#ServicioCalc option[value='INT+TV']").hide();
        $("#ServicioCalc option[value='TV+TLF']").hide();
        $("#ServicioCalc option[value='INT+TV+TLF']").hide();
    }
    else if ($("#ConceptoCalc").val() == "2PLAY") {
        $("#ServicioCalc option[value='INTERNET']").hide();
        $("#ServicioCalc option[value='TV']").hide();
        $("#ServicioCalc option[value='TELEFONÍA']").hide();
        $("#ServicioCalc option[value='INT+TLF']").show();
        $("#ServicioCalc option[value='INT+TV']").show();
        $("#ServicioCalc option[value='TV+TLF']").show();
        $("#ServicioCalc option[value='INT+TV+TLF']").hide();
    }
    else if ($("#ConceptoCalc").val() == "3PLAY") {
        $("#ServicioCalc option[value='INTERNET']").hide();
        $("#ServicioCalc option[value='TV']").hide();
        $("#ServicioCalc option[value='TELEFONÍA']").hide();
        $("#ServicioCalc option[value='INT+TLF']").hide();
        $("#ServicioCalc option[value='INT+TV']").hide();
        $("#ServicioCalc option[value='TV+TLF']").hide();
        $("#ServicioCalc option[value='INT+TV+TLF']").show();
    }
});

$(document).on("change", '#CicloCalc', function (e) {

    var d = new Date();
    var n = d.getDate();

    if ($("#CicloCalc").val() == n + 1) {

        if ($("#RealizoCambioPlanBtn").prop('checked')) {
            var cargofijo = $("#CFCamb").val();
        }
        else {
            var cargofijo = $("#CargoFijoCalc").val();
        }

        if ($("#DescuentoCalc").val() == "") {
            var dsctocal = 0;
        }
        else {
            dsctocal = $("#DescuentoCalc").val();
        }

        var alertatext = document.getElementById("MensajeAlerta");
        alertatext.innerHTML =
            "Estimado(a):" + "\n" +
            "Por favor priorizar la atención del caso:" + "\n\n" +
            "CUSTOMER ID: " + $("#CustomerID").val() + "\n" +
            "CONTRATO: " + $("#Contrato").val() + "\n" +
            "CLIENTE: " + $("#NombreCliente").val() + "\n" +
            "CICLO: " + $("#CicloCalc").val() + "\n" +
            "NRO DE CASO: " + "" + "\n" +
            "MONTO DE DSCTO: " + (cargofijo * dsctocal) / 1.18 + "\n";

        $('#myModal').modal();
    }
});

$(document).on("change", '#MotivoProm', function (e) {
    if ($("#MotivoProm").val() == "ECONÓMICO") {
        $("#DetalleMotivoProm").attr("placeholder", "DETALLAR EL MOTIVO DE LA PROMOCIÓN ADELANTADA ESPECÍFICAMENTE");
    }
    else if ($("#MotivoProm").val() == "PROBLEMA TÉCNICO") {
        $("#DetalleMotivoProm").attr("placeholder", "DETALLAR SERVICIOS AFECTADOS Y PROBLEMA TÉCNICO");
    }
    else if ($("#MotivoProm").val() == "PROBLEMA ADMINISTRATIVO") {
        $("#DetalleMotivoProm").attr("placeholder", "ESPECIFICAR LA INTERACCIÓN. DETALLAR BREVEMENTE EL CASO");
    }
    else {
        $("#DetalleMotivoProm").attr("placeholder", "");
    }
});

$(document).on("change", '#PaquetesPaq1', function (e) {
    if ($("#PaquetesPaq1").val() == $("#PaquetesPaq2").val() || $("#PaquetesPaq1").val() == $("#PaquetesPaq3").val()) {
        $("#PaquetesPaq1").val("");
        $("#CostoPaq1").val("");
        alert("No puede escoger el mismo paquete.");
    }
    else {
        getprice('#PaquetesPaq1', '#CostoPaq1');
    }

});

$(document).on("change", '#PaquetesPaq2', function (e) {
    if ($("#PaquetesPaq2").val() == $("#PaquetesPaq1").val() || $("#PaquetesPaq2").val() == $("#PaquetesPaq3").val()) {
        $("#PaquetesPaq2").val("");
        $("#CostoPaq2").val("");
        alert("No puede escoger el mismo paquete.");
    }
    else {
        getprice('#PaquetesPaq2', '#CostoPaq2');
    }

});

$(document).on("change", '#PaquetesPaq3', function (e) {
    if ($("#PaquetesPaq3").val() == $("#PaquetesPaq1").val() || $("#PaquetesPaq3").val() == $("#PaquetesPaq2").val()) {
        $("#PaquetesPaq3").val("");
        $("#CostoPaq3").val("");
        alert("No puede escoger el mismo paquete.");
    }
    else {
        getprice('#PaquetesPaq3', '#CostoPaq3');
    }

});

$(document).on("click", '#SpeechBtn', function (e) {
    plantillasiac();

});

$(document).on("click", '#ClosePlantillaG', function (e) {
    $('#DescuentoCanalesPremiumDiv').modal('toggle');
});

$(document).on("change", '#DescuentoCalc', function (e) {
    if ($("#DescuentoCalc").val() == "0.75") {
        $("#MesAplicarCalc option[value='3']").hide();
        $("#MesAplicarCalc option[value='4']").hide();
        $("#MesAplicarCalc").val("");
    }
    else {
        $("#MesAplicarCalc option[value='3']").show();
        $("#MesAplicarCalc option[value='4']").show();
        $("#MesAplicarCalc").val("");
    }
});


$(document).on("click", '#BtnBuscarEdificio', function (e) {

    if ($("#DireccionEdificio").val() == "" && $("#DistritoEdificio").val() == "") {
        alert("Ingrese al menos un campo.");
    }

    else {
        $.ajax({
            type: "GET",
            url: "../Gestion/GetListaEdificios",
            data: { Direccion: $("#DireccionEdificio").val(), Distrito: $("#DistritoEdificio").val() },
            success: function (data) {
                $("#PartialEdificios").html(data);
            },
            error: function (xhr, status, error) {
                alert(error);
            }
        });
    }

});