using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;

namespace Impacta.Tarefas.Web.Controllers
{
    public class HomeController : Controller
    {
        //Adicionado objeto a ser instanciado
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
            List<TarefasMOD> listaTarefas = null;

            //Criar o objeto repository
            Repository repo = new Repository();

            try
            {
                //Preencher com a lista de rarefas vinda do select
                listaTarefas = repo.ReadAll();
            }
            catch (Exception ex)
            {

                ViewBag.Falha = "O cadastro da tarefa não foi realizado.";

                //etorna e mantém os dados nos campos - Não recarrega a página
                return RedirectToAction("Index");
            }

            //Retorna a lista de tarefas já preenchidas para a VIEW tipada que já espera um objeto IEnumerable<T> que é do tipo TarefaMOD
            return View(listaTarefas);
        }

        //O parametro int com a interrogação significa que o id pode ser nulo
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

                //Retorna e mantém os dados nos campos - Não recarrega a página
                return View();
            }
            //View tipada - estou passando o objeto
            return View(tarefa);
        }

        public ActionResult Editar(int id = 0)
        {
            TarefasMOD tarefa = null;

            try
            {
                if (id <= 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                //se não for nulo então faz a busca
                repoTarefasTB = new Repository();

                tarefa = repoTarefasTB.Read(id);

                if (tarefa == null)
                {
                    return HttpNotFound();
                }

            }
            catch (Exception ex)
            {

                ViewBag.Falha = "O cadastro da tarefa não foi realizado: " + ex.Message;

                //Retorna e mantém os dados nos campos - Não recarrega a página
                return View();
            }
            return View(tarefa);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(TarefasMOD tarefas)
        {
            try
            {
                //Verificar se os dados informados são válidos
                if (ModelState.IsValid)
                {
                    repoTarefasTB = new Repository();

                    repoTarefasTB.Update(tarefas);

                    return RedirectToAction("ListarTodasTarefas");
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            //Caso não consiga alterar vai retornar a chamada da mesma View
            return View(tarefas);
        }

        public ActionResult Excluir(int id = 0)
        {
            TarefasMOD tarefas = null;

            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                repoTarefasTB = new Repository();

                tarefas = repoTarefasTB.Read(Convert.ToInt32(id));

                if (tarefas == null)
                {
                    return HttpNotFound();
                }

            }
            catch (Exception ex)
            {

                ViewBag.Falha = "O cadastro da tarefa não foi realizado: " + ex.Message;

                //Retorna e mantém os dados nos campos - Não recarrega a página
                return View();
            }
            return View(tarefas);
        }

        //POST: /Home/Delete/5
        [HttpPost, ActionName("Excluir")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TarefasMOD tarefas = null;

            try
            {
                if (id <=0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                repoTarefasTB = new Repository();

                //Vamos acessar a base novamente para verificar se o arquivo permanece lá - pode ter sido excluído por outra pessoa
                tarefas = repoTarefasTB.Read(Convert.ToInt32(id));

                if (tarefas == null)
                {
                    return HttpNotFound();
                }
                repoTarefasTB.Delete(id);
            }
            catch (Exception)
            {

                throw;
            }

            return View("ListarTodasTarefas");
        }
    }
}