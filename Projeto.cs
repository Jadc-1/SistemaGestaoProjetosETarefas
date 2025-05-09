using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestaoProjetosETarefas
{
    public class Projeto
    {
        private static int _IdIncremento = 1;
        public int CodigoDoProjeto { get; private set; }
        public string? Nome { get; set; }
        public string? Desc { get; set; }
        public List<Tarefa>? Tarefas { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataTermino { get; set; }
        public Status? StatusProjeto { get; set; }
        public string? Prioridade { get; set; }
        public Gestor? GestorDelegado { get; set; }

        public Projeto(string nome, string desc, DateTime dataInicio, Status statusProjeto, string prioridade)
        {
            CodigoDoProjeto = _IdIncremento++;
            this.Nome = nome;
            this.Desc = desc;
            this.Tarefas = new List<Tarefa>();
            this.DataInicio = dataInicio;
            this.StatusProjeto = statusProjeto;
            this.Prioridade = prioridade;
        }

        public void AdicionarTarefa(Tarefa tarefa)
        {
            if(tarefa != null)
            {
                Tarefas?.Add(tarefa);
            }
        }

        public void RemoverTarefa(Tarefa tarefa)
        {
            if (tarefa != null && Tarefas != null)
            {
                Tarefas.Remove(tarefa);
            }
        }

        public void AlterarStatus(Status status)
        {
            if (status != null)
            {
                this.StatusProjeto = status;
            }
        }

        public void AlterarPrioridade(string prioridade)
        {
            if (!string.IsNullOrEmpty(prioridade))
            {
                this.Prioridade = prioridade;
            }
        }

        public void AtribuirGestor(Gestor gestor)
        {
            if (gestor != null)
            {
                this.GestorDelegado = gestor;
            }
        }

        public void RemoverGestor()
        {
            this.GestorDelegado = null;
        }

        public void FinalizarProjeto()
        {
            StatusProjeto = Status.Concluido;
        }

    }
}
