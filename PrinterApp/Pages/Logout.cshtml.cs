using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PrinterApp.Pages
{
    public class LogoutModel : PageModel
    {
        public async Task OnGetAsync()
        {
            await HttpContext.SignOutAsync();
            Response.Redirect("/login");
        }

        public async Task OnPostAsync()
        {
            await OnGetAsync();
        }
    }
}
