namespace SistemaInventario.Models
{
    public class InvoiceDetailModel
    {
        public int Id { get; set; }
        public int Cantidad { get; set; }
        public float Valor { get; set; }

        public int ProductModelId { get; set; }
        public ProductModel ProductModel { get; set; }

        public int InvoiceModelId { get; set; }
        public InvoiceModel InvoiceModel { get; set; }

        public int StockModelId { get; set; }
        public StockModel StockModel { get; set; }

    }
}
