using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sgtcc.Controllers
{
    public class ArquivoController : Controller
    {
        // GET: Arquivo
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UploadFile(HttpPostedFileBase UploadedFile)
        {

            if (UploadedFile.ContentLength > 0)
            {
                string fileName = Path.GetFileName(UploadedFile.FileName);
                string folderPath = Path.Combine(Server.MapPath("~/Arquivos"),fileName);

                UploadedFile.SaveAs(folderPath);
            }

            ViewBag.Message = "Arquivo enviado com sucesso";

            return View("Index");

        }
    }
}