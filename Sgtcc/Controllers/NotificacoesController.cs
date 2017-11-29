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
    public class NotificacoesController : Controller
    {
        private Model1Container db = new Model1Container();
        // GET: Notificacoes
        public ActionResult Index()
        {

            //Determina se esta em periodo de envio de alertas            
            string dataAgendamento = PeriodoAlertaAgendamento(db.CronogramaAgendamentoes.ToList());
            string dataSubmissao = PeriodoAlertaSubmissao(db.CronogramaArquivoes.ToList());

            if (dataAgendamento != null)
                EnviarEmailAgendamento(db.Tccs2.ToList(), dataAgendamento);           
            if (dataSubmissao != null)
                EnviarEmailSubmissao(db.Alunos.ToList(), db.Arquivoes.ToList(), dataSubmissao);
            //Verica a proximidade da defesa e envia alerta
            EnviarEmailDefesa(db.Tccs2.ToList());
            return View();
        }

        private static void EnviarEmailSubmissao(List<Aluno> destinarioList, List<Arquivo> arquivoList, string dataSubmissao)
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

                email.Body = "Você ainda não submeteu o seu arquivo de TCC. Fique atento, o prazo é até: " + dataSubmissao;

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

        private static void EnviarEmailAgendamento(List<Tcc2> tccList, string dataAgendamento)
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

                email.Body = "Você ainda não agendou sua defesa de TCC. Fique atento, o prazo é até: " + dataAgendamento;

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

        private static void EnviarEmailDefesa(List<Tcc2> tccList)
        {
            try
            {
                NetworkCredential login = new NetworkCredential("sgtcccefet@gmail.com", "123sgtcc");
                System.Net.Mail.MailMessage email = new System.Net.Mail.MailMessage();
                //Obter lista de tccs que ja tem arquivo
                List<Tcc> tccAgendadoList = new List<Tcc>();
                DateTime dataAtual = DateTime.Now;

                foreach (Tcc2 tcc in tccList)
                {
                    if (tcc.data != "")
                        tccAgendadoList.Add(tcc);
                }

                foreach (Tcc2 tcc in tccAgendadoList)
                {
                    TimeSpan diff1 = dataAtual.Subtract((Convert.ToDateTime(tcc.data)));
                    if (diff1.TotalDays <= 5)
                        email.To.Add(new MailAddress(tcc.Aluno.email));
                }

                email.From = new MailAddress("sgtcccefet@gmail.com");
                email.Subject = "Alerta - SGTCC";
                email.Body = "Sua defesa de TCC está se aproximando. Se prepare e tenha faça ótima apresentação  ";

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

        private static string PeriodoAlertaAgendamento(List<CronogramaAgendamento> agendamentoList)
        {
            DateTime dataAtual = DateTime.Now;

            foreach (CronogramaAgendamento agenda in agendamentoList)
            {
                DateTime prazoInicio = Convert.ToDateTime(agenda.prazoInicial);
                DateTime prazoFim = Convert.ToDateTime(agenda.prazoFinal);
                if (DateTime.Compare(prazoInicio, dataAtual) < 0 && DateTime.Compare(dataAtual, prazoFim) > 0)
                {
                    return agenda.prazoFinal.ToString();
                }
            }

            return null;
        }

        private static string PeriodoAlertaSubmissao(List<CronogramaArquivo> submissaoList)
        {
            DateTime dataAtual = DateTime.Now;

            foreach (CronogramaArquivo agendaSubmissao in submissaoList)
            {
                DateTime prazoInicio = Convert.ToDateTime(agendaSubmissao.prazoInicial);
                DateTime prazoFim = Convert.ToDateTime(agendaSubmissao.prazoFinal);
                if (DateTime.Compare(prazoInicio, dataAtual) < 0 && DateTime.Compare(dataAtual, prazoFim) > 0)
                {
                    return agendaSubmissao.prazoFinal.ToString();
                }
            }

            return null;
        }
    }
}