using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System;
using System.IO;
using System.Linq;
using TedarikciPanel.Models;

namespace TedarikciPanel.Helpers
{
    public class FaturaPdfGenerator
    {
        public static byte[] FaturaOlustur(Order siparis)
        {
            if (siparis == null)
                throw new Exception("Sipariş nesnesi null.");

            if (siparis.Detaylar == null || !siparis.Detaylar.Any())
                throw new Exception("Siparişin detayları yok. PDF oluşturulamaz.");

            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(30);

                    page.Header().Text($"FATURA - Sipariş #{siparis.Id}")
                        .FontSize(18)
                        .Bold()
                        .AlignCenter();

                    page.Content().PaddingVertical(10).Column(col =>
                    {
                        col.Item().Text($"Tarih: {siparis.Tarih:dd.MM.yyyy}");
                        col.Item().Text($"Durum: {siparis.Durum}");
                        col.Item().Text($"Toplam Tutar: {siparis.ToplamTutar:N2} ₺");
                        col.Item().PaddingVertical(10).Element(container =>
                        {
                            container.LineHorizontal(1).LineColor(Colors.Grey.Lighten1);
                        });


                        col.Item().Table(table =>
                        {
                            table.ColumnsDefinition(cols =>
                            {
                                cols.RelativeColumn(4); // Ürün
                                cols.RelativeColumn(2); // Adet
                                cols.RelativeColumn(2); // Fiyat
                                cols.RelativeColumn(2); // Ara Toplam
                            });

                            table.Header(header =>
                            {
                                header.Cell().Element(CellStyleHeader).Text("Ürün");
                                header.Cell().Element(CellStyleHeader).Text("Adet");
                                header.Cell().Element(CellStyleHeader).Text("Birim Fiyat");
                                header.Cell().Element(CellStyleHeader).Text("Ara Toplam");
                            });

                            foreach (var detay in siparis.Detaylar)
                            {
                                table.Cell().Element(CellStyleBody).Text(detay.UrunAd ?? "-");
                                table.Cell().Element(CellStyleBody).Text(detay.Adet.ToString());
                                table.Cell().Element(CellStyleBody).Text($"{detay.Fiyat:N2} ₺");
                                table.Cell().Element(CellStyleBody).Text($"{(detay.Fiyat * detay.Adet):N2} ₺");
                            }

                            IContainer CellStyleHeader(IContainer container) =>
                                container.DefaultTextStyle(x => x.Bold())
                                         .PaddingVertical(5)
                                         .Background(Colors.Grey.Lighten3)
                                         .BorderBottom(1)
                                         .BorderColor(Colors.Grey.Lighten1);

                            IContainer CellStyleBody(IContainer container) =>
                                container.PaddingVertical(5);
                        });
                    });

                    page.Footer().AlignRight().Text($"Toplam: {siparis.ToplamTutar:N2} ₺").FontSize(14).Bold();
                });
            });

            using var ms = new MemoryStream();
            document.GeneratePdf(ms);  // burası artık patlamayacak
            return ms.ToArray();
        }
    }
}
