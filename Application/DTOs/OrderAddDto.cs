using System.ComponentModel.DataAnnotations;

namespace Application.DTOs;

public class OrderAddDto
{
    [Required]
    public int UserId { get; set; }
    [Required]
    public DateTime OrderDate { get; set; }
    public decimal? OrderCost { get; set; }
    public string ItemsDescription { get; set; }
    public string ShippingAddress { get; set; }
}