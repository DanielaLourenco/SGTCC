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
    public class CronogramaArquivosController : Controller
    {
        private Model1Container db = new Model1Container();

        // GET: CronogramaArquivos
        public ActionResult Index()
        {
            return View(db.CronogramaArquivoes.ToList());
        }

        // GET: CronogramaArquivos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CronogramaArquivo cronogramaArquivo = db.CronogramaArquivoes.Find(id);
            if (cronogramaArquivo == null)
            {
                return HttpNotFound();
            }
            return View(cronogramaArquivo);
        }

        // GET: CronogramaArquivos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CronogramaArquivos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,prazoInicial,prazoFinal")] CronogramaArquivo cronogramaArquivo)
        {
            if (ModelState.IsValid)
            {
                db.CronogramaArquivoes.Add(cronogramaArquivo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cronogramaArquivo);
        }

        // GET: CronogramaArquivos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CronogramaArquivo cronogramaArquivo = db.CronogramaArquivoes.Find(id);
            if (cronogramaArquivo == null)
            {
                return HttpNotFound();
            }
            return View(cronogramaArquivo);
        }

        // POST: CronogramaArquivos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,prazoInicial,prazoFinal")] CronogramaArquivo cronogramaArquivo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cronogramaArquivo).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cronogramaArquivo);
        }

        // GET: CronogramaArquivos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CronogramaArquivo cronogramaArquivo = db.CronogramaArquivoes.Find(id);
            if (cronogramaArquivo == null)
            {
                return HttpNotFound();
            }
            return View(cronogramaArquivo);
        }

        // POST: CronogramaArquivos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CronogramaArquivo cronogramaArquivo = db.CronogramaArquivoes.Find(id);
            db.CronogramaArquivoes.Remove(cronogramaArquivo);
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
