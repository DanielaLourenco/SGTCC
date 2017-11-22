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
    public class BuscaTccsController : Controller
    {
        private Model1Container db = new Model1Container();

        // GET: Tccs
        public ActionResult Index()
        {
            return View(db.Tccs.ToList());
        }

        // GET: Tccs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tcc tcc = db.Tccs.Find(id);
            if (tcc == null)
            {
                return HttpNotFound();
            }
            return View(tcc);
        }

        // GET: Tccs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tccs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,titulo,semestre,ano,status")] Tcc tcc)
        {
            if (ModelState.IsValid)
            {
                db.Tccs.Add(tcc);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tcc);
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
