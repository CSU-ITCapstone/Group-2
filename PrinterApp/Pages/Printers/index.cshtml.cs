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

namespace PrinterApp.Pages.Printers
{
    public class IndexModel : PageModel
    {
        //private readonly PrinterContext _context;
        //private readonly IConfiguration Configuration;

        //public IndexModel(PrinterContext context, IConfiguration configuration)
        //{
        //    _context = context;
        //    Configuration = configuration;
        //}

        //public string NameSort { get; set; }
        //public string DateSort { get; set; }
        //public string CurrentFilter { get; set; }
        //public string CurrentSort { get; set; }

        //public PaginatedList<PrinterList> Printers { get; set; }

        //public async Task OnGetAsync(string sortOrder,
        //    string currentFilter, string searchString, int? pageIndex)
        //{
        //    CurrentSort = sortOrder;
        //    NameSort = String.IsNullOrEmpty(sortOrder) ? "id_desc" : "";
        //    DateSort = sortOrder == "Location" ? "loc_desc" : "Location";
        //    if (searchString != null)
        //    {
        //        pageIndex = 1;
        //    }
        //    else
        //    {
        //        searchString = currentFilter;
        //    }
        //}

        //public IList<PrinterList> printerLists { get; set; }

        //public async Task OnGetAsync(string sortOrder, string searchString)
        //{
        //    // using System;
        //    NameSort = String.IsNullOrEmpty(sortOrder) ? "id_desc" : "";
        //    DateSort = sortOrder == "Location" ? "loc_desc" : "Location";

        //    IQueryable<PrinterList> printerLists = from s in _context.Printers
        //                                     select s;
        //    if (!String.IsNullOrEmpty(searchString))
        //    {
        //        printerLists = printerLists.Where(s => s.id.Contains(searchString)
        //                               || s.hostdnsname.Contains(searchString));
        //    }
        //    switch (sortOrder)
        //    {
        //        case "id_desc":
        //            printerLists = printerLists.OrderByDescending(s => s.id);
        //            break;
        //        case "Location":
        //            printerLists = printerLists.OrderBy(s => s.location);
        //            break;
        //        case "loc_desc":
        //            printerLists = printerLists.OrderByDescending(s => s.location);
        //            break;
        //        default:
        //            printerLists = printerLists.OrderBy(s => s.id);
        //            break;
        //    }
        //    var pageSize = Configuration.GetValue("PageSize", 4);
        //    printerLists = await PaginatedList<PrinterList>.CreateAsync(
        //        printerLists.AsNoTracking(), pageIndex ?? 1, pageSize);

        //    Printers = await printerLists.AsNoTracking().ToListAsync();
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }
        public List<PrinterList> listPrinters = new List<PrinterList>();
        public void OnGet()
        {
            try
            {
                String connectionString = "Server=18.221.187.185;Initial Catalog=Printers;User ID=mgowan;Password=SecurePassword!123;MultipleActiveResultSets=true";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM dbo.Printers";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
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
                                

                                listPrinters.Add(printerList);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }

        }
    }

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