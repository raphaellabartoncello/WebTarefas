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
        [Required(ErrorMessage ="A descrição da tarefa é requerida")] //Atributo que indica que o campo é obrigatório
        [Display(Name ="Nome da Tarefa")]
        [MaxLength(50, ErrorMessage ="Tamanho máximo de 50 caracteres")]
        public string Nome { get; set; }
        [Required]
        public int Prioridade { get; set; }
        [Display(Name = "Finalizada?")]
        public bool Concluida { get; set; }
        [Display(Name = "Anotações")]
        [MaxLength(100)]
        public string Observacoes { get; set; }

    }
}