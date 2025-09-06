using System;
using System.Collections.Generic;

namespace TedarikciPanel.Models
{
    public class Order
    {
        public int Id { get; set; }

        public int MusteriId { get; set; } // Session’dan çekilecek

        public DateTime Tarih { get; set; }

        public decimal ToplamTutar { get; set; }

        public string Durum { get; set; } // “Onaylandı”, “Hazırlanıyor” vs.

        public List<OrderDetail> Detaylar { get; set; }
    }
}
