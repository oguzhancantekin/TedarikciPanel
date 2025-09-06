using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TedarikciPanel.Models
{
    public class CustomerRequest
    {
        public int Id { get; set; }

        [Required]
        public string AdSoyad { get; set; }

        [Required]
        public string SirketAdi { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Telefon { get; set; }

        [Required]
        public string Sifre { get; set; }

        [NotMapped]
        [Compare("Sifre", ErrorMessage = "Şifreler uyuşmuyor.")]
        public string SifreTekrar { get; set; }

        public bool IsApproved { get; set; } = false;
    }
}
