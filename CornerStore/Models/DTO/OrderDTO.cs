using System;
using System.Collections.Generic;

namespace CornerStore.Models.DTO;

public class OrderDTO
{
    public int Id { get; set; }
    public int CashierId { get; set; }
    public CashierDTO Cashier { get; set; }
    public DateTime? PaidOnDate { get; set; }
    public List<OrderProductDTO> OrderProducts { get; set; }
    public decimal Total => OrderProducts?.Sum(op => (op.Product?.Price ?? 0m) * op.Quantity) ?? 0m;
    }