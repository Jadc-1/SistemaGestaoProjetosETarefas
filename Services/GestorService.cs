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
        public void ListarGestores()
        {
            if (gestores != null)
            {
                foreach (var gestor in gestores)
                {
                    if (gestor.Ativo != false)
                    {
                        Console.WriteLine($"ID: {gestor.IdGestor} \nNome: {gestor.Nome} \nEmail: {gestor.Email} \nTelefone: {gestor.Telefone} \nData de Cadastro: {gestor.DataCadastro.ToString("dd/MM/yyyy")}\n");
                        Console.WriteLine("(Projetos Gerenciados)");
                        if (gestor.ProjetosGerenciados != null)
                        {
                            foreach (var projeto in gestor.ProjetosGerenciados)
                            {
                                Console.WriteLine($" {projeto.CodigoDoProjeto}- Projeto: {projeto.Nome}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Nenhum projeto associado");
                        }
                    }
                }
            }
        }
    }
}
