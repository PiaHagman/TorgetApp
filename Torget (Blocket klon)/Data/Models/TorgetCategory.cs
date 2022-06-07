using System.ComponentModel.DataAnnotations;

namespace Torget__Blocket_klon_.Data.Models;

public class TorgetCategory
{
    private string _name;

    [Key] public string Name { get => _name; set => _name = value.ToUpper(); }
}