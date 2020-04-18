using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestaoPessoas.Web.Models
{
    public class EmpregadoViewModel
    {
        public EmpregadoMOD EmpregadoMod { get; set; }
        public List<DepartamentoMOD> DepartamentoMOD { get; set; }
    }
}