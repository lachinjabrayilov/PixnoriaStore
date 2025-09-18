using PixnoriaStore.Controllers;
using System;
using System.Collections.Generic;

namespace PixnoriaStore.Models
{
    public class SiparisViewModel
    {
        public int Id { get; set; }
        public DateTime Tarih { get; set; }
        public string Ozet { get; set; }
        public string Alici { get; set; }
        public decimal Toplam { get; set; }
        public string TeslimDurumAciklama { get; set; }
        public List<UrunViewModel> Urunler { get; set; }
    }
}