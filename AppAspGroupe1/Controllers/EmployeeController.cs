using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AppAspGroupe1.Models;
using Microsoft.Owin.BuilderProperties;
using PagedList; 

namespace AppAspGroupe1.Controllers
{
    public class EmployeeController : Controller
    {
        private BdAgenceVoyageContext db = new BdAgenceVoyageContext();
        private const int PageSize = 2; 

        // GET: Employee
        public ActionResult Index(string Name, string State, string Country, int? page)
        {
            ViewBag.Name = Name !=null ? Name :string.Empty;
            ViewBag.State = State != null ? State : string.Empty;
            ViewBag.Country = Country != null ? Country : string.Empty;
           

            var employees = db.employees.AsQueryable();

            // 🔍 Filtrage par Nom
            if (!string.IsNullOrEmpty(Name))
            {
                employees = employees.Where(e => e.Name.Contains(Name));
            }

            // 🔍 Filtrage par State
            if (!string.IsNullOrEmpty(State))
            {
                employees = employees.Where(e => e.State.Contains(State));
            }

            // 🔍 Filtrage par Country
            if (!string.IsNullOrEmpty(Country))
            {
                employees = employees.Where(e => e.Country.Contains(Country));
            }

            
            page = page.HasValue ? page : 1;
            int pageNumber = (int)page;
            return View(employees.OrderBy(e => e.Name).ToPagedList(pageNumber, PageSize));
        }

        
        public JsonResult List()
        {
            return Json(db.employees.ToList(), JsonRequestBehavior.AllowGet);
        }

        
        public JsonResult Add(Employee emp)
        {
            db.employees.Add(emp);
            db.SaveChanges();
            return Json(1, JsonRequestBehavior.AllowGet);
        }

        // 🔍 Obtenir un employé par ID
        public JsonResult GetbyID(int ID)
        {
            var Employee = db.employees.Find(ID);
            return Json(Employee, JsonRequestBehavior.AllowGet);
        }

        
        public JsonResult Update(Employee emp)
        {
            Employee e = db.employees.Find(emp.EmployeeID);
            if (e != null)
            {
                e.Age = emp.Age;
                e.Country = emp.Country;
                e.State = emp.State;
                e.Name = emp.Name;
                db.SaveChanges();
            }
            return Json(1, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Delete(int ID)
        {
            Employee e = db.employees.Find(ID);
            if (e != null)
            {
                db.employees.Remove(e);
                db.SaveChanges();
            }
            return Json(1, JsonRequestBehavior.AllowGet);
        }

        
        public JsonResult GetStates()
        {
            var states = db.employees
                           .Where(e => !string.IsNullOrEmpty(e.State))
                           .Select(e => e.State.Trim())
                           .Distinct()
                           .OrderBy(s => s)
                           .ToList();
            return Json(states, JsonRequestBehavior.AllowGet);
        }

        
        public JsonResult GetCountries()
        {
            var countries = db.employees
                              .Where(e => !string.IsNullOrEmpty(e.Country))
                              .Select(e => e.Country.Trim())
                              .Distinct()
                              .OrderBy(c => c)
                              .ToList();
            return Json(countries, JsonRequestBehavior.AllowGet);
        }
    }
}
