using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;

namespace GestaoPessoas.Web.Models
{
    public class DepartamentoMOD
    {
        [Key]
        public int DepartamentoId { get; set; }
        [Display(Name ="Nome do Departamento")]
        public string Nome { get; set; }
        [Display(Name ="Descrição do Departamento")]
        public string Descricao { get; set; }

        /* Propriedades de navegação são as propriedades da classe através 
         * da qual é possível acessar entidades relacionadas por meio do 
         * Entity Framework ao buscar dados. Portanto, ao buscar os dados 
         * do Departamentos, precisamos buscar os detalhes dos 
         * Empregados que estao associados. As propriedades de navegação são adicionadas como 
         * propriedades virtuais na entidade. */
        public virtual ICollection<EmpregadoMOD> Empregados { get; set; }
    }
}