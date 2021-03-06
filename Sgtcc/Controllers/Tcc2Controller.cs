﻿using System;
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
            int idUsuario = (int)HttpContext.Session["userID"];
            var tcc = db.Tccs2.Where(x => x.Aluno.Id == idUsuario).FirstOrDefault();
            if (tcc == null)
            {
                ViewBag.Existe = 0;
            }
            else
            {
                ViewBag.Existe = 1;
            }
            return View(db.Tccs2.Where(x => x.Aluno.Id == idUsuario));
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
            switch (tcc2.status)
            {
                case "5":
                    tcc2.situação = "Cadastrado";
                    break;
                case "6":
                    tcc2.situação = "Aprovado";
                    break;
                case "7":
                    tcc2.situação = "Reprovado";
                    break;
                case "8":
                    tcc2.situação = "Cancelado";
                    break;
            }
            return View(tcc2);
        }

        // GET: Tcc2/Create
        public ActionResult Create()
        {
            int idUsuario = (int)HttpContext.Session["userID"];
            var tcc = db.Tccs2.Where(x => x.Aluno.Id == idUsuario).FirstOrDefault();
            if (tcc != null)
            {
                return RedirectToAction("Index");
            }
            ViewBag.Professores = new SelectList(db.Professores, "Id", "nome");
            return View();
        }

        // POST: Tcc2/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Sgtcc.Models.Tcc2 tcc2)
        {
            if (ModelState.IsValid)
            {
                int orientador = Int32.Parse(tcc2.Orientador);
                var professor = db.Professores.Where(x => x.Id == orientador).FirstOrDefault();
                tcc2.Professor = professor;
                DateTime dataAtual = DateTime.Now;
                tcc2.semestre = (dataAtual.Month <= 6 ? 1 : 2).ToString();
                tcc2.ano = (dataAtual.Year).ToString();
                int aux = (int)HttpContext.Session["userID"];
                var aluno = db.Alunos.Where(x => x.Id == aux).FirstOrDefault();
                tcc2.Aluno = aluno;
                tcc2.status = "5";
                tcc2.situação = "Cadastrado";
                tcc2.data = "";
                tcc2.local = "";
                //Banca default - sempre vazia
                tcc2.Banca = db.Bancas.Where(x => x.Id == 3).FirstOrDefault();
                db.Tccs2.Add(tcc2);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tcc2);
        }

        public ActionResult Agendamento(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tcc2 tcc2 = db.Tccs2.Where(x => x.Aluno.Id == id).FirstOrDefault();
           
            if (tcc2 == null)
            {
                return HttpNotFound();
            }           
            return View(tcc2);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Agendamento(Sgtcc.Models.Tcc2 tcc2)
        {
            if (tcc2.data!=null && tcc2.local!= null)
            {
                Tcc2 tcc = db.Tccs2.Where(x => x.Aluno.Id == tcc2.Id).FirstOrDefault();
                if (tcc != null) {
                    tcc.data = tcc2.data;
                    tcc.local = tcc2.local;
                    //Banca default - sempre vazia
                    tcc.Banca = db.Bancas.Where(x => x.Id == 3).FirstOrDefault();

                    db.SaveChanges();
                }
                
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
            tcc2.Orientador = tcc2.Professor.nome;
            if (tcc2 == null)
            {
                return HttpNotFound();
            }
            ViewBag.Professores = new SelectList(db.Professores, "Id", "nome");
            return View(tcc2);
        }

        // POST: Tcc2/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Sgtcc.Models.Tcc2 tcc2)
        {
            if (ModelState.IsValid)
            {
                var tcc = db.Tccs2.Where(x => x.Id == tcc2.Id).FirstOrDefault();
              
                int orientador = Int32.Parse(tcc2.Orientador);
                var professor = db.Professores.Where(x => x.Id == orientador).FirstOrDefault();
                tcc.Professor = professor;
                tcc.titulo = tcc2.titulo;
                                
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
