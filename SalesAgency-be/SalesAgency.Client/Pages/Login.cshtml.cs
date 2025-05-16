using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SalesAgency.Client.Pages;
using SalesAgency.Entities.Data;

namespace MyApp.Namespace
{
    public class LoginModel : PageModel
    {
        private readonly AppDbContext _db;

        [BindProperty]
        public string? Username { get; set; }

        [BindProperty]
        public string? Password { get; set; }

        public string? ErrorMessage { get; set; }

        public LoginModel(AppDbContext db)
        {
            _db = db;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync() 
        {
            var user = await _db.TUsers
                .Where(_ => _.Email == Username && _.Password == Password)
                .FirstOrDefaultAsync();
            
            if (user != null)
            {
                List<Claim> claims =
                [
                    new("UserId", user.Id.ToString()),
                    new(ClaimTypes.Name, user.Name!),
                    new(ClaimTypes.Email, user.Email!),
                ];

                ClaimsIdentity claimsIdentity = new(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                ClaimsPrincipal claimsPrincipal = new(claimsIdentity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

                return RedirectToPage("/Dashboard");
            }

            ErrorMessage = "Email sau parola invalide!";
        
            return Page();
        }
    }
}
