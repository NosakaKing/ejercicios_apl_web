
$().ready(
    () => {
        invoiceDetail()
    }
);

var invoiceDetail = () => {
    var leerFactura = new InvoiceDetail()
    leerFactura.listaInvoiceDetail()
}

var unCliente = () => {
    var id = $('#listacliente').val()
    var uncliente = new InvoiceDetail()
    uncliente.unCliente(id)
}

var nuevoCliente = () => {
    var nuevoCliente = new InvoiceDetail()
    nuevoCliente.nuevoCliente()
}

var listaProductos = () => {
    var listaProductos = new InvoiceDetail()
    listaProductos.listaProductos()
}

var controlarStock = (caja) => {
    var controlarStock = new InvoiceDetail()
    controlarStock.controlarStock(caja.id, caja.value)

    console.log(caja.id)
    console.log(caja.value)
}

var cargarProductosLista = (id, precio) => {
    var cargarProductosLista = new InvoiceDetail()
    cargarProductosLista.cargarProductosLista()
}

var limpiarCampos = () => {
    var limpiarCampos = new InvoiceDetail()
    limpiarCampos.limpiarCampos()
}