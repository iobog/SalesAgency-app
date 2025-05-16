
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesAgency.Entities.Data;
using SalesAgency.Entities.DTOs.Product;
namespace SalesAgency.Api.Controller.Product;

[Route("api")]
[ApiController]
public class ProductController: ControllerBase
{
  private readonly AppDbContext _db;
  private readonly IMapper _mapper;

  public ProductController(AppDbContext appDbContext, IMapper mapper)
  {
    _db = appDbContext;
    _mapper = mapper;
  }

  [Route("products")]
  [HttpGet]
  public async Task<ActionResult<IEnumerable<GetProductDTO>>> GetAllProducts()
  { 
    return Ok(_mapper.Map<IEnumerable<GetProductDTO>>(await _db.TProducts.ToListAsync()));
  }

  
  [HttpGet("{id}",Name = "GetProductById")]
  public async Task<ActionResult<GetProductDTO>>GetProductById(int id)
  {
    try
    {
      var client = await _db.TProducts
        .Where(_ => _.Id == id)
        .FirstOrDefaultAsync();
      
      if(client == null)
      {
        return NotFound();
      }
      return Ok(_mapper.Map<GetProductDTO>(client));
    }
    catch(Exception e)
    {
      return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
    }
  }

  [HttpPost]
  public async Task<ActionResult<GetProductDTO>>CreateProduct(CreateUpdateProductDTO createUpdateProductDTO)
  {
    try
    {
      var productModel = _mapper.Map<TProduct>(createUpdateProductDTO);
      await _db.AddAsync(productModel);
      await _db.SaveChangesAsync();

      var getProductDTO = _mapper.Map<GetProductDTO>(productModel);
      return CreatedAtRoute(nameof(GetProductById),new{Id = getProductDTO.Id},getProductDTO);
    }
    catch(Exception e)
    {
     return StatusCode(StatusCodes.Status500InternalServerError, e.Message); 
    }
  }

  [HttpPut("{id}")]
  public async Task<ActionResult> UpdateProduct(int id, CreateUpdateProductDTO createUpdateProductDTO)
  {
    try
    {
      var produs = await _db.TProducts
        .Where(_ => _.Id == id)
        .FirstOrDefaultAsync();

      if(produs == null) 
      {
        return NotFound();
      }

      _mapper.Map(createUpdateProductDTO,produs);
      _db.Update(produs);
      await _db.SaveChangesAsync();
      return NoContent();
    }
    catch(Exception e)
    {
      return StatusCode(StatusCodes.Status500InternalServerError,e.Message);
    }

  }
  //PATCH api/commands/{id}
  // [HttpPatch("{id}")]
  // public async Task<ActionResult> PartialTaskUpdateAsync(int id,JsonPatchDocument<CreateUpdateTaskDTO> patchDoc)
  // {
  //   try
  //   {
  //     var taskModelFromDb = await _db.TTasks
  //       .Where(_ => _.Id == id)
  //       .FirstOrDefaultAsync();

  //     if(taskModelFromDb == null) 
  //     {
  //       return NotFound();
  //     }

  //     var taskToPatch = _mapper.Map<CreateUpdateTaskDTO>(taskModelFromDb);
  //     patchDoc.ApplyTo(taskToPatch,ModelState);

  //     if(!TryValidateModel(taskToPatch))
  //     {
  //       return ValidationProblem(ModelState);
  //     }

  //     _mapper.Map(taskToPatch,taskModelFromDb);
  //     _db.Update(taskModelFromDb);
  //     await _db.SaveChangesAsync();

  //     return NoContent();
  //   }
  //   catch(Exception e)
  //   {
  //     return StatusCode(StatusCodes.Status500InternalServerError,e.Message);
  //   }
  // }


  

  [HttpDelete("{id}")]
  public async Task<ActionResult> DeleteProduct(int id)
  {
    var produs = await _db.TProducts
      .Where(_ => _.Id == id)
      .FirstOrDefaultAsync();

    if(produs == null) 
    {
      return NotFound();
    }
    _db.Remove(produs);
    await _db.SaveChangesAsync();

    return NoContent();

  }

}