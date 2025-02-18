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
    public class GestionnairesController : Controller
    {
        private BdAgenceVoyageContext db = new BdAgenceVoyageContext();
        private const int pageSize = 2; // Nombre d'éléments par page

        // GET: Gestionnaires
        public ActionResult Index(string nom, string prenom, int? page)
        {
            ViewBag.Nom = nom!=null?  nom: string.Empty;
            ViewBag.Prenom = prenom!=null?  prenom: string.Empty;
            
            var listes = db.gestionnaires.ToList();
            

            if (!string.IsNullOrEmpty(nom))
            {
                listes = (List<Gestionnaire>)listes.Where(g => g.NomUtilisateur.ToLower().Contains(nom.ToLower()));
            }
            if (!string.IsNullOrEmpty(prenom))
            {
                listes = (List<Gestionnaire>)listes.Where(g => g.PrenomUtilisateur.ToLower().Contains(prenom.ToLower()));
            }


            page = page.HasValue ? page : 1;
            int pageNumber = (int)page;
            return View(listes.OrderBy(g => g.NomUtilisateur).ToPagedList(pageNumber, pageSize));
        }

        // GET: Gestionnaires/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Gestionnaire gestionnaire = db.utilisateurs.Find(id) as Gestionnaire;

            if (gestionnaire == null)
                return HttpNotFound();

            return View(gestionnaire);
        }

        // GET: Gestionnaires/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Gestionnaires/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdUtilisateur,NomUtilisateur,PrenomUtilisateur,EmailUtilisateur,TelUtilisateur,CNIGestionnaire")] Gestionnaire gestionnaire)
        {
            if (ModelState.IsValid)
            {
                db.utilisateurs.Add(gestionnaire);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(gestionnaire);
        }

        // GET: Gestionnaires/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Gestionnaire gestionnaire = db.utilisateurs.Find(id) as Gestionnaire;

            if (gestionnaire == null)
                return HttpNotFound();

            return View(gestionnaire);
        }

        // POST: Gestionnaires/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdUtilisateur,NomUtilisateur,PrenomUtilisateur,EmailUtilisateur,TelUtilisateur,CNIGestionnaire")] Gestionnaire gestionnaire)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gestionnaire).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(gestionnaire);
        }

        // GET: Gestionnaires/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Gestionnaire gestionnaire = db.utilisateurs.Find(id) as Gestionnaire;

            if (gestionnaire == null)
                return HttpNotFound();

            return View(gestionnaire);
        }

        // POST: Gestionnaires/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Gestionnaire gestionnaire = db.utilisateurs.Find(id) as Gestionnaire;
            if (gestionnaire != null)
            {
                db.utilisateurs.Remove(gestionnaire);
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
