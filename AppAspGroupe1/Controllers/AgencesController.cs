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
    public class AgencesController : Controller
    {
        private BdAgenceVoyageContext db = new BdAgenceVoyageContext();
        const int pageSize = 1;

        // GET: Agences
        public ActionResult Index(string Adresse, string ninea, string rccn, int? page)
        {
            ViewBag.Adresse = Adresse != null ? Adresse : string.Empty;
            ViewBag.ninea = ninea != null ? ninea : string.Empty;
            ViewBag.rccn = rccn != null ? rccn : string.Empty;
            //EAGER LOADING EN RECUPERENAT AGENCE ET SES GESTIONNAIRES 
            var agences = db.Agences.Include(a => a.Gestionnaire);
            var liste = db.Agences.ToList();
            if (!string.IsNullOrEmpty(Adresse))
            {
                liste = liste.Where(a => a.AddresseAgence.ToLower().Contains(Adresse.ToLower())).ToList();
            }
            if (!string.IsNullOrEmpty(ninea))
            {
                liste = liste.Where(a => a.NineaGestionnaire.ToLower().Contains(ninea.ToLower())).ToList();
            }
            if (!string.IsNullOrEmpty(rccn))
            {
                liste = liste.Where(a => a.RcmGestionnaire.ToLower().Contains(rccn.ToLower())).ToList();
            }
            page = page.HasValue ? page : 1;
            int pageNumber = (int)page;

            return View(liste.ToPagedList(pageNumber, pageSize));
        }

        // GET: Agences/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Agence agence = db.Agences.Find(id);
            if (agence == null)
            {
                return HttpNotFound();
            }
            return View(agence);
        }

        // GET: Agences/Create
        public ActionResult Create()
        {
            ViewBag.IdGestionnaire = new SelectList(db.utilisateurs, "IdUtilisateur", "NomUtilisateur");
            return View();
        }

        // POST: Agences/Create
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdAgence,AddresseAgence,Lattitude,Longitude,NineaGestionnaire,RcmGestionnaire,IdGestionnaire")] Agence agence)
        {
            if (ModelState.IsValid)
            {
                db.Agences.Add(agence);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdGestionnaire = new SelectList(db.utilisateurs, "IdUtilisateur", "NomUtilisateur", agence.IdGestionnaire);
            return View(agence);
        }

        // GET: Agences/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Agence agence = db.Agences.Find(id);
            if (agence == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdGestionnaire = new SelectList(db.utilisateurs, "IdUtilisateur", "NomUtilisateur", agence.IdGestionnaire);
            return View(agence);
        }

        // POST: Agences/Edit/5
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdAgence,AddresseAgence,Lattitude,Longitude,NineaGestionnaire,RcmGestionnaire,IdGestionnaire")] Agence agence)
        {
            if (ModelState.IsValid)
            {
                db.Entry(agence).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdGestionnaire = new SelectList(db.utilisateurs, "IdUtilisateur", "NomUtilisateur", agence.IdGestionnaire);
            return View(agence);
        }

        // GET: Agences/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Agence agence = db.Agences.Find(id);
            if (agence == null)
            {
                return HttpNotFound();
            }
            return View(agence);
        }

        // POST: Agences/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Agence agence = db.Agences.Find(id);
            db.Agences.Remove(agence);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
