using Sgtcc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sgtcc.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Autherize(Sgtcc.Models.Usuario userModel)
        {
            using (Model1Container db = new Model1Container())
            {
                var userDetails = db.Usuarios.Where(x => x.cpf == userModel.cpf && x.senha == userModel.senha).FirstOrDefault();
                if (userDetails == null)
                {
                    userModel.LoginErrorMessage = "CPF ou senha inválidos";
                    return View("Index", userModel);
                }
                else if (db.Alunos.Where((Sgtcc.Models.Aluno a) => a.Id == userDetails.Id).FirstOrDefault() != null)
                {   // aluno
                    Session["userID"] = userDetails.Id;
                    Session["userName"] = userDetails.nome;
                    return RedirectToAction("IndexAluno", "Home");
                }
                else if (db.Professores.Where((Sgtcc.Models.Professor p) => p.Id == userDetails.Id).FirstOrDefault() != null)
                {   // professor
                    Session["userID"] = userDetails.Id;
                    Session["userName"] = userDetails.nome;
                    return RedirectToAction("IndexProfessor", "Home");
                }
                else
                {   // admin
                    Session["userID"] = userDetails.Id;
                    Session["userName"] = userDetails.nome;
                    return RedirectToAction("IndexAdmin", "Home");
                }
            }
        }

        public ActionResult LogOut()
        {
            int userId = (int)Session["userID"];
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }
    }
}