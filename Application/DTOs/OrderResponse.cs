namespace Application.DTOs;

public class OrderResponse
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string UserLogin { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal? OrderCost { get; set; }
    public string ItemsDescription { get; set; }
    public string ShippingAddress { get; set; }
}