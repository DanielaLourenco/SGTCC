using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Mail;
using System.Net;
using Sgtcc.Models;

namespace Sgtcc.Controllers
{
    public class HomeController : Controller
    {
        private Model1Container db = new Model1Container();

        public ActionResult Index()
        {
            //Determina se esta em periodo de envio de alertas
            if(PeriodoAlertaAgendamento(db.CronogramaAgendamentoes.ToList()))
                EnviarEmailAgendamento(db.Tccs2.ToList());
            if (PeriodoAlertaSubmissao(db.CronogramaArquivoes.ToList()))
                EnviarEmailSubmissao(db.Alunos.ToList(), db.Arquivoes.ToList());
            
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
        private static void EnviarEmailSubmissao(List<Aluno> destinarioList, List<Arquivo> arquivoList)
        {
            try
            {
                NetworkCredential login = new NetworkCredential("sgtcccefet@gmail.com", "123sgtcc");
                System.Net.Mail.MailMessage email = new System.Net.Mail.MailMessage();
                //Obter lista de tccs que ja tem arquivo
                List<String> tccsComArquivo = new List<string>();
                foreach (Arquivo arquivo in arquivoList)
                {
                    tccsComArquivo.Add(arquivo.Tcc.Id.ToString());
                }

                foreach (Aluno aluno in destinarioList)
                {
                    if (!tccsComArquivo.Contains(aluno.Id.ToString()))
                        email.To.Add(new MailAddress(aluno.email));
                }
              
                email.From = new MailAddress("sgtcccefet@gmail.com");
                email.Subject = "Alerta - SGTCC";

                email.Body = "Você ainda não submeteu o seu arquivo de TCC. Fique atento, o prazo é até: ";

                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                client.Credentials = login;
                client.Send(email);
            }
            catch
            {

            }
        }

        private static void EnviarEmailAgendamento(List<Tcc2> tccList)
        {
            try
            {
                NetworkCredential login = new NetworkCredential("sgtcccefet@gmail.com", "123sgtcc");
                System.Net.Mail.MailMessage email = new System.Net.Mail.MailMessage();
                //Obter lista de tccs que ja tem arquivo
                List<Aluno> alunoSemAgendamentoList = new List<Aluno>();
                foreach (Tcc2 tcc in tccList)
                {
                    if (tcc.data == "")
                        alunoSemAgendamentoList.Add(tcc.Aluno);
                }

                foreach (Aluno aluno in alunoSemAgendamentoList)
                {

                    email.To.Add(new MailAddress(aluno.email));
                }
                              
                email.From = new MailAddress("sgtcccefet@gmail.com");
                email.Subject = "Alerta - SGTCC";

                email.Body = "Você ainda não agendou sua defesa de TCC. Fique atento, o prazo é até: ";

                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                client.Credentials = login;
                client.Send(email);
            }
            catch
            {

            }
        }

        private static bool PeriodoAlertaAgendamento(List<CronogramaAgendamento> agendamentoList)
        {
            DateTime dataAtual = DateTime.Now;

            foreach (CronogramaAgendamento agenda in agendamentoList)
            {
                DateTime prazoInicio = Convert.ToDateTime(agenda.prazoInicial.ToString());
                DateTime prazoFim = Convert.ToDateTime(agenda.prazoFinal.ToString());

                if (DateTime.Compare(prazoInicio, dataAtual) < 0 && DateTime.Compare(dataAtual, prazoFim) < 0)
                    return true;
            }

            return false;
        }

        private static bool PeriodoAlertaSubmissao(List<CronogramaArquivo> submissaoList)
        {
            DateTime dataAtual = DateTime.Now;

            foreach (CronogramaArquivo agendaSubmissao in submissaoList)
            {
                DateTime prazoInicio = Convert.ToDateTime(agendaSubmissao.prazoInicial.ToString());
                DateTime prazoFim = Convert.ToDateTime(agendaSubmissao.prazoFinal.ToString());

                if (DateTime.Compare(prazoInicio, dataAtual) < 0 && DateTime.Compare(dataAtual, prazoFim) < 0)
                    return true;
            }
            
            return false;
        }

        public bool VerificaEnvio() {
            string dateString = "5/1/2008 8:30:52 AM";
            DateTime date1 = DateTime.Parse(dateString,
                                      System.Globalization.CultureInfo.InvariantCulture);
            return true;
        }
    }
}