namespace Domain.Entities;

public class Order
{
    public int Id { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal OrderCost { get; set; }
    public string? ItemsDescription { get; set; }
    public string? ShippingAddress { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
}