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
    public class TccController : Controller
    {
        private Model1Container db = new Model1Container();

        // GET: Tcc
        public ActionResult Index()
        {
            return View(db.Tccs.ToList());
        }

        // GET: Tcc/Details/5
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

        // GET: Tcc/Create
        public ActionResult Create()
        {
            /*var professor = db.Professores.ToList();
            List<String> lista_professor = new List<string>();
            foreach (Professor prof in professor)
            {
                lista_professor.Add(prof.nome.ToString());
            }
            ViewBag.Professores = lista_professor;*/
            //ViewBag.Professores = new SelectList(db.Professores, "Id", "nome");
            return View();
        }

        // POST: Tcc/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,titulo,Orientador")] Tcc tcc)
        {
            if (ModelState.IsValid)
            {
                var professor = db.Professores.Where(x => x.nome == tcc.Orientador).FirstOrDefault();
                tcc.Professor = professor;
                DateTime dataAtual = DateTime.Now;
                tcc.semestre = (dataAtual.Month <= 6 ? 1 : 2).ToString();
                tcc.ano = (dataAtual.Year).ToString();
                int aux = (int)HttpContext.Session["userID"];
                var aluno = db.Alunos.Where(x => x.Id == aux).FirstOrDefault();
                tcc.Aluno = aluno;
                tcc.status = "1";
                db.Tccs.Add(tcc);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tcc);
        }

        // GET: Tcc/Edit/5
        public ActionResult Edit(int? id)
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

        // POST: Tcc/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,titulo,Orientador")] Tcc tcc)
        {
            if (ModelState.IsValid)
            {

                var professor = db.Professores.Where(x => x.nome == tcc.Orientador).FirstOrDefault();
                tcc.Professor = professor;
                db.Entry(tcc).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tcc);
        }

        // GET: Tcc/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: Tcc/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tcc tcc = db.Tccs.Find(id);
            db.Tccs.Remove(tcc);
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
