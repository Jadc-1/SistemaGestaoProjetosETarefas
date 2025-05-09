using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestaoProjetosETarefas
{
    public class Tarefa
    {
        public int IdTarefa { get; private set; }
        public string? NameTarefa { get; set; }
        public string? DescricaoTarefa { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime Datatermino { get; set; }
        public Status? StatusTarefa { get; set; }
        public char Prioridade { get; set; }
        public Funcionario? FuncionarioDelegado { get; set; }
    }
}
