using SistemaGestaoProjetosETarefas.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestaoProjetosETarefas.Domain
{
    public class Gestor : Usuario
    {
        private static int _IdIncremento = 0;
        public int IdGestor { get; private set; }
        public List<Projeto> ProjetosGerenciados { get; set; }

        public Gestor(string name, string email, string telefone, DateTime dataCadastro, Endereco endereco) : base(name, email, telefone, dataCadastro, endereco)
        {
            IdGestor = _IdIncremento++;
            ProjetosGerenciados = new List<Projeto>();
        }
    }
}
