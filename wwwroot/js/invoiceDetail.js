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
                            <input type="number" class="form-control" id="stock-${stock.id}" onfocusout="controlarStock(this)" value="1" style="width: 50px">
                            </td>
                            <td>${stock.precioUnitario}</td>
                            <td>${stock.precioVenta}</td><
<td><button class="btn btn-primary" onclick="cargarProductosLista(${stock.id}, ${stock.precioVenta})">Agregar</button></td>
                        </tr>`
            })
            $("#listaProductos").html(html)
        })
    }

    cargarProductosLista(id, precio) {
        var cantidad = $(`#stock-${id}`).val()
        console.log(cantidad)
        if (!cantidad || cantidad <= 0) {
            alert("Ingrese una cantidad valida")
            return
        }
        alert(cantidad * precio)
    }


    controlarStock(id, cantidad) {
        if (cantidad <= 0 || !cantidad || cantidad == null) return
        id = id.split("-")
       
        console.log(id + "stock")
        $.post("Stock/controlarStock", { id: id, cantidad: cantidad }, (dato) => {
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