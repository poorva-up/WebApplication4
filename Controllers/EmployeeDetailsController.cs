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

        public ActionResult UpdateEmployeeDetails(EmployeeDetail emp)
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

        public ActionResult AddEmployeeDetails()
        {
            EmployeeDetail emp = new EmployeeDetail();
            return View("AddNewEmployee", emp);
        }

        public ActionResult AddNewEmployee(EmployeeDetail emp)
        {

            using (EmployeeEntities1 employee = new EmployeeEntities1())
            {
                if (ModelState.IsValid && emp != null)
                {
                    try
                    {
                        var em = employee.Entry(emp);
                        EmployeeDetail empObj = new EmployeeDetail();
                        var largestEmpId = Convert.ToInt32((from m in employee.EmployeeDetails
                                                            orderby m.EmployeeID descending
                                                            select m.EmployeeID).FirstOrDefault()) + 1;
                        empObj.EmployeeID = largestEmpId.ToString();
                        empObj.EmployeeName = emp.EmployeeName;
                        empObj.EmployeeContact = emp.EmployeeContact;
                        empObj.EmployeeCountry = emp.EmployeeCountry;
                        employee.EmployeeDetails.Add(empObj);
                        employee.SaveChanges();
                        ViewBag.Message = "New Employee Added!";
                    }
                    catch(Exception ex)
                    {
                        string s= ex.StackTrace;
                    }

                }
                else
                {
                    ViewBag.Message = "Employee not saved!";
                }
                return View();

            }
        }

        public ActionResult DeleteEmployeeDetails(EmployeeDetail employee)
        {
            return View("DeleteEmployee",employee);

        }
        [HttpPost]
        public ActionResult DeleteEmployee(EmployeeDetail emp)
        {

            using (EmployeeEntities1 employee = new EmployeeEntities1())
            {
                if (ModelState.IsValid && emp != null)
                {
                    EmployeeDetail empObj = new EmployeeDetail();
                    empObj = employee.EmployeeDetails.Find(emp.EmployeeID);
                    employee.EmployeeDetails.Remove(empObj);
                    employee.SaveChanges();
                    ViewBag.Message = "Employee Details Deleted";

                }
                else
                {
                    ViewBag.Message = "Details Cannot be deleted";
                }
            }
            
            return View();
        }
    }
}