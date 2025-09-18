using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PixnoriaStore.Models
{
    public class SiparisDetay
    {
        [Key]
        public int Id { get; set; }
        public int SiparisId { get; set; }
        public int UrunId { get; set; }
        public int Adet { get; set; }
        public decimal Fiyat { get; set; }

        [ForeignKey("UrunId")]
        public virtual Urun Urun { get; set; }
        [ForeignKey("SiparisId")]
        public virtual Siparis Siparis { get; set; }
    }
}