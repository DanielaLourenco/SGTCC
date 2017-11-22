using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Sgtcc.Models;
using PagedList;

namespace Sgtcc.Controllers
{
    public class AgendamentoController : Controller
    {
        private Model1Container db = new Model1Container();

        // GET: Agendamento
        public ActionResult Index(/*string sortOrder, int? page*/)
        {
            /*ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            ViewBag.TitleSortParm = sortOrder == "Title" ? "title_desc" : "Title";
            ViewBag.PlaceSortParm = sortOrder == "Place" ? "place_desc" : "Place";

            var agendamento = from s in db.Tccs2 select s;

            switch (sortOrder)
            {
                case "name_desc":
                    agendamento = agendamento.OrderByDescending(s => s.Aluno);
                    break;
                case "Date":
                    agendamento = agendamento.OrderBy(s => s.data);
                    break;
                case "date_desc":
                    agendamento = agendamento.OrderByDescending(s => s.data);
                    break;
                case "Title":
                    agendamento = agendamento.OrderBy(s => s.titulo);
                    break;
                case "title_desc":
                    agendamento = agendamento.OrderByDescending(s => s.titulo);
                    break;
                case "Place":
                    agendamento = agendamento.OrderBy(s => s.local);
                    break;
                case "Place_desc":
                    agendamento = agendamento.OrderByDescending(s => s.local);
                    break;
                default:  // Name ascending 
                    agendamento = agendamento.OrderBy(s => s.Aluno);
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(db.Tccs2.ToPagedList(pageNumber, pageSize));*/
            return View(db.Tccs2.ToList());
        }

        // GET: Agendamento/Details/5
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

        // GET: Agendamento/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        // POST: Agendamento/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Id,titulo,semestre,ano,status,data,local")] Tcc2 tcc2)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Tccs.Add(tcc2);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(tcc2);
        //}

        // GET: Agendamento/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Tcc2 tcc2 = db.Tccs2.Find(id);
        //    if (tcc2 == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(tcc2);
        //}

        // POST: Agendamento/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Id,titulo,semestre,ano,status,data,local")] Tcc2 tcc2)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(tcc2).State = System.Data.Entity.EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(tcc2);
        //}

        // GET: Agendamento/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Tcc2 tcc2 = db.Tccs2.Find(id);
        //    if (tcc2 == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(tcc2);
        //}

        // POST: Agendamento/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Tcc2 tcc2 = db.Tccs2.Find(id);
        //    db.Tccs.Remove(tcc2);
        //   db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

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
