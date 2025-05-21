using SistemaGestaoProjetosETarefas.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestaoProjetosETarefas.Domain
{
    public class Departamento
    {
        private static int _IdIncremento = 1;
        public int IdDept { get; private set; }
        public string? NomeDept { get; set; }
        public List<Funcionario>? Funcionarios { get; set; }
        public bool Ativo { get; set; } = true;

        public Departamento (string nomeDept)
        {
            IdDept = _IdIncremento++;
            this.NomeDept = nomeDept;
            this.Funcionarios = new List<Funcionario>();
        }

        public void AdicionarFuncionario(Funcionario funcionario)
        {
            if (funcionario != null)
            {
                Funcionarios?.Add(funcionario);
            }
        }

        public void RemoverFuncionario(Funcionario funcionario)
        {
            if (funcionario != null && Funcionarios != null)
            {
                Funcionarios.Remove(funcionario); // Remove o funcionário do departamento
            }   
        }

    }
}
