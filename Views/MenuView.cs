using SistemaGestaoProjetosETarefas.Domain;
using SistemaGestaoProjetosETarefas.Service;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SistemaGestaoProjetosETarefas.Views
{
    public class MenuView
    {
        public static void MenuPrincipal()
        {
            Departamento departamento = new Departamento("Desenvolvimento");
            Endereco endereco = new Endereco("Rua Treze de Maio","141", "Vinhedo", "SP");
            Funcionario joao = new Funcionario("João", "joaoale.25@gmail.com", "123456789", DateTime.Now, endereco);
            departamento.AdicionarFuncionario(joao);
            Projeto projeto = new Projeto("teste", "teste1", DateTime.Now, Domain.Status.Atrasado, 'C');
            ProjetoService projetoService = new ProjetoService();
            Tarefa tarefa = new Tarefa("Tarefa 1", "Descricao da tarefa 1fffffffffffffffffffffffffffffffffffffffffffffffff", DateTime.Now, Domain.Status.Atrasado, 'C');
            projeto.AdicionarTarefa(tarefa);
            tarefa.AtribuirFuncionario(joao);
            Tarefa tarefa1 = new Tarefa("Tarefa 2", "Descricao da tarefa 2", DateTime.Now, Domain.Status.Atrasado, 'C');
            projeto.AdicionarTarefa(tarefa1);
            projetoService.AdicionarProjeto(projeto);
            Projeto projeto1 = new Projeto("teste2", "teste3", DateTime.Now, Domain.Status.Atrasado, 'C');
            projetoService.AdicionarProjeto(projeto1);
            string opcao;
            do
            {
                AnsiConsole.Clear();
                AnsiConsole.Write(new Rule("[gold1 bold]Sistema de Gestão de Projetos e Tarefas[/]").RuleStyle("grey").Centered());
                Console.WriteLine();
                opcao = AnsiConsole.Prompt
                    (
                        new SelectionPrompt<string>()
                        .Title("[gold1] Selecione uma opção: [/]")
                        .AddChoices(new[]
                        {
                            "Gerenciar Projetos",
                            "Gerenciar Tarefas",
                            "Gerenciar Departamentos",
                            "Gerenciar Usuários",
                            "Relatórios",
                            "Sair"
                        })
                    );

                switch (opcao)
                { // Chamar os métodos para gerencia-los
                    case "Gerenciar Projetos": ProjetoView.MenuProjetos(); break;
                    case "Gerenciar Tarefas": Console.WriteLine("Tarefas"); break;
                    case "Gerenciar Departamentos": Console.WriteLine("Departamentos"); break;
                    case "Gerenciar Usuários": Console.WriteLine("Usuarios"); break;
                    case "Relatórios": Console.WriteLine("Relatorio"); break;
                    case "Sair":
                        {
                            AnsiConsole.Clear();
                            AnsiConsole.MarkupLine("[red]Saindo do sistema...[/]");
                            Thread.Sleep(1500);
                            break;
                        }    
                }

            } while (opcao != "Sair");
        }
    }
}
