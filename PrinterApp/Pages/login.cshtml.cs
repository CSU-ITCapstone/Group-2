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
    // This is the class for the LoginModel
    public class LoginModel : PageModel
    {
        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public string Password { get; set; }

        private readonly IConfiguration _configuration;

        // This is the constructor for the LoginModel
        public LoginModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        // This is the class for the InputModel
        public class InputModel
        {
            [Required]
            public string Username { get; set; }

            [Required]
            public string Password { get; set; }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // Check if the user entered an email and password
            if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password))
            {
                // Display error message
                ModelState.AddModelError("InvalidCredentials", "Email and password are required.");
                return Page();
            }

            // Check if the user exists in the database
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("PrinterAppContext")))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM UserLogins WHERE Email = @Email AND Password = @Password", connection))
                {
                    // Add the parameters for the query
                    command.Parameters.AddWithValue("@Email", Email);
                    command.Parameters.AddWithValue("@Password", Password);

                    // Execute the query and get the count
                    var count = (int)await command.ExecuteScalarAsync();
                    
                    // Check if the user exists
                    if (count > 0)
                    {
                        // Authentication successful, redirect to the Printers/Index page
                        return RedirectToPage("/Printers/Index");
                    }
                    else
                    {
                        // Authentication failed, display error message
                        ModelState.AddModelError("InvalidCredentials", "Invalid email or password.");
                        return Page();
                    }
                }
            }
        }

    }
}
