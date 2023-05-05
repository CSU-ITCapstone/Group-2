// Import necessary libraries and namespaces
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

// Define the namespace and the IndexModel class within the PrinterApp.Pages.Printers namespace
namespace PrinterApp.Pages.Printers
{
    // The IndexModel class inherits from the PageModel class, which is provided by the Razor Pages framework
    public class IndexModel : PageModel
    {
        // Declare a private ILogger instance to enable logging within the IndexModel class
        private readonly ILogger<IndexModel> _logger;

        // Constructor for the IndexModel class that accepts an ILogger instance as a parameter
        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        // Define a list of printers to store the results of the SQL query
        public List<PrinterList> listPrinters = new List<PrinterList>();

        // OnGet() is the entry point for the Razor Page
        public void OnGet()
        {
            try
            {
                // Define the connection string to the database
                String connectionString = "Server=18.221.187.185;Initial Catalog=Printers;User ID=mgowan;Password=SecurePassword!123;MultipleActiveResultSets=true";

                // Create a SQL connection and open it
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Define the SQL query to select all printers from the database
                    String sql = "SELECT * FROM dbo.Printers";

                    // Create a SQL command and execute the query
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            // Loop through the results of the query and create a PrinterList object for each printer
                            while (reader.Read())
                            {
                                PrinterList printerList = new PrinterList();

                                printerList.assetno = reader.GetString(0);
                                printerList.ipaddress = reader.GetString(1);
                                printerList.mac = reader.GetString(2);
                                printerList.hostdnsname = reader.GetString(3);
                                printerList.location = reader.GetString(4);
                                printerList.type = reader.GetString(5);
                                printerList.productmodel = reader.GetString(6);
                                printerList.serialnumber = reader.GetString(7);
                                printerList.mfgdate = reader.GetString(8);
                                printerList.notes = reader.GetString(9);
                                printerList.id = "" + reader.GetInt32(10);

                                // Add the PrinterList object to the list of printers
                                listPrinters.Add(printerList);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during the database connection
                Console.WriteLine("Exception: " + ex.ToString());
            }
        }
    }

    // Define the PrinterList class to store information about a printer
    public class PrinterList
    {
        public String id;
        public String assetno;
        public String ipaddress;
        public String mac;
        public String hostdnsname;
        public String location;
        public String type;
        public String productmodel;
        public String serialnumber;
        public String mfgdate;
        public String notes;
    }
}
