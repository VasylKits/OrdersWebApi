using System.ComponentModel.DataAnnotations;

namespace Application.DTOs;

public class OrderEditDto
{
    [Required]
    public int Id { get; set; }
    [Required]
    public int UserId { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal? OrderCost { get; set; }
    public string ItemsDescription { get; set; }
    public string ShippingAddress { get; set; }
}