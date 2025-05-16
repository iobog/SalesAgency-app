using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SalesAgency.Entities.Data;
using SalesAgency.Entities.DTOs.Client;
using SalesAgency.Services.Clients;

namespace MyApp.Namespace
{
    public class GetProductDTO
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public decimal? Price { get; set; }

        public int? Stock { get; set; }

        public int Quantity { get; set; } = 0;
    }

    public class CreateModel : PageModel
    {
        private readonly AppDbContext _db;
        private readonly IClientService _clientService;

        public List<GetClientDTO> Clients = [];

        [BindProperty]
        public int ClientId { get; set; }

        [BindProperty]
        public string Address { get; set; } = null!;

        [BindProperty]
        public List<GetProductDTO> Products { get; set; } = [];

        public List<string> ErrorMessages { get; set; } = [];


        public CreateModel(AppDbContext db, IClientService clientService)
        {
            _db = db;
            _clientService = clientService;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            Clients = await _clientService.ListClientsAsync();

            Products = await _db.TProducts
                .Select(_ => new GetProductDTO()
                {
                    Id = _.Id,
                    Name = _.Name,
                    Price = _.Price,
                    Stock = _.Stock
                })
                .ToListAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var productsDTO = Products.Where(p => p.Quantity > 0).ToList();
            decimal Total = 0;

            foreach (var productDTO in productsDTO)
            {
                var product = await _db.TProducts.FirstOrDefaultAsync(_ => _.Id == productDTO.Id);
                if (product != null)
                {
                    if (productDTO.Quantity > product.Stock)
                    {
                        ErrorMessages.Add($"Cantitate insuficienta pentru '{product.Name}'");
                    }
                    
                    product.Stock -= productDTO.Quantity;
                    Total += (decimal)product.Price! * productDTO.Quantity;
                }
            }

            if (productsDTO.Count == 0)
            {
                ErrorMessages.Add("Adauga cel putin un produs pe comanda");
            }

            if (ErrorMessages.Count != 0)
            {
                return Page();
            }

            TOrder order = new()
            {
                ClientId = ClientId,
                Adress = Address,
                CreatedAt = DateTime.UtcNow,
                UserId = int.Parse(HttpContext.User.Claims.Where(c => c.Type == "UserId").First().Value),
                TOrderProducts = productsDTO.Select(p => new TOrderProduct()
                {
                    ProductId = p.Id,
                    Quantity = p.Quantity
                })
                .ToList(),
                Total = Total
            };

            await _db.TOrders.AddAsync(order);
            await _db.SaveChangesAsync();

            return RedirectToPage("/Order/Index");
        }
    }
}
