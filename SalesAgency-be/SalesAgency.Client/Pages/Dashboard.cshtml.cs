using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MyApp.Namespace
{
    public class DashboardModel : PageModel
    {
        public string Email => HttpContext.User.Claims.Where(_ => _.Type == ClaimTypes.Email).FirstOrDefault().Value;
        
        public void OnGet()
        {
            
        }
    }
}
