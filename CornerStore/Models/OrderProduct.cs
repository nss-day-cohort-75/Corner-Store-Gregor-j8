namespace CornerStore.Models;
using System.ComponentModel.DataAnnotations;

public class OrderProduct
{
    public int Id { get; set; }
    [Required]
    public int ProductId { get; set; }
    public Product Product { get; set; }

    public int OrderId { get; set; }
    public Order Order { get; set; }

    public int Quantity { get; set; }
}