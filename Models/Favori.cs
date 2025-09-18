using PixnoriaStore.Models;
using System.ComponentModel.DataAnnotations.Schema;

public class Favori
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int UrunId { get; set; }

    [ForeignKey("UrunId")]
    public Urun Urun { get; set; }
}