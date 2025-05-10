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
        public List<Projeto> projetos = new List<Projeto>();

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

        public void ListarProjetos()
        {
            if(projetos != null)
            {
                foreach (var projeto in projetos)
                {
                    Console.WriteLine($"Projeto: {projeto.Nome}");
                }
            }
        }
    }
}
