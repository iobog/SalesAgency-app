namespace SalesAgency.Entities.DTOs.Product;

public class GetProductDTO
{
  public int Id { get; set; }
  public string? Name { get; set; }
  public decimal? Pret { get; set; }
  public string? Description { get; set; }
  public int? Stock { get; set; }
  
}