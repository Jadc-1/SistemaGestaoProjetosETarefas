using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestaoProjetosETarefas.Domain
{
    public class Endereco
    {
        public string Rua { get; set; }
        public string Numero { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }

        public Endereco(string rua, string numero, string cidade, string estado) 
        {
            this.Rua = rua;
            this.Numero = numero;
            this.Cidade = cidade;
            this.Estado = estado;
        }
    }
}
