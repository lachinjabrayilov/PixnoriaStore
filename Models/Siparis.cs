using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PixnoriaStore.Models
{
    public class Siparis
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Alici { get; set; }
        public DateTime Tarih { get; set; }
        public string TeslimDurumu { get; set; }

        // Sipariş detayları: Her bir ürünü ve adedini/fiyatını tutar
        public virtual List<SiparisDetay> SiparisDetaylar { get; set; } = new List<SiparisDetay>();
    }
}