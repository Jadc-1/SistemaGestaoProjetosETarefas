using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaGestaoProjetosETarefas.Domain;

namespace SistemaGestaoProjetosETarefas.Services
{
    public class FuncionarioService
    {
        private static readonly List<Funcionario> funcionarios = new List<Funcionario>();
        
        public void AdicionarFuncionario(Funcionario funcionario)
        {
            funcionarios.Add(funcionario);
        }
        public void DesativarFuncionario(int id)
        {
            var funcionario = funcionarios.FirstOrDefault(f => f.IdFuncionario == id);
            if (funcionario != null)
            {
                funcionario.Desativar(funcionario);
            }
            else
            {
                Console.WriteLine("Funcionário não encontrado");
            }
        }
        public static Dictionary<string, Funcionario> ListarFuncionarios()
        {
            var dicionarioFunc = new Dictionary<string, Funcionario>();
            foreach (var funcionario in funcionarios)
            {
                var chave = $"[cornflowerblue] Funcionário: {funcionario.Nome}";
                dicionarioFunc.Add(chave, funcionario);
            }
            return dicionarioFunc;
        }
    }
}
