using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AppAspGroupe1.Models;
using PagedList;

namespace AppAspGroupe1.Controllers
{
    public class AdminsController : Controller
    {
        private BdAgenceVoyageContext db = new BdAgenceVoyageContext();
        private const int pageSize = 1; // Nombre d'éléments par page

        // GET: Admins
        public ActionResult Index(string nom, string prenom,int? page)
        {
            ViewBag.Nom = nom != null ? nom : string.Empty;
            ViewBag.Prenom = prenom != null ? prenom : string.Empty;

            var admins = db.utilisateurs.OfType<Admin>().AsQueryable();

            if (!string.IsNullOrEmpty(nom))
            {
                admins = admins.Where(a => a.NomUtilisateur.ToLower().Contains(nom.ToLower()));
            }
            if (!string.IsNullOrEmpty(prenom))
            {
                admins = admins.Where(a => a.PrenomUtilisateur.ToLower().Contains(prenom.ToLower()));
            }


            page = page.HasValue ? page : 1;
            int pageNumber = (int)page;
            return View(admins.OrderBy(a => a.NomUtilisateur).ToPagedList(pageNumber, pageSize));
        }

        // GET: Admins/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Admin admin = db.utilisateurs.Find(id) as Admin;

            if (admin == null)
                return HttpNotFound();

            return View(admin);
        }

        // GET: Admins/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admins/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdUtilisateur,NomUtilisateur,PrenomUtilisateur,EmailUtilisateur,TelUtilisateur,MatriculeAdmin")] Admin admin)
        {
            if (ModelState.IsValid)
            {
                db.utilisateurs.Add(admin);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(admin);
        }

        // GET: Admins/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Admin admin = db.utilisateurs.Find(id) as Admin;

            if (admin == null)
                return HttpNotFound();

            return View(admin);
        }

        // POST: Admins/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdUtilisateur,NomUtilisateur,PrenomUtilisateur,EmailUtilisateur,TelUtilisateur,MatriculeAdmin")] Admin admin)
        {
            if (ModelState.IsValid)
            {
                db.Entry(admin).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(admin);
        }

        // GET: Admins/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Admin admin = db.utilisateurs.Find(id) as Admin;

            if (admin == null)
                return HttpNotFound();

            return View(admin);
        }

        // POST: Admins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Admin admin = db.utilisateurs.Find(id) as Admin;
            if (admin != null)
            {
                db.utilisateurs.Remove(admin);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                db.Dispose();

            base.Dispose(disposing);
        }
    }
}
