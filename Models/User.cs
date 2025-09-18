using System;
using System.ComponentModel.DataAnnotations;

namespace PixnoriaStore.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Ad gereklidir!")]
        public string Ad { get; set; }

        [Required(ErrorMessage = "Soyad gereklidir!")]
        public string Soyad { get; set; }

        [Required(ErrorMessage = "E-posta gereklidir!")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta girin!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Şifre gereklidir!")]
        public string Sifre { get; set; }

 

        public DateTime KayitTarihi { get; set; }
        public DateTime? SonGirisTarihi { get; set; }
    }
}