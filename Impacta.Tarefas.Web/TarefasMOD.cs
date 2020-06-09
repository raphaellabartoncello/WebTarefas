using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Impacta.Tarefas.Web
{
    public class TarefasMOD
    {
        public int Id { get; set; }
        [Required] //Atributo que indica que o campo é obrigatório
        [Display(Name ="Nome da Tarefa")]
        [MaxLength(100, ErrorMessage ="Tamanho máximo de 100 caracteres")]
        public string Nome { get; set; }
        [Required]
        public int Prioridade { get; set; }
        [Display(Name = "Status Atual")]
        public bool Concluida { get; set; }
        [Display(Name = "Anotações")]
        [MaxLength(200)]
        public string Observacoes { get; set; }

    }
}