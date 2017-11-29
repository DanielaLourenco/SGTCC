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
                var usuarioModel = db.Usuarios.Where(x => x.cpf == userModel.cpf && x.senha == userModel.senha).FirstOrDefault();
                if (usuarioModel == null)
                {
                    usuarioModel.LoginErrorMessage = "CPF ou senha inválidos";
                    return View("Index", usuarioModel);
                }
                else if( db.Alunos.Where((Sgtcc.Models.Aluno a) => a.Id == usuarioModel.Id).First() != null )
                {   // aluno
                    Session["userID"] = usuarioModel.Id;
                    Session["userName"] = usuarioModel.nome;
                    return RedirectToAction("IndexAluno", "Home");
                }
                else if (db.Professores.Where((Sgtcc.Models.Professor p) => p.Id == usuarioModel.Id).First() != null)
                {   // professor
                    Session["userID"] = usuarioModel.Id;
                    Session["userName"] = usuarioModel.nome;
                    return RedirectToAction("IndexProfessor", "Home");
                }
                else
                {   // admin
                    Session["userID"] = usuarioModel.Id;
                    Session["userName"] = usuarioModel.nome;
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