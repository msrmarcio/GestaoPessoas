using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

using GestaoPessoas.Web.Models;

namespace GestaoPessoas.Web.Contexto
{
    /* Vamos tornar nossa implementação mais abstrata e, 
     * em vez de acessar o dbContext diretamente do Controller, 
     * vamos abstraí-la em uma classe chamada DataAccessHelper. 
     * Esta classe atuará como uma classe auxiliar para todas as 
     * nossas operações de banco de dados.
     * */
    public class DataAccessHelper
    {
        /* Crie uma instância somente leitura da classe DbContext GestaoPessoaContext */
        readonly GestaoPessoaContext _dbContext = new GestaoPessoaContext();
        
        /* Adicione alguns metodos para obter dados do Empregado e dos departamentos
         * AproveitAMOS e adicionamos a importacao do namespace da Models para usar Empregado e
         * Departamento
         * */
         public List<EmpregadoMOD> BuscarEmpregados()
        {
            return _dbContext.Empregados.ToList();
        }

        public EmpregadoMOD BuscarEmpregado(int id)
        {
            var query = from emp in _dbContext.Empregados
                        where emp.EmpregadoId == id
                        select emp;

            EmpregadoMOD temp = query.ToList().FirstOrDefault();

            return temp;
        }

        public List<DepartamentoMOD> BuscarDepartamentos()
        {
            return _dbContext.Departamentos.ToList();
        }

        public List<String> BuscarDepartamentosNome()
        {
            return _dbContext.Departamentos.Select(x => x.Nome).ToList();
        }


        public int AddEmpregado(EmpregadoMOD empregado)
        {
            // Todo contexto, lista apos o ponto as tabelas que vc
            // quer acessar e usar. Todo contexto fornence via Herança
            // o metodo ADD por exemplo, que serve para adicioanr um objeto model
            // para ser incluido na tabela.
            _dbContext.Empregados.Add(empregado);
            _dbContext.SaveChanges();

            return empregado.EmpregadoId;
        }

        public EmpregadoMOD GetEmpregadoId(int id)
        {
            EmpregadoMOD empregado = _dbContext.Empregados.Find(id);

            return empregado;
        }

        public int AddDepartamento(DepartamentoMOD departamento)
        {
            _dbContext.Departamentos.Add(departamento);
            _dbContext.SaveChanges();
            return departamento.DepartamentoId;
        }

        public void UpdateEmpregado(EmpregadoMOD empregado)
        {
            _dbContext.Entry(empregado).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public int ExcluirById(int id)
        {
            EmpregadoMOD empregado = _dbContext.Empregados.Find(id);
            _dbContext.Empregados.Remove(empregado);

            return _dbContext.SaveChanges();
        }

        
    }
}