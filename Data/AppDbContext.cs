using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using TedarikciPanel.Models;

namespace TedarikciPanel.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<CustomerRequest> CustomerRequests { get; set; }
        public DbSet<Musteri> Musteriler { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Siparisler { get; set; }
        public DbSet<OrderDetail> SiparisDetaylar { get; set; }


        // Diğer tablolar geldikçe buraya eklenecek
    }
}
