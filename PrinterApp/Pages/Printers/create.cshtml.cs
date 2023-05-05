// This file is used to create a new printer in the database


using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

// This is the namespace for the PrinterApp
namespace PrinterApp.Pages.Printers
{
    // This is the class for the createModel
    public class createModel : PageModel
    {
        public PrinterList printerList = new PrinterList();

        // This is the errorMessage that will be displayed
        // if the user does not fill out all the fields
        [BindProperty]
        public string errorMessage { get; set; } = "";
        public void OnGet()
        {
        }

        // This is the OnPost method that will be called
        // when the user clicks the submit button
        public void OnPost()
        {
            // This is the printerList that will be used to
            // store the information that the user enters
            printerList.assetno = Request.Form["asset"];
            printerList.ipaddress = Request.Form["ipaddress"];
            printerList.mac = Request.Form["mac"];
            printerList.hostdnsname = Request.Form["hostdnsname"];
            printerList.location = Request.Form["location"];
            printerList.type = Request.Form["type"];
            printerList.productmodel = Request.Form["productmodel"];
            printerList.serialnumber = Request.Form["serialnumber"];
            printerList.mfgdate = Request.Form["mfgdate"];
            printerList.notes = Request.Form["notes"];


            // This is the error message that will be displayed
            // if the user does not fill out all the fields

            if (printerList.assetno.Length == 0 || printerList.ipaddress.Length == 0 ||
                printerList.mac.Length == 0 || printerList.hostdnsname.Length == 0 ||
                printerList.location.Length == 0 || printerList.type.Length == 0 ||
                printerList.productmodel.Length == 0 || printerList.serialnumber.Length == 0 ||
                printerList.mfgdate.Length == 0 || printerList.notes.Length == 0)
            {
                errorMessage = "All the fields are required";
                return;
            }

            try
            {
                // This is the connection string that will be used
                // to connect to the database
                String connectionString = "Server=18.221.187.185;Initial Catalog=Printers;User ID=mgowan;Password=SecurePassword!123;MultipleActiveResultSets=true";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // This is the sql statement that will be used
                    // to insert the new printer into the database

                    connection.Open();
                    String sql = "INSERT INTO dbo.Printers " + "(AssetNo, IPAddress, MAC, HostDNSName, Location, Type, ProductModel, SerialNumber, MFGDate, Notes) VALUES " +
                        "(@asset, @ipaddress, @mac, @hostdnsname, @location, @type, @productmodel, @serialnumber, @mfgdate, @notes);";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        // This is where the parameters are added to the sql statement
                        command.Parameters.AddWithValue("@asset", printerList.assetno);
                        command.Parameters.AddWithValue("@ipaddress", printerList.ipaddress);
                        command.Parameters.AddWithValue("@mac", printerList.mac);
                        command.Parameters.AddWithValue("@hostdnsname", printerList.hostdnsname);
                        command.Parameters.AddWithValue("@location", printerList.location);
                        command.Parameters.AddWithValue("@type", printerList.type);
                        command.Parameters.AddWithValue("@productmodel", printerList.productmodel);
                        command.Parameters.AddWithValue("@serialnumber", printerList.serialnumber);
                        command.Parameters.AddWithValue("@mfgdate", printerList.mfgdate);
                        command.Parameters.AddWithValue("@notes", printerList.notes);

                        command.ExecuteNonQuery();
                    }
                }


            }
            // This is the catch block that will catch any exceptions
            // that are thrown
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            printerList.assetno = ""; printerList.ipaddress = ""; printerList.mac = ""; printerList.hostdnsname = ""; printerList.location = ""; printerList.type = ""; printerList.productmodel = ""; printerList.serialnumber = ""; printerList.mfgdate = ""; printerList.notes = "";
            TempData["SuccessMessage"] = "New Printer Added Successfully";

            Response.Redirect("/Printers/Index");
        }
    }
}