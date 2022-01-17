using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Employee.Models;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace Employee.Controllers;

public class TheDB
{
    string constr = "Data Source=can-toi;Database=S_SQR_PERSONNEL;Trusted_Connection=True;MultipleActiveResultSets=true;Integrated Security=False;TrustServerCertificate=True;User Id=sa;Password=Pepper!9";

    public SelectList ReturnManagerDropList()
    {
       
        List<Employee.Models.Employee > empList = new List<Employee.Models.Employee >();
        try
        {
            string query = string.Format("select * from dbo.EMPLOYEE where EMPLOYEEID NOT in (' ') and ISMANAGER in ('Y')");
            
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
                            Employee.Models.Employee emp = new Employee.Models.Employee ();
                            emp.EMPLOYEEID = sdr["EMPLOYEEID"].ToString();

                            empList.Add(emp);
                        }
                    }
                   con.Close();

                }
            }

           


        }
        catch (Exception ex )
        {
            string err = ex.Message;
        }


        return new SelectList(empList,"EMPLOYEEID","EMPLOYEEID");;
    }

   public List<Employee.Models.Employee> returnCrewAllEmployee(string mgr_empid)
    {
        List<Employee.Models.Employee> emplist = new List<Models.Employee>();
        try
        {
            string query = string.Format("select * from dbo.EMPLOYEE a INNER JOIN dbo.CREW b on b.CREWEMP = a.EMPLOYEEID where b.MANAGER IN ('{0}') ",mgr_empid);
            
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

        return emplist;
    }

 
    public List<Employee.Models.Employee> returnAllEmployee()
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

        return emplist;
    }

    public Boolean CreateCrew(string mgr_empid, string empid)
    {
        Boolean success = true;
        
      try
        {
            string query = string.Format("insert into dbo.CREW (MANAGER,CREWEMP) values ('{0}','{1}')",mgr_empid,empid);


            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    con.Open();
                    int sdr = cmd.ExecuteNonQuery();

                    int a = sdr;
                }

                
            }


        }
        catch(Exception ex)
        {
            string err = ex.Message;
        }


        return success;
    }

    public Boolean CreateEmployee(Employee.Models.Employee theemp)
    {
        Boolean success = true;

       try
        {
            string isManager = (theemp.ISMANAGER ?"Y":"N");
            string query = string.Format("insert into dbo.EMPLOYEE (EMPLOYEEID,FNAME,LNAME,ISMANAGER,EMPROLE) values ('{0}','{1}','{2}','{3}','{4}')",
                                            theemp.EMPLOYEEID,theemp.FNAME,theemp.LNAME,isManager,theemp.EMPROLE);

            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    con.Open();
                    int sdr = cmd.ExecuteNonQuery();

                    int a = sdr;
                }

                
            }


        }
        catch(Exception ex)
        {
            string err = ex.Message;
        }


        return success;
    }
    // public List<mgrList> ReturnAllManager()
    // {
    //     List<mgrList> allmgr = new List<mgrList>();

    //    try
    //     {
    //         string query = "select * from dbo.EMPLOYEE where EMPLOYEEID <>' ' and ISMANAGER in ('Y')";
            
    //         using (SqlConnection con = new SqlConnection(constr))
    //         {
    //             using (SqlCommand cmd = new SqlCommand(query))
    //             {
    //                 cmd.Connection = con;
    //                 con.Open();
    //                 using (SqlDataReader sdr = cmd.ExecuteReader())
    //                 {
    //                     while (sdr.Read())
    //                     {
    //                         Models.Employee emp = new Models.Employee();

    //                         emp.EMPLOYEEID = sdr["EMPLOYEEID"].ToString();
    //                         emp.FNAME = sdr["FNAME"].ToString();
    //                         emp.LNAME = sdr["LNAME"].ToString();
    //                         emp.EMPROLE = sdr["EMPROLE"].ToString();
    //                         emp.ISMANAGER = sdr["ISMANAGER"].ToString()=="Y";
    //                         empList.Add(emp);
    //                     }
    //                 }
    //                con.Close();

    //             }
    //         }

    //         mylist.myddList = new SelectList(empList,"EMPLOYEEID","EMPLOYEEID");

    //     }
    //     catch (Exception ex )
    //     {
    //         string err = ex.Message;
    //     }


    //     return allmgr;
    // }

}

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    #region Misc
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
    public IActionResult ManInBlack(string myname,int numtimes)
    {
        ViewData["Message"] = "Hello " + myname;
        ViewData["NumTimes"] = numtimes;

        return View();
    }
    #endregion

#region  ViewTheEMployees

    [HttpGet]
    public IActionResult ViewCrew()
    {
        mgrList themgr = new mgrList();
        try
        {
            TheDB db = new TheDB();
            themgr.myddList = db.ReturnManagerDropList();

        }
        catch(Exception ex)
        {
            string err = ex.Message;
        }

        return View(themgr);
    }

    

//    [HttpGet]
    // public IActionResult VewCrewLoaded(Models.mgrList theemp)
    // {
    //     mgrList themgr = new mgrList();
    //     try
    //     {
    //         TheDB db = new TheDB();
 
    //     }
    //     catch(Exception ex)
    //     {
    //         string err = ex.Message;
    //     }

    //     return View(theemp);
    // }

   [HttpPost]
    public IActionResult ViewCrew(Models.mgrList theemp)
    {
        mgrList themgr = new mgrList();
        try
        {
            TheDB db = new TheDB();

            theemp.empsList = db.returnCrewAllEmployee(theemp.EMPLOYEEID);

        }
        catch(Exception ex)
        {
            string err = ex.Message;
        }

        return View("ViewCrewLoaded",theemp);
    }

    public IActionResult ViewAllEmployee()
    {
        List<Employee.Models.Employee> emplist = new List<Models.Employee>();
        try
        {
            TheDB db = new TheDB();
            emplist = db.returnAllEmployee();


        }
        catch (Exception ex)
        {
            string Error = ex.Message;
        }

        return View(emplist);
    }

#endregion

#region CreateEmps
    [HttpGet]
    public IActionResult Create()
    {
        mgrList themgr = new mgrList();

        try
        {
            TheDB db = new TheDB();

            themgr.myddList = db.ReturnManagerDropList();


        }
        catch (Exception ex)
        {
            string err = ex.Message;
        }

        return View(themgr);
    }

    [HttpPost]
    public IActionResult Create(Models.mgrList theemp)
    {
        try
        {

            TheDB db = new TheDB();


            db.CreateEmployee(theemp.emp);

            if (!string.IsNullOrEmpty(theemp.EMPLOYEEID.Trim()))
            {
                db.CreateCrew(theemp.EMPLOYEEID,theemp.emp.EMPLOYEEID);
            }

            // theemp.myddList = db.ReturnManagerDropList();

        }
        catch(Exception ex)
        {
            string err = ex.Message;
        }

        return View("Index");
    }


  
    

#endregion

#region Crews

    public IActionResult BuildCrew_push(Models.mgrList ml )
    {
        try
        {
            string ep = ml.EMPLOYEEID;
            int a=0;
        }
        catch(Exception ex)
        {
            string err = ex.Message;
        }

        return View();
    }

    public IActionResult BuildCrew()
    {
        List<Employee.Models.Employee> empList = new List<Employee.Models.Employee>();

        var mylist = new mgrList();
        try
        {
            // string query = "select * from dbo.EMPLOYEE where EMPLOYEEID <>' ' and ISMANAGER in ('Y')";
            
            // using (SqlConnection con = new SqlConnection(constr))
            // {
            //     using (SqlCommand cmd = new SqlCommand(query))
            //     {
            //         cmd.Connection = con;
            //         con.Open();
            //         using (SqlDataReader sdr = cmd.ExecuteReader())
            //         {
            //             while (sdr.Read())
            //             {
            //                 Models.Employee emp = new Models.Employee();

            //                 emp.EMPLOYEEID = sdr["EMPLOYEEID"].ToString();
            //                 emp.FNAME = sdr["FNAME"].ToString();
            //                 emp.LNAME = sdr["LNAME"].ToString();
            //                 emp.EMPROLE = sdr["EMPROLE"].ToString();
            //                 emp.ISMANAGER = sdr["ISMANAGER"].ToString()=="Y";
            //                 empList.Add(emp);
            //             }
            //         }
            //        con.Close();

            //     }
            // }

            // mylist.myddList = new SelectList(empList,"EMPLOYEEID","EMPLOYEEID");

        }
        catch (Exception ex )
        {
            string err = ex.Message;
        }

        return View(mylist);
    }
#endregion
}
