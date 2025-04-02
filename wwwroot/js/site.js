
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

}

var cargarProductosLista = (id, nombre, precio) => {
    var cargarProductosLista = new InvoiceDetail()
    cargarProductosLista.cargarProductosLista(id, nombre, precio)

    console.log(id, nombre, precio)
}

var limpiarCampos = () => {
    var limpiarCampos = new InvoiceDetail()
    limpiarCampos.limpiarCampos()
}

var buscarProducto = () => {
    var buscarProducto = new InvoiceDetail()
    buscarProducto.buscarProducto()
}

var eliminarfila = (boton) => {
    var eliminarfila = new InvoiceDetail()
    eliminarfila.eliminarfila(boton)
}

var actualizarTotal = () => {
    var actualizarTotal = new InvoiceDetail()
    actualizarTotal.actualizarTotal()
}

var imprimirFactura = () => {
    var contenidoOrignal = document.body.innerHTML;

    var imprimir = document.getElementById("imprimir").innerHTML;

    document.body.innerHTML = imprimir;

    window.print();

    document.body.innerHTML = contenidoOrignal;

}