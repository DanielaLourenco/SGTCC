using System;
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

            var dadosAluno = db.Alunos.Where((Sgtcc.Models.Aluno a) => a.Id == (int)HttpContext.Session["userID"]).FirstOrDefault();
            if (dadosAluno.Tccs != null)
            {
                return null;
            }
            else if (UploadedFile.ContentLength > 0)
            {
                string fileName = Path.GetFileName(UploadedFile.FileName);
                string folderPath = Path.Combine(Server.MapPath("~/Arquivos"), fileName);

                UploadedFile.SaveAs(folderPath);
/*
                Sgtcc.Models.Arquivo novoArquivo = new Sgtcc.Models.Arquivo();

                novoArquivo.caminho = folderPath;
                novoArquivo.extensao = Path.GetExtension(UploadedFile.FileName);
                novoArquivo.nome = fileName;
                novoArquivo.Id = rnd.Next(32);
                novoArquivo.Tcc = null;

                db.Arquivoes.Add(novoArquivo);
                db.SaveChanges();
*/
            }

            ViewBag.Message = "Arquivo enviado com sucesso";

            return View("Index");

        }
    }
}