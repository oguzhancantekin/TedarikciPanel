namespace TedarikciPanel.Models
{
    public class OrderDetail
    {
        public int Id { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; }

        public string UrunAd { get; set; }
        public int Adet { get; set; }
        public decimal Fiyat { get; set; }
    }
}
