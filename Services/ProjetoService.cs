using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaGestaoProjetosETarefas.Domain;

namespace SistemaGestaoProjetosETarefas.Service
{
    public class ProjetoService
    {
        public static readonly List<Projeto> projetos = new List<Projeto>();

        public void AdicionarProjeto(Projeto projeto)
        {
            if (projeto != null)
            {
                projetos.Add(projeto);
            }
        }

        public void ExcluirProjeto(Projeto projeto)
        {
            if (projeto != null && projetos != null)
            {
                projetos.Remove(projeto);
            }
        }
        public static Dictionary<string, Projeto> ListarProjetos()
        {
            var projetos = new Dictionary<string, Projeto>(); // Dicionário para armazenar as opções de projetos ja criados na lista do ProjetoService

            foreach (var projeto in ProjetoService.projetos)
            {
                if (ProjetoService.projetos.Count != 0)
                {
                    var chave = $"[cornflowerblue] Projeto: [/] {projeto.Nome}"; // Defino que a chave recebe o ID do projeto e o nome do projeto
                    projetos.Add(chave, projeto);
                }
            }
            return projetos;
        }

        public static List<string> ListarTarefasDoProjeto(Projeto projeto)
        {
            List<string> lista = new List<string>();
            if (projeto.Tarefas?.Count != 0)
            {
                foreach(var tarefa in projeto.Tarefas!)
                {
                    lista.Add(tarefa.NomeTarefa!);
                }
                return lista;
            }
            return lista;
        }

    }
}
