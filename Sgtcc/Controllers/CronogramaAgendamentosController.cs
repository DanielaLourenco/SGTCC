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
    public class CronogramaAgendamentosController : Controller
    {
        private Model1Container db = new Model1Container();

        // GET: CronogramaAgendamentoes
        public ActionResult Index()
        {
            return View(db.CronogramaAgendamentoes.ToList());
        }

        // GET: CronogramaAgendamentoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CronogramaAgendamento cronogramaAgendamento = db.CronogramaAgendamentoes.Find(id);
            if (cronogramaAgendamento == null)
            {
                return HttpNotFound();
            }
            return View(cronogramaAgendamento);
        }

        // GET: CronogramaAgendamentoes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CronogramaAgendamentoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,prazoInicial,prazoFinal")] CronogramaAgendamento cronogramaAgendamento)
        {
            if (ModelState.IsValid)
            {
                db.CronogramaAgendamentoes.Add(cronogramaAgendamento);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cronogramaAgendamento);
        }

        // GET: CronogramaAgendamentoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CronogramaAgendamento cronogramaAgendamento = db.CronogramaAgendamentoes.Find(id);
            if (cronogramaAgendamento == null)
            {
                return HttpNotFound();
            }
            return View(cronogramaAgendamento);
        }

        // POST: CronogramaAgendamentoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,prazoInicial,prazoFinal")] CronogramaAgendamento cronogramaAgendamento)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cronogramaAgendamento).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cronogramaAgendamento);
        }

        // GET: CronogramaAgendamentoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CronogramaAgendamento cronogramaAgendamento = db.CronogramaAgendamentoes.Find(id);
            if (cronogramaAgendamento == null)
            {
                return HttpNotFound();
            }
            return View(cronogramaAgendamento);
        }

        // POST: CronogramaAgendamentoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CronogramaAgendamento cronogramaAgendamento = db.CronogramaAgendamentoes.Find(id);
            db.CronogramaAgendamentoes.Remove(cronogramaAgendamento);
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
