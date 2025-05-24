using SistemaGestaoProjetosETarefas.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestaoProjetosETarefas.Services
{
    public class StatusService
    {
        static List<Status> ListaStatus = Status.RetornarListaStatus();

        public static Dictionary<string, Status> ListarStatus()
        {
            var status = new Dictionary<string, Status>(); // Dicionário para armazenar as opções de status ja criados na lista do StatusService
            foreach(var item in ListaStatus)
            {
                var chave = $"{item.Categoria}"; // Defino que a chave recebe o ID do status e o nome do status
                status.Add(chave, item);
            }
            return status;
        }
    }
}
