namespace CornerStore.Models;
using System.ComponentModel.DataAnnotations;

public class Product
{
    public int Id { get; set; }
    [Required]
    public string ProductName { get; set; }
    public decimal Price { get; set; }
    public string Brand { get; set; }

    public int CategoryId { get; set; }

    public Category Category { get; set; }
}