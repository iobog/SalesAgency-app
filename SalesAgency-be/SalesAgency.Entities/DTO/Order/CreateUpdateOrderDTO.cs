namespace SalesAgency.Entities.DTO.Order;



public class CreateUpdateOrderDTO
{
  public int? UserId { get; set; }
  public int? ClientId { get; set; }
  public string? Adress { get; set; }
  public decimal? Total { get; set; }
}
