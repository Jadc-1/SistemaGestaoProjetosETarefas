using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaGestaoProjetosETarefas.Domain;

namespace SistemaGestaoProjetosETarefas.Services
{
    public class FuncionarioService
    {
        private static readonly List<Funcionario> funcionarios = new List<Funcionario>();
        
        public void AdicionarFuncionario(Funcionario funcionario)
        {
            funcionarios.Add(funcionario);
        }
        public void DesativarFuncionario(int id)
        {
            var funcionario = funcionarios.FirstOrDefault(f => f.IdFuncionario == id);
            if (funcionario != null)
            {
                funcionario.Desativar(funcionario);
            }
            else
            {
                Console.WriteLine("Funcionário não encontrado");
            }
        }
        public void ListarFuncionario()
        {
            if (funcionarios != null)
            {
                foreach(var funcionario in funcionarios)
                {
                    if(funcionario.Ativo != false)
                    {
                        Console.WriteLine($"ID: {funcionario.IdFuncionario} - Nome: {funcionario.Nome} - Email: {funcionario.Email} - Telefone: {funcionario.Telefone} - Data de Cadastro: {funcionario.DataCadastro.ToString("dd/MM/yyyy")}");
                        if (funcionario.Departamento != null)
                        {
                            Console.WriteLine($"Departamento: {funcionario.Departamento.NomeDept}");
                        }
                        else
                        {
                            Console.WriteLine("Departamento: Nenhum departamento associado");
                        }
                    }
                }
            }
        }
    }
}
