using System.ComponentModel.DataAnnotations;

namespace PixnoriaStore.Models
{
    public class Urun
    {
        [Key]
        public int Id { get; set; }
        public string Ad { get; set; }
        public int Adet { get; set; }
        public decimal Fiyat { get; set; }
        public string ResimUrl { get; set; }
        public string Marka { get; set; }
        public string Aciklama { get; set; }
        public string Kategori { get; set; }
        // SiparisId ve Siparis navigation YOK!
    }
}