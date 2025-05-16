namespace SalesAgency.DTO.Order;

public class CreateOrderProductDTO
{
  public int ProductId { get; set; }
  public string Name { get; set; }
  public decimal Price { get; set; }
  public int Quantity { get; set; }
}