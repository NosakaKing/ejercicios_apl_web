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
        var html = `<tr>
            <td>
            #
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
            </tr>`
        $("#listaProductosFactura").append(html)
        $("#modalProducto").modal("hide")
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
}