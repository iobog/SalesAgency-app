namespace SalesAgency.Entities.DTO.Product;

public class CreateUpdateProductDTO
{
  
  public string? Name { get; set; }
  public decimal? Pret { get; set; }
  public string? Description { get; set; }
  public int? Stock { get; set; }

}