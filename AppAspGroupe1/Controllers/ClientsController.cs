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
    public class ClientsController : Controller
    {
        private BdAgenceVoyageContext db = new BdAgenceVoyageContext();
        private const int pageSize = 1; // Nombre d'éléments par page

        // GET: Clients
        public ActionResult Index(string nom, string prenom, int? page)
        {
            ViewBag.Nom = nom != null ? nom : string.Empty;
            ViewBag.Prenom = prenom != null ? prenom : string.Empty;

            var clients = db.utilisateurs.OfType<Client>().AsQueryable();

            if (!string.IsNullOrEmpty(nom))
            {
                clients = clients.Where(c => c.NomUtilisateur.ToLower().Contains(nom.ToLower()));
            }
            if (!string.IsNullOrEmpty(prenom))
            {
                clients = clients.Where(c => c.PrenomUtilisateur.ToLower().Contains(prenom.ToLower()));
            }
          

            int pageNumber = page ?? 1;
            return View(clients.OrderBy(c => c.NomUtilisateur).ToPagedList(pageNumber, pageSize));
        }

        // GET: Clients/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Client client = db.utilisateurs.Find(id) as Client;

            if (client == null)
                return HttpNotFound();

            return View(client);
        }

        // GET: Clients/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Clients/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdUtilisateur,NomUtilisateur,PrenomUtilisateur,EmailUtilisateur,TelUtilisateur,CniClient")] Client client)
        {
            if (ModelState.IsValid)
            {
                db.utilisateurs.Add(client);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(client);
        }

        // GET: Clients/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Client client = db.utilisateurs.Find(id) as Client;

            if (client == null)
                return HttpNotFound();

            return View(client);
        }

        // POST: Clients/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdUtilisateur,NomUtilisateur,PrenomUtilisateur,EmailUtilisateur,TelUtilisateur,CniClient")] Client client)
        {
            if (ModelState.IsValid)
            {
                db.Entry(client).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(client);
        }

        // GET: Clients/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Client client = db.utilisateurs.Find(id) as Client;

            if (client == null)
                return HttpNotFound();

            return View(client);
        }

        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Client client = db.utilisateurs.Find(id) as Client;
            if (client != null)
            {
                db.utilisateurs.Remove(client);
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
