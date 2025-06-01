using SalesAgency.Entities.Data;

namespace SalesAgency.Entities.DTO.Order;



public class CreateOrderListDTO
{
  public int ClientId { get; set; }
  public string? Address { get; set; }

  // public List<TProduct> Products { get; set; } = new();
  public List<CreateOrderProductDTO> Products { get; set; } = new();

}