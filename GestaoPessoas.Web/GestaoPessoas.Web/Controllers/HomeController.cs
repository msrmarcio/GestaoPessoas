// adicione uma nova entrada para usar a classe Helper para 
// usar nosso DbContext
using GestaoPessoas.Web.Contexto;
using GestaoPessoas.Web.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace GestaoPessoas.Web.Controllers
{
    public class HomeController : Controller
    {
        /* Vamos escrever um código para executar operações de banco de dados 
         * com o nosso código. Para isso vamos criar uma variavel e instanciar ela 
         quando precisar utilizar */
        DataAccessHelper dataAccessHelper = null;

        public ActionResult Index()
        {

            return View();
        }

        public ActionResult Empregados()
        {
            List<EmpregadoMOD> lstEmpregadoMOD = null;

            try
            {
                dataAccessHelper = new DataAccessHelper();

                lstEmpregadoMOD = dataAccessHelper.BuscarEmpregados();

            }
            catch (Exception ex)
            {

                throw;
            }

            return View(lstEmpregadoMOD);
        }

        public ActionResult Editar(int id = 0)
        {
            EmpregadoMOD empResult = null;

            try
            {
                dataAccessHelper = new DataAccessHelper();

                if (id >= 0)
                {
                    empResult = dataAccessHelper.BuscarEmpregado(id);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View(empResult);
        }

        public ActionResult Criar()
        {
            dataAccessHelper = new DataAccessHelper();
            /* consulta todos departamentos */
             IEnumerable<DepartamentoMOD> listaDpto = dataAccessHelper.BuscarDepartamentos();

            // Criando uma ViewBag com uma lista de clientes. 
            // Utilizo o nome da ViewBag com ClienteId apenas 
            // para facilitar o entendimento do código 
            // new SelectList significa retornar uma 
            // estrutura de DropDownList 
            ViewBag.DepartamentoId = new SelectList(listaDpto, "DepartamentoId", "Nome"); 

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Criar(EmpregadoMOD empregado)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    dataAccessHelper = new DataAccessHelper();
                    var result = dataAccessHelper.AddEmpregado(empregado);
                    if (result > 0) TempData["AddSuccess"] = "Empregado cadastrado com sucesso";

                    return RedirectToAction("Index");
                }
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name after DataException and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(empregado);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(int? id)
        {
            dataAccessHelper = new DataAccessHelper();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmpregadoMOD empregado = dataAccessHelper.GetEmpregadoId(Convert.ToInt32(id));

            if (TryUpdateModel(empregado, "",
               new string[] { "Nome" }))
            {
                try
                {
                    dataAccessHelper.UpdateEmpregado(empregado);

                    return RedirectToAction("Empregados");
                }
                catch (DataException dex)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            return View(empregado);
        }

        public ActionResult Detalhes(int id = 0)
        {
            dataAccessHelper = new DataAccessHelper();
            EmpregadoMOD empregadoMod = null;

            try
            {
                empregadoMod = dataAccessHelper.GetEmpregadoId(id);

                if (empregadoMod == null)
                {
                    throw new HttpException(404, "Not found");
                }

                return View(empregadoMod);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "ERROS: " + ex.Message);
            }

            return View(empregadoMod);
        }


        /* antes da exclusao  fazemso a pesquisa do item a ser excluido e pedimos uma confirmacao. 
         * A exclusao ocorrera na chamada do metodo excluir que responde pelo atributo HttpPost .
         O parametro saveChangesError é uma flag que vamos receber do metodo post */
        [HttpGet]
        public ActionResult Excluir(int id = 0, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Exclusao falhou. Tente novamente, e se o problema persistir veja com seu administrador de sistema";
            }

            dataAccessHelper = new DataAccessHelper();
            EmpregadoMOD empregado = dataAccessHelper.GetEmpregadoId(id);
            if (empregado == null)
            {
                return HttpNotFound();
            }
            return View(empregado);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Excluir(int id)
        {
            try
            {
                EmpregadoMOD empregado = null;
                dataAccessHelper = new DataAccessHelper();

                var result = dataAccessHelper.ExcluirById(id);


            }
            catch (Exception ex/* dex */)
            {
                // a variavel ex recebera a exception
                // caso ocorrer uma falha para excluir, ele chama o metodo Excluir GET, passando a flag true e id
                return RedirectToAction("Excluir", new { id = id, saveChangesError = true });
            }
            return RedirectToAction("Index");
        }


    }
}