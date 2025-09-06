using System.ComponentModel.DataAnnotations;

namespace TedarikciPanel.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        public string Ad { get; set; }

        public string Aciklama { get; set; }

        [Required]
        public decimal Fiyat { get; set; }

        public int Stok { get; set; }
        public string? ResimYolu { get; set; }  // nullable olması iyidir ilk etapta

    }
}
