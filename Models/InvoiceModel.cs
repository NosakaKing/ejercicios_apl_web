namespace SistemaInventario.Models
{
        public class InvoiceModel
        {
            public int Id { get; set; }
            public DateOnly FechaIngreso { get; set; }
            public int NumeroFactura { get; set; }

            public int ClientesModelId { get; set; }
            public ClientModel? ClientesModel { get; set; }
        }

    }
