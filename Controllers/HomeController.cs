using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Employee.Models;
using Microsoft.Data.SqlClient;



namespace Employee.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    string constr = "Data Source=can-toi;Database=S_SQR_PERSONNEL;Trusted_Connection=True;MultipleActiveResultSets=true;Integrated Security=False;TrustServerCertificate=True;User Id=sa;Password=Pepper!9";

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }


    public IActionResult ManInBlack(string myname,int numtimes)
    {
        ViewData["Message"] = "Hello " + myname;
        ViewData["NumTimes"] = numtimes;

        return View();
    }

    public IActionResult ViewAllEmployee(string myname,int numtimes)
    {
        List<Employee.Models.Employee> emplist = new List<Models.Employee>();
        try
        {
            string query = "select * from dbo.EMPLOYEE where EMPLOYEEID <>' '";
            
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            Models.Employee emp = new Models.Employee();

                            emp.EMPLOYEEID = sdr["EMPLOYEEID"].ToString();
                            emp.FNAME = sdr["FNAME"].ToString();
                            emp.LNAME = sdr["LNAME"].ToString();
                            emp.EMPROLE = sdr["EMPROLE"].ToString();
                            emp.ISMANAGER = sdr["ISMANAGER"].ToString()=="Y";

                            emplist.Add(emp);                            
                        }

                    }
                   con.Close();

                }
            }

        }
        catch (Exception ex)
        {
            string Error = ex.Message;
        }

        return View(emplist);
    }


    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
