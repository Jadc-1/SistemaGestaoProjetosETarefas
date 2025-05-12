using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestaoProjetosETarefas.Domain
{
    public class Status
    {
        private static int _IdIncremento = 0;
        public int IdStatus { get; private set; }
        public string? Categoria { get; set; }

        public Status(string categoria)
        {
            IdStatus = _IdIncremento++;
            this.Categoria = categoria;
        }

        public static readonly Status EmAndamento = new Status("Em Andamento");
        public static readonly Status Concluido = new Status("Concluído");
        public static readonly Status Pendente = new Status("Pendente");
        public static readonly Status Cancelado = new Status("Cancelado");
        public static readonly Status Atrasado = new Status("Atrasado");
        public static readonly Status EmEspera = new Status("Em Espera");
        public static readonly Status EmRevisao = new Status("Em Revisão");

        public static List<Status> ExibirTodosStatus()
        {
            return new List<Status>
            {
                EmAndamento,
                Concluido,
                Pendente,
                Cancelado,
                Atrasado,
                EmEspera,
                EmRevisao
            };
        }

        public static void AdicionarNovoStatus(string categoria)
        {
            if (categoria != null)
            {          
                Status status = new Status(categoria);
                ExibirTodosStatus().Add(status);
            }
        }

        public static void RemoverStatus(Status status)
        {
            if (status != null)
            {
                ExibirTodosStatus().Remove(status);
            }
        }
    }
}
