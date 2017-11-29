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
        public ActionResult Autherize(Sgtcc.Models.Usuario usuarioModel)
        {
            using (Model1Container db = new Model1Container())
            {
                var detalhesUsuario = db.Usuarios.Where(x => x.cpf == usuarioModel.cpf && x.senha == usuarioModel.senha).FirstOrDefault();
                if(detalhesUsuario == null)
                {
                    usuarioModel.LoginErrorMessage = "CPF ou senha inválidos";
                    return View("Index", usuarioModel);
                }
                else
                {
                    Session["usuarioID"] = usuarioModel.Id;
                    return RedirectToAction("Index","Home");
                }
            }

        }
    }
}