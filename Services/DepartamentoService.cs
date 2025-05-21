using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaGestaoProjetosETarefas.Domain;

namespace SistemaGestaoProjetosETarefas.Service
{
    public class DepartamentoService
    {
        private static List<Departamento> _departamentos { get; set; } = new List<Departamento>();

        public Dictionary<string, Departamento> ListarDepartamentos()
        {
            var departamentos = new Dictionary<string, Departamento>(); // Dicionário para armazenar as opções de departamentos ja criados na lista do DepartamentoService
            foreach (var departamento in _departamentos)
            {
                var chave = $"Departamento: {departamento}";
                departamentos.Add(chave, departamento); // Adiciona o departamento ao dicionário
            }
            return departamentos;
        }
        public void AdicionarDepartamento(Departamento departamento)
        {
            if (departamento != null)
            {
                _departamentos.Add(departamento);
            }
        }
        public void DesativarDepartamento(int id)
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
