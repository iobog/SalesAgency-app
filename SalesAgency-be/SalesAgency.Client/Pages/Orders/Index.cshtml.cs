using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SalesAgency.Entities.Data;

namespace MyApp.Namespace
{
    public class GetOrderListItemDTO
    {
        public int Id { get; set; }

        public string? Adress { get; set; }

        public decimal? Total { get; set; }

        public DateTime? CreatedAt { get; set; }

        public string Client { get; set; } = null!;

        public int CountProducts { get; set; }

        public string User { get; set; } = null!;
    }

    public class IndexModel : PageModel
    {
        private readonly AppDbContext _db;

        public List<GetOrderListItemDTO> Orders { get; set; } = [];

        public IndexModel(AppDbContext db)
        {
            _db = db;            
        }

        public async Task<IActionResult> OnGet()
        {
            Orders = await _db.TOrders
                .Select(_ => new GetOrderListItemDTO()
                {
                    Id = _.Id,
                    Adress = _.Adress,
                    Total = _.Total,
                    CreatedAt = _.CreatedAt,
                    Client = _.Client.Name,
                    CountProducts = _.TOrderProducts.Count(),
                    User = _.User.Email
                })
                .ToListAsync();

            return Page();
        }
    }
}
