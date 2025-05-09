using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestaoProjetosETarefas
{
    public class Departamento
    {
        private static int _IdIncremento = 1;
        public static List<Departamento> Departamentos { get; set; } = new List<Departamento>();
        public int IdDept { get; private set; }
        public string? NomeDept { get; set; }
        public List<Funcionario>? Funcionarios { get; set; }
        public bool Ativo { get; set; } = true;

        public Departamento (string nomeDept)
        {
            IdDept = _IdIncremento++;
            this.NomeDept = nomeDept;
            Departamentos.Add(this);
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

        public static void ListarDepartamentos()
        {
            if(Departamentos != null)
            {
                foreach (var departamento in Departamentos)
                {
                    if(departamento.Ativo != false) // Verifica se o departamento está ativo
                    {
                        Console.WriteLine($"ID: {departamento.IdDept} - Nome: {departamento.NomeDept}");
                    }    
                    
                }
            }
        }

        public static void DesativarDepartamento(int id)
        {
            var departamento = Departamentos.FirstOrDefault(departamento => departamento.IdDept == id); // Busca o departamento pelo ID
            if (departamento != null && departamento.Ativo != false)
            {
                departamento.Funcionarios?.Clear(); // Remove todos os funcionários associados ao departamento
                departamento.Ativo = false; // Marca o departamento como inativo
            }
        }
    }
}
