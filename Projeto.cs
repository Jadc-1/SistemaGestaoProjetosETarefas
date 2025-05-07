using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestaoProjetosETarefas
{
    public class Projeto
    {
        public int CodigoDoProjeto { get; set; }
        public string? Nome { get; set; }
        public string? Desc { get; set; }
        public List<Tarefa>? Tarefas { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataTermino { get; set; }
        public Status? StatusTarefa { get; set; }
        public string? Prioridade { get; set; }
    }
}
