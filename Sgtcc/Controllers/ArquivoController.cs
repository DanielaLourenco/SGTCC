﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sgtcc.Models;

namespace Sgtcc.Controllers
{
    public class ArquivoController : Controller
    {
        private Model1Container db = new Model1Container();
        Random rnd = new Random();

        // GET: Arquivo
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UploadFile(HttpPostedFileBase UploadedFile)
        {
            int idAluno = (int)HttpContext.Session["userID"];

            var dadosAluno = db.Alunos.Where((Sgtcc.Models.Aluno a) => a.Id == idAluno).First();
            if (dadosAluno.Tccs.Count <= 0)
            {
                return View();
            }
            else if (UploadedFile.ContentLength > 0)
            {
                string fileName = Path.GetFileName(UploadedFile.FileName);
                string folderPath = Path.Combine(Server.MapPath("~/Arquivos"), fileName);

                UploadedFile.SaveAs(folderPath);

                Sgtcc.Models.Arquivo novoArquivo = new Sgtcc.Models.Arquivo();

                novoArquivo.caminho = folderPath;
                novoArquivo.extensao = Path.GetExtension(UploadedFile.FileName).ToString();
                novoArquivo.nome = fileName;
               // novoArquivo.Id = rnd.Next(32);
                novoArquivo.Tcc = dadosAluno.Tccs.FirstOrDefault();

                db.Arquivoes.Add(novoArquivo);
                db.SaveChanges();

            }

            ViewBag.Message = "Arquivo enviado com sucesso";

            return View("Index");

        }
    }
}