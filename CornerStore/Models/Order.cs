
namespace CornerStore.Models;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

public class Order
{
    public int Id { get; set; }
    [Required]
    public int CashierId { get; set; }
    public Cashier Cashier { get; set; }
    public DateTime? PaidOnDate { get; set; }
    public List<OrderProduct> OrderProducts { get; set; }
    public decimal Total => OrderProducts?.Sum(op => (op.Product?.Price ?? 0m) * op.Quantity) ?? 0m;
}