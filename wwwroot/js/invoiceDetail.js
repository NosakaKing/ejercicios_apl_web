class InvoiceDetail {
    constructor() {

    }

    listaInvoiceDetail() {
        var html = "<option value= 0> Seleccione una opcion</option>";
        $.get("Client/ListaClientes", (listaClientes) => {
            console.log(listaClientes)
            $.each(listaClientes, (index, valor) => {
                html += `<option value=${valor.id}>${valor.nombre}</option>`
            })
            $("#listacliente").html(html)
        })
    }

    unCliente(id) {
        $.get("Client/UnCliente?id="+id, (cliente) => {
            console.log(cliente)
            $("#nombreCliente").val(cliente.nombre)
            $("#direccionCliente").val(cliente.direccion)
            $("#telefonoCliente").val(cliente.telefono)
            $("#emailCliente").val(cliente.email)
        })

    }

    nuevoCliente() {
        $("#nombreCliente").prop("disabled", false)
        $("#direccionCliente").prop("disabled", false)
        $("#telefonoCliente").prop("disabled", false)
        $("#emailCliente").prop("disabled", false)

        $("#listacliente").prop("disabled", true)
        $("#nuevoCliente").val(1)
        $("#cancelar").css("display", "block")
    }

    listaProductos() {
        var html = ""
        //Tabla
        $.get("Product/ListaProductos", (listaProductos) => {
            console.log(listaProductos)
            $.each(listaProductos, (index, stock) => {
                html += `<tr>
                            <td>${index + 1}</td>
                            <td>${stock.product.nombreProducto}</td>
                            <td>
                        <input type="number" id="stock-${stock.id}" onfocusout="controlarStock(this)" class="form-control" style="width:60px">
                            </td>
                            <td>${stock.cantidad}</td>
                            <td>${stock.precioVenta}</td><
                            <td><button class="btn btn-primary" onclick="cargarProductosLista(${stock.id}, '${stock.product.nombreProducto}', ${stock.precioVenta})">Agregar</button></td>
                            </td>
                        </tr>`
            })
            $("#listaProductos").html(html)
        })
    }

    buscarProducto() {
        var textoBusqueda = $("#buscador").val().toLowerCase();
        $("#listaProductos tr").filter(function () {
            $(this).toggle($(this).text().toLowerCase().indexOf(textoBusqueda) > -1);
        });
    }

    cargarProductosLista(id, nombre, precio) {
        console.log(id + "esta es el id")
        var cantidad = $(`#stock-${id}`).val()
        console.log(cantidad + "esto es la cantidad")
        if (!cantidad || cantidad <= 0) {
            alert("Ingrese una cantidad valida")
            return
        }
        let contador = 0;
        $("#listaProductosFactura tr").each(function () {
            contador = parseInt($(this).find("td:eq(0)").text())
        });

        var html = `<tr>
            <td>
            ${contador + 1}
            </td>
            <td>
            ${nombre}
            </td>
            <td>
            ${precio}
            </td>
            <td>
            ${cantidad}
            </td>
            <td>
            ${precio * cantidad}
            </td>
            </tr>`;
         $("#listaProductosFactura").append(html)
        $("#modalProducto").modal("hide")
        actualizarTotal();
    }

    eliminarfila(boton) {
        $(boton).closest("tr").remove();
        actualizarTotal();
    }

    actualizarTotal() {
        let subtotal = 0;
        $("#listaProductosFactura tr").each(function () {
            const valor = parseFloat($(this).find("td:eq(4)").text());
            if (!isNaN(valor)) {
                subtotal += valor;
            }
        });
        let descuentoIngreado = $("#descuentoId").val();
        let descuento = 0;
        if (descuentoIngreado > 0) {
            descuento = (parseFloat(descuentoIngreado) * subtotal) / 100;
        }
        let subtotalcondescuento = subtotal - descuento;
        let iva = subtotalcondescuento * 0.15;
        let total = iva + subtotalcondescuento;

        $("#subtotal").text(subtotal);
        $("#descuento").text(descuento);
        $("#iva").text(iva.toFixed(2));
        $("#total").text(total.toFixed(2));
    }


    controlarStock(id, cantidad) {
        if (cantidad <= 0 || !cantidad || cantidad == null) return
        id = id.split("-")
       
        console.log(id + "stock")
        $.post("Stock/controlarStock", { id: id[1], cantidad: cantidad }, (dato) => {
            if (!dato) {
                alert("No se encuentra la cantidad en stock")
                $("#btn_agregar").prop("disabled", true)
            }
        })
    }




    limpiarCampos() {
        $("#nombreCliente").prop("disabled", true)
        $("#direccionCliente").prop("disabled", true)
        $("#telefonoCliente").prop("disabled", true)
        $("#emailCliente").prop("disabled", true)
        $("#listacliente").prop("disabled", false)
        $("#nuevoCliente").val(0)
        $("#cancelar").css("display", "none")
    }

    async guardarFactura() {
        try {
            // Validar que haya productos
            if ($("#listaProductosFactura tr").length === 0) {
                alert("Debe agregar al menos un producto a la factura");
                return;
            }

            // Validar que haya un cliente seleccionado
            let clienteId = $("#listacliente").val();
            if (!clienteId || clienteId === "0") {
                alert("Debe seleccionar un cliente");
                return;
            }

            let esNuevoCliente = $("#nuevoCliente").val() === "1";
            let cliente = {
                id: clienteId,
                nombre: $("#nombreCliente").val(),
                direccion: $("#direccionCliente").val(),
                telefono: $("#telefonoCliente").val(),
                email: $("#emailCliente").val(),
                esNuevo: esNuevoCliente,
            };

            let productos = [];
            $("#listaProductosFactura tr").each(function () {
                let producto = {
                    cantidad: parseInt($(this).find("td:eq(1)").text()),
                    nombreProducto: $(this).find("td:eq(2)").text().trim(),
                    precioUnitario: parseFloat($(this).find("td:eq(3)").text()),
                    total: parseFloat($(this).find("td:eq(4)").text()),
                };
                productos.push(producto);
            });

            // Obtener totales
            let totales = {
                subtotal: parseFloat($("#subtotal").text()),
                descuento: parseFloat($("#descuento").text()),
                iva: parseFloat($("#iva").text()),
                total: parseFloat($("#total").text()),
            };

            // Crear objeto factura
            let factura = {
                cliente: cliente,
                productos: productos,
                totales: totales,
            };

            console.log("Datos a enviar:", factura); 

            const response = await $.ajax({
                url: "../../DetalleFactura/GuardarFactura",
                type: "POST",
                data: JSON.stringify(factura),
                contentType: "application/json",
            });


            if (response.success) {
                alert("Factura guardada exitosamente");
                $("#cuerpoTabla").empty();
                $("#subtotal").text("0.00");
                $("#descuento").text("0.00");
                $("#iva").text("0.00");
                $("#total").text("0.00");
                $("#descuentoId").val("");
                limpiarcajas();
            } else {
                alert("Error al guardar la factura: " + response.message);
            }
        } catch (error) {
            console.error("Error:", error);
            alert("Error al guardar la factura: " + error.message);
        }
}
}