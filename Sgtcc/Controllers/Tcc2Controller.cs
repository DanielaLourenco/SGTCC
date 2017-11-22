using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Sgtcc.Models;

namespace Sgtcc.Controllers
{
    public class Tcc2Controller : Controller
    {
        private Model1Container db = new Model1Container();

        // GET: Tcc2
        public ActionResult Index()
        {
            return View(db.Tccs2.ToList());
        }

        // GET: Tcc2/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tcc2 tcc2 = db.Tccs2.Find(id);
            if (tcc2 == null)
            {
                return HttpNotFound();
            }
            return View(tcc2);
        }

        // GET: Tcc2/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tcc2/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,titulo,semestre,ano,status,data,local")] Tcc2 tcc2)
        {
            if (ModelState.IsValid)
            {
                db.Tccs2.Add(tcc2);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tcc2);
        }

        // GET: Tcc2/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tcc2 tcc2 = db.Tccs2.Find(id);
            if (tcc2 == null)
            {
                return HttpNotFound();
            }
            return View(tcc2);
        }

        // POST: Tcc2/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,titulo,semestre,ano,status,data,local")] Tcc2 tcc2)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tcc2).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tcc2);
        }

        // GET: Tcc2/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tcc2 tcc2 = db.Tccs2.Find(id);
            if (tcc2 == null)
            {
                return HttpNotFound();
            }
            return View(tcc2);
        }

        // POST: Tcc2/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tcc2 tcc2 = db.Tccs2.Find(id);
            db.Tccs.Remove(tcc2);
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
