using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestaoProjetosETarefas.Domain
{
    public class Usuario
    {
        private static int _proximoId = 1; // Variável estática para gerar IDs únicos
        public int IdDoUsuario { get; private set; }
        public string? Nome { get; set; }
        public string? Email { get; set; } 
        public string? Telefone { get; set; }
        public DateTime DataCadastro { get; set; }
        public Endereco? Endereco { get; set; } // Propriedade para armazenar o endereço do usuário
        public bool Ativo { get; set; } = true; // Propriedade para indicar se o usuário está ativo ou não
        

        public Usuario (string nome, string email, string telefone, DateTime dataCadastro, Endereco? endereco)
        {
            IdDoUsuario = _proximoId++;
            this.Nome = nome;
            this.Email = email;
            this.Telefone = telefone;
            this.DataCadastro = dataCadastro;
            this.Endereco = endereco;
        }

        public void Desativar(Usuario usuario) 
        {
            usuario.Ativo = false;
        }

        public virtual void Editar(string nome, string email, string telefone, DateTime dataCadastro)
        {
            this.Nome = nome;
            this.Email = email;
            this.Telefone = telefone;
            this.DataCadastro = dataCadastro;
        }

        public override string ToString()
        {
            return $"Id: {IdDoUsuario}, Nome: {Nome}, Email: {Email}, Telefone: {Telefone}, Data de Cadastro: {DataCadastro}" +
                $"Endereço: {Endereco}";
        }


    }
}
