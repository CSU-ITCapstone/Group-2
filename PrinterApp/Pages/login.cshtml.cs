using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

namespace PrinterApp.Pages
{
    public class LoginModel : PageModel
    {
        private readonly IConfiguration _configuration;

        public LoginModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            public string Username { get; set; }

            [Required]
            public string Password { get; set; }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("PrinterAppContext")))
            {
                await connection.OpenAsync();
                using (SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM [Printers].[dbo].[UserLogins] WHERE Email = @Email AND Password = @Password", connection))
                {
                    command.Parameters.AddWithValue("@Email", Input.Username);
                    command.Parameters.AddWithValue("@Password", Input.Password);
                    var count = (int)await command.ExecuteScalarAsync();

                    if (count > 0)
                    {
                        // Login successful, redirect to the Index page of the Printers group.
                        return RedirectToPage("/Printers/index");
                    }
                    else
                    {
                        // Login failed.
                        ModelState.AddModelError("", "Invalid username or password.");
                        return Page();
                    }
                }
            }
        }
    }
}
