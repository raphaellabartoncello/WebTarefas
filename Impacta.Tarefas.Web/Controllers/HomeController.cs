using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Impacta.Tarefas.Web.Controllers
{
    public class HomeController : Controller
    {

        Repository repoTarefasTB = null;

        public ActionResult Index()
        {
            Repository repo = new Repository();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Tarefas()
        {
            return View();
        }

        public ActionResult NovasTarefas()
        {
            return View();
        }

        public ActionResult NovasTarefas(TarefasMOD tarefasMOD)
        {
            Repository repo = new Repository();

            try
            {
                if (repo.Create(tarefasMOD))
                {
                    TempData["Sucesso"] = "Tarefa cadastrada com sucesso!";
                }

                else
                {
                    ViewBag.Falha = "O cadastro da tarefa não foi realizado.";
                }
            }
            catch (Exception ex)
            {
                ViewBag.Falha = "[ERRO] Verifique:" + ex.Message;
            }
            return View();
        }
        //Método devolve a lista de tarefas para o Browser

        [HttpPost]
        public ActionResult ListarTodasTarefas()
        {
            return View();
        }

        public ActionResult Detalhes(int? id)
        {
            TarefasMOD tarefa = null;

            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                //Se não for nulo faremos a busca
                repoTarefasTB = new Repository();

                tarefa = repoTarefasTB.Read(Convert.ToInt32(id));

                if (tarefa == null)
                {
                    return HttpNotFound();
                }
            }
            catch (Exception ex)
            {

                ViewBag.Falha = "O cadastro da tarefa não foi realizado: " + ex.Message;
            }

            return RedirectToAction("NovaTarefa");
        }

    }
}