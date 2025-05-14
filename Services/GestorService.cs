using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaGestaoProjetosETarefas.Domain;

namespace SistemaGestaoProjetosETarefas.Services
{
    public class GestorService
    {
        private static readonly List<Gestor> gestores = new List<Gestor>();
        public void AdicionarGestor(Gestor gestor)
        {
            if (gestor != null)
            {
                gestores.Add(gestor);
            }
        }
        public void DesativarGestor(int id)
        {
            var gestor = gestores.FirstOrDefault(g => g.IdGestor == id);
            if(gestor != null)
            {
                gestor.Desativar(gestor);
            }
            else
            {
                Console.WriteLine("Gestor não encontrado");
            }

        }
        public static Dictionary<string, Gestor> ListarGestores()
        {
            var gestorDict = new Dictionary<string, Gestor>();
            foreach (var gestor in gestores)
            {
                var chave = $"[cornflowerblue] Gestor: [/] {gestor.Nome}";
                gestorDict.Add(chave, gestor);
            }
            return gestorDict;
        }
    }
}
