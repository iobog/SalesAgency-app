namespace SalesAgency.DTO.Order;


public class CreateOrderDTO
{
  public int ClientId { get; set; }
  public string Address { get; set; }
  public List<CreateOrderProductDTO> Products { get; set; }
}