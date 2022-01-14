    using System;  
    using System.ComponentModel.DataAnnotations;  
    using Microsoft.AspNetCore.Mvc.Rendering;

    namespace Employee.Models  
    {  
        public class mgrList
        {
            public string EMPLOYEEID {get; set;}
            public SelectList myddList {get; set;}
        }

        public class Employee
        {
            [Key]
            public string EMPLOYEEID {get; set;}
            public string FNAME {get; set;}
            public string LNAME {get; set;}
            public string EMPROLE {get; set;}
            public Boolean ISMANAGER {get;set;}
            
            public Employee()
            {
                // EMPROLE = new List<EMPROLES>();
                EMPLOYEEID = string.Empty;
                FNAME = string.Empty;
                LNAME = string.Empty;
                ISMANAGER = false;
            }
            
        }

        public class EMPROLES
        {
            [Key]
            public string CODE {get; set;}
            public string DESCRIPTION {get; set; }

            public EMPROLES()
            {
                CODE = string.Empty;
                DESCRIPTION = string.Empty;
            }
        }
    }  
