using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace PrinterApp.Pages.Printers
{
    public class EditModel : PageModel
    {
        public PrinterList printerList = new PrinterList();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
            String id = Request.Query["ID"];

            try
            {
                String connectionString = "Server=18.221.187.185;Initial Catalog=Printers;User ID=mgowan;Password=SecurePassword!123;MultipleActiveResultSets=true";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM dbo.Printers WHERE ID=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
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
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                errorMessage = ex.Message;
            }
        }


        public void OnPost()
        {
            printerList.id = Request.Form["id"];
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

            if (printerList.id.Length == 0 || printerList.assetno.Length == 0 || printerList.ipaddress.Length == 0 ||
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
                    String sql = "UPDATE dbo.Printers " + "SET AssetNo=@asset, IPAddress=@ipaddress, MAC=@mac, HostDNSName=@hostdnsname, Location=@location, Type=@type, ProductModel=@productmodel, SerialNumber=@serialnumber, MFGDate=@mfgdate, Notes=@notes " +
                        "WHERE ID=@id";

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
                        command.Parameters.AddWithValue("@id", printerList.id);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            Response.Redirect("/Printers/Index");
        }
    }
}