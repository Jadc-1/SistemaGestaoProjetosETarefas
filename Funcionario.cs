using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestaoProjetosETarefas
{
    public class Funcionario
    {
        public int IdFuncionario { get; private set; }
        public List<Tarefa>? Tarefas { get; set; }
    }
}
