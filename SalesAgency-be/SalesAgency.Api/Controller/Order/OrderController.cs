using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesAgency.Entities.Data;
using SalesAgency.Entities.DTO.Order;

namespace SalesAgency.Api.Controller.Order;


[Route("api")]
[ApiController]

public class OrderController : ControllerBase
{
  private readonly AppDbContext _db;
  private readonly IMapper _mapper;

  public OrderController(AppDbContext db, IMapper mapper)
  {
    _db = db;
    _mapper = mapper;
  }

  [HttpGet("orders")]
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

}