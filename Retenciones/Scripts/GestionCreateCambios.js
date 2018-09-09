//SOLO REEMPLAZAR EN GestionCreate (Verificar si no hubo algún cambio)
$(document).ready(function () {
    $("input[name=RbTipoClienteProm]").val(["SGA"]);
    $("#DescuentoCalc option[value='0.50']").hide();
    $("#DescuentoCalc option[value='50']").hide();
});

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

    var meses = $("#MesAplicarCalc").val();

    var mesespaq = $("#MesPaq").val();

    if (c > z) {
        var x = d.getMonth() + 1;
    }

    else if (c <= z) {
        var x = d.getMonth() + 2;
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

    var cargofijonuevo = 0;

    if ($("#DescuentoCalc").val() == "50") { dsctonuevo = 50 } else { dsctonuevo = $("#CargoFijoCalc").val() * $("#DescuentoCalc").val() }

    if ($("#PaquetesBtn").prop('checked')) {
        //CUADRO
        var cuadro1 = month[x]
            + " S/. "
            + (+((dsctonuevo) / 1.18).toFixed(2)
                + +(+$("#CostoPaq1").val() / 1.18).toFixed(2)
                + +(((+$("#CostoPaq1").val() * diffDays) / date4) / 1.18).toFixed(2)).toFixed(2) //OCC

            + " (Incluye " + $("#DescuentoCalc").find('option:selected').text() + " al CF: S/. "
            + ((dsctonuevo) / 1.18).toFixed(2) //CF

            + "), (" + $("#PaquetesPaq1").val() + ") al 100% dscto S/."
            + (+$("#CostoPaq1").val() / 1.18).toFixed(2) //PAQUETES

            + " + Prorrateo (" + diffDays + ") días S./"
            + (((+$("#CostoPaq1").val() * diffDays) / date4) / 1.18).toFixed(2) //PRORRATEO

            + "\n";

        if (mesespaq < meses) {
            for (var i = 1; i < cantidadmeses; i++) {

                if (i < mesespaq) {
                    cuadro1 = cuadro1 + " - " + month[x + i]
                        + " S/. "
                        + (+((dsctonuevo) / 1.18).toFixed(2)
                            + +(+$("#CostoPaq1").val() / 1.18).toFixed(2)).toFixed(2) //OCC

                        + " (Incluye " + $("#DescuentoCalc").find('option:selected').text() + " al CF: S/. "
                        + ((dsctonuevo) / 1.18).toFixed(2) //CF

                        + "), (" + $("#PaquetesPaq1").val() + ") al 100% dscto S/."
                        + (+$("#CostoPaq1").val() / 1.18).toFixed(2) //PAQUETES 

                        + "\n";
                }
                else {
                    cuadro1 = cuadro1 + " - " + month[x + i]
                        + " S/. "
                        + +((dsctonuevo) / 1.18).toFixed(2) //OCC

                        + " (Incluye " + $("#DescuentoCalc").find('option:selected').text() + " al CF: S/. "
                        + ((dsctonuevo) / 1.18).toFixed(2) //CF
                        + ")\n";
                }
            }
        }

        else {
            for (var i = 1; i < cantidadmeses; i++) {

                if (i < meses) {
                    cuadro1 = cuadro1 + " - " + month[x + i]
                        + " S/. "
                        + (+((dsctonuevo) / 1.18).toFixed(2)
                            + +(+$("#CostoPaq1").val() / 1.18).toFixed(2)).toFixed(2) //OCC

                        + " (Incluye " + $("#DescuentoCalc").find('option:selected').text() + " al CF: S/. "
                        + ((dsctonuevo) / 1.18).toFixed(2) //CF

                        + "), (" + $("#PaquetesPaq1").val() + ") al 100% dscto S/."
                        + (+$("#CostoPaq1").val() / 1.18).toFixed(2) //PAQUETES 

                        + "\n";
                }
                else {
                    cuadro1 = cuadro1 + " - " + month[x + i]
                        + " S/. "
                        + (+$("#CostoPaq1").val() / 1.18).toFixed(2) //OCC

                        + " (Incluye " + $("#PaquetesPaq1").val() + ") al 100% dscto S/."
                        + (+$("#CostoPaq1").val() / 1.18).toFixed(2) //SOLO PAQUETES

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
            "Paquete Premium Ofrecido: " + $("#PaquetesPaq1").val() + "\n" +
            "Meses de Paquete: " + $("#MesPaq").val() + "\n" +
            "Importes a Aplicar sin IGV: " + "\n - " + cuadro1 + "\n";

        //CUADRO PARA SPEECH
        var cuadrosp = month[x]
            + " S/. "
            + ((dsctonuevo) + +$("#CostoPaq1").val() + ((+$("#CostoPaq1").val() * diffDays) / date4)).toFixed(2)

            + " (Incluye " + $("#DescuentoCalc").find('option:selected').text() + " al CF: S/. " + (dsctonuevo)

            + " (" + $("#PaquetesPaq1").val() + ") al 100% dscto S/."

            + +$("#CostoPaq1").val()

            + " + Prorrateo (" + diffDays + ") días S./" + ((+$("#CostoPaq1").val() * diffDays) / date4).toFixed(2) + "\n";

        if (mesespaq < meses) {
            for (var i = 1; i < cantidadmeses; i++) {

                if (i < mesespaq) {
                    cuadrosp = cuadrosp + " - " + month[x + i]
                        + " S/. "
                        + ((dsctonuevo) + +$("#CostoPaq1").val()).toFixed(2)

                        + " (Incluye " + $("#DescuentoCalc").find('option:selected').text() + " al CF: S/. " + (dsctonuevo)

                        + ") (" + $("#PaquetesPaq1").val() + ") al 100% dscto S/."

                        + +$("#CostoPaq1").val() + "\n";
                }
                else {
                    cuadrosp = cuadrosp + " - " + month[x + i]
                        + " S/. "
                        + (dsctonuevo).toFixed(2)

                        + " (Incluye " + $("#DescuentoCalc").find('option:selected').text() + " al CF: S/. " + (dsctonuevo) + ")\n";
                }
            }
        }

        else {
            for (var i = 1; i < cantidadmeses; i++) {

                if (i < meses) {
                    cuadrosp = cuadrosp + " - " + month[x + i]
                        + " S/. "
                        + ((dsctonuevo) + +$("#CostoPaq1").val()).toFixed(2)

                        + " (Incluye " + $("#DescuentoCalc").find('option:selected').text() + " al CF: S/. " + (dsctonuevo)

                        + ") (" + $("#PaquetesPaq1").val() + ") al 100% dscto S/."

                        + +$("#CostoPaq1").val() + "\n";
                }
                else {
                    cuadrosp = cuadrosp + " - " + month[x + i]
                        + " S/. "
                        + +$("#CostoPaq1").val()
                        + " (Incluye " + $("#PaquetesPaq1").val() + ") al 100% dscto S/."
                        + +$("#CostoPaq1").val() + "\n";
                }
            }
        }

    }

    else {
        //CUADRO
        var cuadro1 = month[x]
            + " S/. "
            + ((dsctonuevo) / 1.18).toFixed(2) //OCC

            + " (Incluye " + $("#DescuentoCalc").find('option:selected').text() + " al CF: S/. "
            + ((dsctonuevo) / 1.18).toFixed(2) //CF

            + ")\n";

        for (var i = 1; i < cantidadmeses; i++) {
            cuadro1 = cuadro1 + " - " + month[x + i]
                + " S/. "
                + ((dsctonuevo) / 1.18).toFixed(2) //OCC

                + " (Incluye " + $("#DescuentoCalc").find('option:selected').text() + " al CF: S/. "
                + ((dsctonuevo) / 1.18).toFixed(2) //CF

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
            + (dsctonuevo)
            + " (Incluye " + $("#DescuentoCalc").find('option:selected').text() + " al CF: S/. " + (dsctonuevo).toFixed(2) + ")\n";

        for (var i = 1; i < cantidadmeses; i++) {
            cuadrosp = cuadrosp + " - " + month[x + i]
                + " S/. "
                + (dsctonuevo)
                + " (Incluye " + $("#DescuentoCalc").find('option:selected').text() + " al CF: S/. " + (dsctonuevo).toFixed(2) + ")\n";
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

    //if ($("#PaquetesPaq2").val() == "") {
    //    pq2 = "";
    //}

    //else {
    //    pq2 = ", " + $("#PaquetesPaq2").find('option:selected').text();
    //}

    //if ($("#PaquetesPaq3").val() == "") {
    //    pq3 = "";
    //}

    //else {
    //    pq3 = ", " + $("#PaquetesPaq3").find('option:selected').text();
    //}

    //
    //if ($("#PromocionAdelantadaBtn").prop('checked')) {
    //    if1 = "Adicional a ello, en la factura del mes de " + month[x - 1] + ", le brindaremos una promoción de manera adelantada, es decir para la factura N° " + $("#NroFacturaProm").val() + " pendiente de pago. ";
    //}

    if ($("#PaquetesBtn").prop('checked')) {
        if2 = "También, mencionarle los paquetes brindados y detallados incluyen los siguientes canales: " + pq1 +
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

$(document).on("change", '#DescuentoCalc', function (e) {
    $("#MesAplicarCalc").val("");

    if ($("#RangoCli").val() == "1") {
        $("#MesAplicarCalc option[value='1']").show();
        $("#MesAplicarCalc option[value='3']").hide();
        $("#MesAplicarCalc option[value='6']").hide();
    }

    else if ($("#RangoCli").val() == "2") {
        $("#MesAplicarCalc option[value='1']").show();
        $("#MesAplicarCalc option[value='3']").show();
        $("#MesAplicarCalc option[value='6']").hide();
    }

    else if ($("#RangoCli").val() == "3") {
        $("#MesAplicarCalc option[value='1']").show();
        $("#MesAplicarCalc option[value='3']").show();
        $("#MesAplicarCalc option[value='6']").show();
    }
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

        if (dsctocal == "50") { dsctonuevo = 50 } else { dsctonuevo = +cargofijo * dsctocal }

        var alertatext = document.getElementById("MensajeAlerta");
        alertatext.innerHTML =
            "Estimado(a):" + "\n" +
            "Por favor priorizar la atención del caso:" + "\n\n" +
            "CUSTOMER ID: " + $("#CustomerIDProm").val() + "\n" +
            "CONTRATO: " + $("#Contrato").val() + "\n" +
            "CLIENTE: " + $("#NombreCliente").val() + "\n" +
            "CICLO: " + $("#CicloCalc").val() + "\n" +
            "NRO DE CASO: " + "" + "\n" +
            "MONTO DE DSCTO: " + ((dsctonuevo) / 1.18).toFixed(2) + "\n";
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

    getprice('#PaquetesPaq1', '#CostoPaq1');
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
    if (!$("#NroCaso").val() || !$("#InteraccPromAnterior").val() || !$("#CicloCalc").val()) {
        alert("Debe llenar el campo Nro de Caso, Última Interacc Prom y Ciclo.");
    }
    else {
        $('#DescuentoCanalesPremiumDiv').modal('toggle');
    }
});

$(document).on("click", '#Soles', function (e) {
    $("#DescuentoCalc option[value='0.50']").hide();
    $("#DescuentoCalc option[value='50']").show();
    $("#DescuentoCalc").val("");
});

$(document).on("click", '#Porcentaje', function (e) {
    $("#DescuentoCalc option[value='50']").hide();
    $("#DescuentoCalc option[value='0.50']").show();
    $("#DescuentoCalc").val("");
});

$(document).on("click", '#PaquetesBtn', function (e) {
    if ($("#PaquetesBtn").prop('checked')) {
        $("#PaquetesBtnDiv").removeClass("col-sm-12");
        $("#PaquetesBtnDiv").addClass("col-sm-2");
        $(".paquetesopen").show();
    }
    else {
        $("#PaquetesBtnDiv").removeClass("col-sm-2");
        $("#PaquetesBtnDiv").addClass("col-sm-12");
        $(".paquetesopen").hide();
    }
});

$(document).on("click", '#RealizoCambioPlanBtn', function (e) {
    if ($("#RealizoCambioPlanBtn").prop('checked')) {
        $("#RealizoCambioPlanBtnDiv").removeClass("col-sm-12");
        $("#RealizoCambioPlanBtnDiv").addClass("col-sm-3");
        $(".cambioopen").show();
    }
    else {
        $("#RealizoCambioPlanBtnDiv").removeClass("col-sm-3");
        $("#RealizoCambioPlanBtnDiv").addClass("col-sm-12");
        $(".cambioopen").hide();
    }
});

$(document).on("click", '#IngresoPromocionesBtn', function (e) {

    var codesga = $("#CodigoSGA").val();

    $("#CodigoClienteProm").val(codesga);

    $.ajax({
        type: "GET",
        url: "../Gestion/GetClienteInfo",
        dataType: "json",
        data: { cod_cli: $("#CodigoClienteProm").val() },
        success: function (data) {

            if (data == "") {
                $("#CustomerIDProm").val("");
                alert("No existen registros con este Código.");
            }

            else {
                $("#CustomerIDProm").val(data.CustomerID);
            }

        },
        error: function (xhr, status, error) {
            alert(error);
        }
    });

    $('#IngresoPromocionesDiv').modal({ backdrop: 'static', keyboard: false });
});

$(document).on("click", '#BtnOKPartial', function (e) {
    $.ajax({
        type: "GET",
        url: "../Gestion/GetClienteTablas",
        data: { cod_cli: $("#CodigoClienteProm").val() },
        success: function (data) {
            $("#Partial").html(data);
        },
        error: function (xhr, status, error) {
            alert(error);
        }
    });
});

$(document).on("click", '.BtnEscoger', function (e) {

    $("#TablesHidden").show();

    $("#DivBtnContinuar").show();

    var mesesprom = +$(this).closest("tr").find(".tdmeses").text();

    var proyx = $(this).closest("tr").find(".tdproyecto").text();

    var antx = $(this).closest("tr").find(".tdantiguedad").text();

    $("#ProyectoProm").val(proyx);

    $("#AntiguedadProm").val(antx);

    //GET RANGOS/MES PAQ FROM PROMOCIONES

    if (mesesprom < 6) {
        $("#RangoCli").val("1");

        $("#MesPaq option[value='1']").show();
        $("#MesPaq option[value='2']").hide();
        $("#MesPaq option[value='3']").hide();
    }

    else if (mesesprom >= 6 && mesesprom <= 17) {
        $("#RangoCli").val("2");

        $("#MesPaq option[value='1']").show();
        $("#MesPaq option[value='2']").show();
        $("#MesPaq option[value='3']").hide();
    }

    else if (mesesprom >= 18) {
        $("#RangoCli").val("3");

        $("#MesPaq option[value='1']").show();
        $("#MesPaq option[value='2']").show();
        $("#MesPaq option[value='3']").show();
    }
});

$(document).on("click", '#BtnContinuar', function (e) {

    var occx = 0;
    var ncsix = 0;
    var prox = 0;
    var ncsgx = 0;

    $('#TbOccSiac tr').each(function () {
        occx = +$(this).find(".tdOccSiacDate").html();
    });

    $('#TbNcSiac tr').each(function () {
        ncsix = +$(this).find(".tdNcSiacDate").html();
    });

    $('#TbPromoSGA tr').each(function () {
        prox = +$(this).find(".tdPromoSGADate").html();
    });

    $('#TbNcSGA tr').each(function () {
        ncsgx = +$(this).find(".tdNcSGADate").html();
    });

    if (occx < 12 || ncsix < 12 || prox < 12 || ncsgx < 12) {
        $('#AlertaPromocionModal').modal();
    }

    else {
        $('#IngresoPromocionesDiv').modal('toggle');
        $('#DescuentoCanalesPremiumDiv').modal({ backdrop: 'static', keyboard: false });
    }
});

$(document).on("click", '#BtnContinuar2', function (e) {
    $('#AlertaPromocionModal').modal('toggle');
    $('#IngresoPromocionesDiv').modal('toggle');
    $('#DescuentoCanalesPremiumDiv').modal({ backdrop: 'static', keyboard: false });
});