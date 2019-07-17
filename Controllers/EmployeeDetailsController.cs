using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication4.Models;

namespace WebApplication4.Controllers
{
    public class EmployeeDetailsController : Controller
    {
        // GET: EmployeeDetails
        public ActionResult Index()
        {
            List<EmployeeDetail> employees = new List<EmployeeDetail>();
            employees = GetEmployeeDetails();
            return View(employees);
        }
        public List<EmployeeDetail> GetEmployeeDetails()
        {
            using (EmployeeEntities1 employee = new EmployeeEntities1())
            {
                List<EmployeeDetail> employeeDetails = employee.EmployeeDetails.ToList();
                return employeeDetails;

            }
        }

        public ActionResult DisplayEmployeeDetails(EmployeeDetail emp)
        {
            return View("UpdateEmployee", emp);
        }

        [HttpPost]
        public ActionResult UpdateEmployee(EmployeeDetail emp)
        {
           
            using (EmployeeEntities1 employee = new EmployeeEntities1())
            {
                if (ModelState.IsValid && emp!=null)
                {
                    var em = employee.Entry(emp);
                    EmployeeDetail empObj = employee.EmployeeDetails.Where(x => x.EmployeeID == emp.EmployeeID).FirstOrDefault();
                    empObj.EmployeeName = emp.EmployeeName;
                    empObj.EmployeeContact = emp.EmployeeContact;
                    empObj.EmployeeCountry = emp.EmployeeCountry;
                    employee.SaveChanges();
                    ViewBag.Message = "Changes Saved!";
                    
                }
                else
                {
                    ViewBag.Message= "changes not saved!";
                }
                return View();

            } 
        }
               
    }
}