using System;
using System.Data;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Sgtcc.Models;
using System.Collections.Generic;


namespace Sgtcc.Controllers
{
    public class TccController : Controller
    {
        private Model1Container db = new Model1Container();

        // GET: Tcc
        public ActionResult Index()
        {
            int idUsuario = (int)HttpContext.Session["userID"];
            return View(db.Tccs.Where(x => x.Aluno.Id == idUsuario));
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
            ViewBag.Professores = new SelectList(db.Professores, "Id","nome");
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
                int orientador = Int32.Parse(tcc.Orientador);
                var professor = db.Professores.Where(x => x.Id == orientador).FirstOrDefault();
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
            //ViewBag.Professores = new SelectList(db.Professores, "Id", "nome");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tcc tcc = db.Tccs.Find(id);
            if (tcc == null)
            {
                return HttpNotFound();
            }
            /*var professor = db.Professores.ToList();
            List<string> lista_professor = new List<string>();
            foreach (Professor prof in professor)
            {
                lista_professor.Add(prof.nome.ToString());
            }
            ViewBag.Professores = lista_professor;*/
            return View(tcc);
        }

        // POST: Tcc/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,titulo,Orientador,Aluno, Professor,ano,semestre,status")] Tcc tcc)
        {
            //if (ModelState.IsValid)
            //{
                db.Entry(tcc).State = System.Data.Entity.EntityState.Modified;
                var professor = db.Professores.Where(x => x.nome == tcc.Orientador).FirstOrDefault();
                tcc.Professor = professor;
                int aux = (int)HttpContext.Session["userID"];
                var aluno = db.Alunos.Where(x => x.Id == aux).FirstOrDefault();
                tcc.Aluno = aluno;
                db.SaveChanges();
                return RedirectToAction("Index");
            //}
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
