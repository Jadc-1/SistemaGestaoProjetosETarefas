using Spectre.Console;
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
        public DateTime DataTermino { get; set; }
        public Status? StatusTarefa { get; set; }
        public char Prioridade { get; set; }
        public Funcionario? FuncionarioDelegado { get; set; }

        public Tarefa(string? nomeTarefa, string? descricaoTarefa, DateTime dataInicio, Status? statusTarefa, char prioridade)
        {
            IdTarefa = _idIncremento++;
            this.NomeTarefa = nomeTarefa;
            this.DescricaoTarefa = descricaoTarefa;
            this.DataInicio = dataInicio;
            this.DataTermino = default; // Inicializa a data de término como padrão (DateTime.MinValue)
            this.StatusTarefa = statusTarefa;
            this.Prioridade = prioridade;
        }

        public void AtribuirFuncionario(Funcionario funcionario)
        {
            if (funcionario != null)
            {
                FuncionarioDelegado = funcionario;
            }
        }

        public void FinalizarTarefa()
        {
            if (StatusTarefa != null && StatusTarefa != Status.Concluido)
            {
                StatusTarefa = Status.Concluido;
                DataTermino = DateTime.Now; // Define a data de término como a data atual
            }
            else
            {
                AnsiConsole.Markup("[red]A tarefa já está concluída ou o status é inválido.[/]");
            }
        }

        public void CancelarTarefa()
        {
            if (StatusTarefa != null && StatusTarefa != Status.Cancelado)
            {
                StatusTarefa = Status.Cancelado;
                DataTermino = DateTime.Now; // Define a data de término como a data atual
            }
            else
            {
                AnsiConsole.Markup("[red]A tarefa já está cancelada ou o status é inválido.[/]");
            }
        }
    }
}
