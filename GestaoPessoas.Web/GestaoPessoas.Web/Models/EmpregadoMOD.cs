using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GestaoPessoas.Web.Models
{
    public class EmpregadoMOD
    {
        [Key]
        public int EmpregadoId { get; set; }

        [Display(Name ="Nome do Funcionário")]
        public string Nome { get; set; }

        // Como um funcionário pertence a um departamento, 
        // cada funcionário teria um departamento relacionado a ele
        public int DepartamentoId { get; set; }

        /* Propriedades de navegação são as propriedades da classe através 
         * da qual é possível acessar entidades relacionadas por meio do 
         * Entity Framework ao buscar dados. Portanto, ao buscar os dados dos 
         * Empregados, podemos precisar buscar os detalhes dos Departamentos relacionados 
         * e, as propriedades de navegação são adicionadas como 
         * propriedades virtuais na entidade. */
        public virtual DepartamentoMOD Departamento { get; set; }
    }
}
 