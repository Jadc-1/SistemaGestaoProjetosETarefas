using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestaoProjetosETarefas
{
    public class Status
    {
        private static int _IdIncremento = 1;
        public int IdStatus { get; set; }
        public string? Categoria { get; set; }
    }
}
