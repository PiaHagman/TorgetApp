﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Torget__Blocket_klon_.Data.Models;

public class Annons
{
    public int Id { get; set; }
    [Required] public string Titel { get; set; }
    [Required] public string Kategori { get; set; }
    [Required] public string Beskrivning { get; set; }
    [Required] public int Pris { get; set; }

    public DateTime DatumUpplagd { get; } = DateTime.Now;
    public DateTime DatumUppdaterad { get; set; }

    [InverseProperty("Annonser")] public TorgetUser TorgetUser { get; set; }

    [InverseProperty("SparadeAnnonser")] public List<TorgetUser>? UsersSomSparatAnnons { get; set; }
    public List<Bild> Bilder { get; set; }
    public List<Tag> Taggar { get; set; }
}