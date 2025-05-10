using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestaoProjetosETarefas.Gerenciadores
{
    public class DepartamentoGerenciador
    {
        private static List<Departamento> _departamentos { get; set; } = new List<Departamento>();

        public static void ListarDepartamentos()
        {
            if (_departamentos != null)
            {
                foreach (var departamento in _departamentos)
                {
                    if (departamento.Ativo != false) // Verifica se o departamento está ativo
                    {
                        Console.WriteLine($"ID: {departamento.IdDept} - Nome: {departamento.NomeDept}");
                    }

                }
            }
        }
        public static void AdicionarDepartamento(Departamento departamento)
        {
            if (departamento != null)
            {
                _departamentos.Add(departamento);
            }
        }
        public static void DesativarDepartamento(int id)
        {
            var departamento = _departamentos.FirstOrDefault(departamento => departamento.IdDept == id); // Busca o departamento pelo ID
            if (departamento != null && departamento.Ativo != false)
            {
                departamento.Funcionarios?.Clear(); // Remove todos os funcionários associados ao departamento
                departamento.Ativo = false; // Marca o departamento como inativo
            }
        }
    }
}
