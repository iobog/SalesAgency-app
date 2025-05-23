namespace SalesAgency.Entities.DTO.User;

public class GetUserDTO
{
  public int Id { get; set; }
  public string? Name { get; set; }
  public string? Email { get; set; }
  public string? Password { get; set; }
  public int? IsAdmin { get; set; }
}