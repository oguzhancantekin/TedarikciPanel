using System;
using System.ComponentModel.DataAnnotations;

namespace TedarikciPanel.Models
{
    public class Musteri
    {
        public int Id { get; set; }

        [Required]
        public string AdSoyad { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        public string Telefon { get; set; }

        public string SirketAdi { get; set; }

        public string Sifre { get; set; } // Şimdilik düz şifre

        public DateTime KayitTarihi { get; set; } = DateTime.Now;
    }
}
