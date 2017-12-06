using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Helpers;
using System.Web.Mvc;
using Sgtcc.Models;

namespace Sgtcc.Controllers
{
    public class Relatorio : Controller
    {
        private Model1Container db = new Model1Container();

        // GET: Relatorio
        public ActionResult Index()
        {
            //db.Tccs.ToList()
            return View();
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
            switch (tcc.status)
            {
                case "1":
                    tcc.situação = "Cadastrado";
                    break;
                case "2":
                    tcc.situação = "Aprovado";
                    break;
                case "3":
                    tcc.situação = "Reprovado";
                    break;
                case "4":
                    tcc.situação = "Cancelado";
                    break;
            }
            return View(tcc);
        }

        public ActionResult ChartPie1()
        {
            List<string> xValue = new List<string>();
            List<string> yValue = new List<string>();

            var tccs = db.Tccs.Where(x => x.status != "5" && x.status != "6" && x.status != "7" && x.status != "8").ToList();
            List<Tcc> results = new List<Tcc>();

            foreach(Tcc tcc in tccs)
            {
                switch (tcc.status)
                {
                    case "1":
                        tcc.situação = "Cadastrado";
                        break;
                    case "2":
                        tcc.situação = "Aprovado";
                        break;
                    case "3":
                        tcc.situação = "Reprovado";
                        break;
                    case "4":
                        tcc.situação = "Cancelado";
                        break;
                }
                results.Add(tcc);
            }

            results.ToList().ForEach(rs => xValue.Add(rs.situação));
            results.ToList().ForEach(rs => yValue.Add(rs.titulo));

            new Chart(width: 600, height: 400, theme: ChartTheme.Vanilla)
            .AddTitle("Situação TCC1")
                  .AddLegend("Summary")
                  .AddSeries("Default", chartType: "Pie", xValue: xValue, yValues: yValue)
                  .Write("bmp");

            return null;
        }

        public ActionResult ChartPie2()
        {
            List<string> xValue = new List<string>();
            List<string> yValue = new List<string>();

            var tccs = db.Tccs2.Where(x => x.status != "5" && x.status != "6" && x.status != "7" && x.status != "8").ToList();
            List<Tcc> results = new List<Tcc>();

            foreach (Tcc2 tcc in tccs)
            {
                switch (tcc.status)
                {
                    case "1":
                        tcc.situação = "Cadastrado";
                        break;
                    case "2":
                        tcc.situação = "Aprovado";
                        break;
                    case "3":
                        tcc.situação = "Reprovado";
                        break;
                    case "4":
                        tcc.situação = "Cancelado";
                        break;
                }
                results.Add(tcc);
            }

            results.ToList().ForEach(rs => xValue.Add(rs.situação));
            results.ToList().ForEach(rs => yValue.Add(rs.titulo));

            new Chart(width: 600, height: 400, theme: ChartTheme.Vanilla)
            .AddTitle("Situação TCC2")
                  .AddLegend("Summary")
                  .AddSeries("Default", chartType: "Pie", xValue: xValue, yValues: yValue)
                  .Write("bmp");

            return null;
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
