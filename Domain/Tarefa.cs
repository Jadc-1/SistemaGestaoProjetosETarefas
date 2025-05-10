using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestaoProjetosETarefas.Domain
{
    public class Tarefa
    {
        private static int _idIncremento = 1;
        public int IdTarefa { get; private set; }
        public string? NomeTarefa { get; set; }
        public string? DescricaoTarefa { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime Datatermino { get; set; }
        public Status? StatusTarefa { get; set; }
        public char Prioridade { get; set; }
        public Funcionario? FuncionarioDelegado { get; set; }

        public Tarefa(string? nomeTarefa, string? descricaoTarefa, DateTime dataInicio, Status? statusTarefa, char prioridade)
        {
            IdTarefa = _idIncremento++;
            NomeTarefa = nomeTarefa;
            DescricaoTarefa = descricaoTarefa;
            DataInicio = dataInicio;
            StatusTarefa = statusTarefa;
            Prioridade = prioridade;
        }
    }
}
