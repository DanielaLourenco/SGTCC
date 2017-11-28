using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sgtcc.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult IndexAluno()
        {
            return View();
        }

        public ActionResult IndexProfessor()
        {
            return View();
        }

        public ActionResult IndexAdmin()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "SGTCC - Sistema de gerenciamento de Trabalhos de Conclusão de Curso.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "SGTCC - Contato.";

            return View();
        }
    }
}