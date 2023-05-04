using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace PrinterApp.Pages.Printers
{
    public class createModel : PageModel
    {
        public PrinterList printerList = new PrinterList();

        // Add [BindProperty] attribute and make the properties public
        [BindProperty]
        public string errorMessage { get; set; } = "";
        public void OnGet()
        {
        }

        public void OnPost()
        { 
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
                String connectionString = "Server=18.221.187.185;Initial Catalog=Printers;User ID=mgowan;Password=SecurePassword!123;MultipleActiveResultSets=true";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "INSERT INTO dbo.Printers " + "(AssetNo, IPAddress, MAC, HostDNSName, Location, Type, ProductModel, SerialNumber, MFGDate, Notes) VALUES " +
                        "(@asset, @ipaddress, @mac, @hostdnsname, @location, @type, @productmodel, @serialnumber, @mfgdate, @notes);";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
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