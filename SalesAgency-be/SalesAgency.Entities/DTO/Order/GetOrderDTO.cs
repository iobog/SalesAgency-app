using SalesAgency.Entities.DTO.Client;

namespace SalesAgency.Entities.DTO.Order;


public class GetOrderDTO
{
  public int Id { get; set; }
  public int? UserId { get; set; }
  public ClientDTO? Client { get; set; }
  public string? Adress { get; set; }
  public decimal? Total { get; set; }
  public DateTime? CreatedAt { get; set; } 
}
