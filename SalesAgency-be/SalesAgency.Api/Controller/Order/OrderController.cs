using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesAgency.Entities.Data;
using SalesAgency.Entities.DTO.Order;

namespace SalesAgency.Api.Controller.Order;


[Route("api/orders")]
[ApiController]
[Authorize]
public class OrderController : ControllerBase
{
  private readonly AppDbContext _db;
  private readonly IMapper _mapper;

  public OrderController(AppDbContext db, IMapper mapper)
  {
    _db = db;
    _mapper = mapper;
  }

  [HttpGet]
  public async Task<ActionResult<IEnumerable<GetOrderListItemDTO>>> GetAllOrders()
  {
    try
    {
      var orders = await _db.TOrders
        .ProjectTo<GetOrderListItemDTO>(_mapper.ConfigurationProvider)
        .ToListAsync();


      return Ok(orders);
    }
    catch (Exception e)
    {
      return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
    }
  }
  
  [HttpPost]
  public async Task<ActionResult<GetOrderDTO>> CreateOrder([FromBody] CreateOrderListDTO createOrderListDTO)
  {
    try
    {

      var userIdClaim = User.FindFirst("UserId");
      if (userIdClaim == null)
      {
        return Unauthorized("User not found in token.");
      }

      var userId = int.Parse(userIdClaim.Value);

      var client = await _db.TClients.FindAsync(createOrderListDTO.ClientId);

      if (client == null)
      {
        return NotFound("Client not found!");
      }


      var productIds = createOrderListDTO.Products.Select(p => p.ProductId).ToList();

      var productsInDb = await _db.TProducts
        .Where(p => productIds.Contains(p.Id))
        .ToListAsync();

      if (productsInDb.Count != productIds.Count)
      {
        return BadRequest("One or more products not found.");

      }
      List<string> errorMessages = new();

      decimal total = 0;

      foreach (var requestedProduct in createOrderListDTO.Products)
      {

        var dbProduct = productsInDb.First(p => p.Id == requestedProduct.ProductId);
        if (requestedProduct.Quantity <= 0)
        {
          errorMessages.Add($"Cantitate invalida pentru '{dbProduct.Name}'");
          continue;
        }
    
        if (requestedProduct.Quantity > dbProduct.Stock)
        {
          errorMessages.Add($"Cantitate insuficienta pentru '{dbProduct.Name}' (Stoc: {dbProduct.Stock})");
          continue;
        }
      
        total += dbProduct.Price.GetValueOrDefault() * requestedProduct.Quantity;
      }

      if (errorMessages.Any())
      {
        return BadRequest(new { Errors = errorMessages });
      }

      foreach (var requestedProduct in createOrderListDTO.Products)
      {
        var dbProduct = productsInDb.First(p => p.Id == requestedProduct.ProductId);
        dbProduct.Stock -= requestedProduct.Quantity;
      }

      var newOrder = new TOrder
      {
        ClientId = createOrderListDTO.ClientId,
        Adress = createOrderListDTO.Address,
        Total = total,
        CreatedAt = DateTime.UtcNow,
        UserId = userId
      };

      await _db.TOrders.AddAsync(newOrder);
      await _db.SaveChangesAsync();

      var orderProducts = createOrderListDTO.Products.Select(p => new TOrderProduct
      {
        OrderId = newOrder.Id,
        ProductId = p.ProductId,
        Quantity = p.Quantity
      }).ToList();

      _db.TOrderProducts.AddRange(orderProducts);
      await _db.SaveChangesAsync();
      return Ok(new { message = "Order created successfully", orderId = newOrder.Id });
    }
    catch (Exception e)
    {
      return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
    }
}


  
  // [HttpPost]
  // public async Task<ActionResult<GetOrderDTO>> CreateOrder([FromBody] CreateOrderListDTO createOrderListDTO)
  // {
  //   try
  //   {

  //     var userIdClaim = User.FindFirst("UserId");
  //     if (userIdClaim == null)
  //     {
  //       return Unauthorized("User not found in token.");
  //     }

  //     var userId = int.Parse(userIdClaim.Value);



  //     var client = await _db.TClients.FindAsync(createOrderListDTO.ClientId);

  //     if (client == null)
  //     {
  //       return NotFound("Client not Found!");
  //     }

  //     var productIds = createOrderListDTO.Products.Select(_ => _.ProductId).ToList();

  //     var productsInDb = await _db.TProducts
  //       .Where(p => productIds.Contains(p.Id) )
  //       .ToListAsync();

  //     foreach (var p in productsInDb)
  //     {

  //     }


  //     if (productsInDb.Count != createOrderListDTO.Products.Count)
  //       {
  //         return BadRequest("Product not Found!");
  //       }

  //     var total = createOrderListDTO.Products.Sum(_ => _.Price * _.Quantity);

  //     var newOrder = new TOrder
  //     {
  //       ClientId = createOrderListDTO.ClientId,
  //       Adress = createOrderListDTO.Address,
  //       Total = total,
  //       CreatedAt = DateTime.UtcNow,
  //       UserId = userId
  //     };

  //     _db.TOrders.Add(newOrder);
  //     await _db.SaveChangesAsync();

  //     var orderProducts = createOrderListDTO.Products.Select(p => new TOrderProduct
  //     {
  //       OrderId = newOrder.Id,
  //       ProductId = p.ProductId,
  //       Quantity = p.Quantity
  //     }).ToList();

  //     _db.TOrderProducts.AddRange(orderProducts);
  //     await _db.SaveChangesAsync();

  //     return Ok(new { message = "Order created successfully", orderId = newOrder.Id });

  //   }
  //   catch (Exception e)
  //   {
  //     return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
  //   }
  // }

}