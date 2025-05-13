using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestaoProjetosETarefas.Domain
{
    public class Funcionario : Usuario
    {
        private static int _IdIncremento = 0;
        public int IdFuncionario { get; private set; }
        public List<Tarefa>? Tarefas { get; set; }
        public Departamento? Departamento { get; set; }

        public Funcionario(string nome, string email, string telefone, DateTime dataCadastro, Endereco? endereco) : base(nome, email, telefone, dataCadastro, endereco)
        {
            this.IdFuncionario = _IdIncremento++;
            this.Tarefas = new List<Tarefa>();
        }
    }
}
