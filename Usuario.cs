using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestaoProjetosETarefas
{
    public class Usuario
    {
        private static int _proximoId = 1; // Variável estática para gerar IDs únicos
        public int IdDoUsuario { get; set; }
        public string? Nome { get; set; }
        public string? Email { get; set; } 
        public string? Telefone { get; set; }
        public DateTime DataCadastro { get; set; }
        public bool Ativo { get; set; } = true; // Propriedade para indicar se o usuário está ativo ou não


        public virtual void SetUsuario(Usuario usuario)
        {
            IdDoUsuario = _proximoId++;
            this.Nome = usuario.Nome;
            this.Email = usuario.Email;
            this.Telefone = usuario.Telefone;
            this.DataCadastro = usuario.DataCadastro;
        }

        public virtual void ExcluirUsuario(int id) { }

        public virtual void EditarUsuario(Usuario usuario)
        {
            this.Nome = usuario.Nome;
            this.Email = usuario.Email;
            this.Telefone = usuario.Telefone;
            this.DataCadastro = usuario.DataCadastro;
        }

        public override string ToString()
        {
            return $"Id: {IdDoUsuario}, Nome: {Nome}, Email: {Email}, Telefone: {Telefone}, Data de Cadastro: {DataCadastro}";
        }


    }
}
