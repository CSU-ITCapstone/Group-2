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
        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public string Password { get; set; }

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
            if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password))
            {
                ModelState.AddModelError("InvalidCredentials", "Email and password are required.");
                return Page();
            }

            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("PrinterAppContext")))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM UserLogins WHERE Email = @Email AND Password = @Password", connection))
                {
                    command.Parameters.AddWithValue("@Email", Email);
                    command.Parameters.AddWithValue("@Password", Password);

                    var count = (int)await command.ExecuteScalarAsync();

                    if (count > 0)
                    {
                        // Authentication successful, redirect to the Printers/Index page
                        return RedirectToPage("/Printers/Index");
                    }
                    else
                    {
                        ModelState.AddModelError("InvalidCredentials", "Invalid email or password.");
                        return Page();
                    }
                }
            }
        }

    }
}
