using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

// adicionar o EntityFramework
using System.Data.Entity;
using GestaoPessoas.Web.Models;

namespace GestaoPessoas.Web.Contexto
{
    // para utilizar as funcionalidades do entity
    // precisamos criar uma classe que herda de DbContext
    public class GestaoPessoaContext : DbContext
    {
        // adicione duas propriedades do DbSet denominadas 
        // Empregados e Departamentos, conforme mostrado a seguir
        public DbSet<EmpregadoMOD> Empregados { get; set; }
        public DbSet<DepartamentoMOD> Departamentos { get; set; }
    }
}