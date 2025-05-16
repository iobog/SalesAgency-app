namespace SalesAgency.Entities.DTO.Order;


public class GetOrderListItemDTO
{
  public int Id { get; set; }
  public string Client { get; set; }
  public DateTime CreatedAt { get; set; }
  public decimal Total { get; set; }
  public int CountProducts { get; set; }
  public string User { get; set; }
}