using SistemaGestaoProjetosETarefas.Domain;
using SistemaGestaoProjetosETarefas.Service;
using SistemaGestaoProjetosETarefas.Services;
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
        public static async Task MenuPrincipal()
        {
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
                            " Gerenciar Projetos",
                            " Gerenciar Departamentos",
                            " Gerenciar Usuários",
                            " Relatórios",
                            "[red] Sair[/]"
                        })
                    );

                switch (opcao)
                { // Chamar os métodos para gerencia-los
                    case " Gerenciar Projetos": ProjetoMenuView.MenuProjetos(); break;
                    case " Gerenciar Departamentos": DeptMenuView.MenuDepartamentos(); break;
                    case " Gerenciar Usuários": UsuarioMenuView.MenuUsuario(); break;
                    case " Relatórios": await RelatorioView.MenuRelatorio(); break;
                    case "[red] Sair[/]":
                        {
                            AnsiConsole.Clear();
                            AnsiConsole.MarkupLine("[red] Saindo do sistema...[/]");
                            Thread.Sleep(1500);
                            System.Environment.Exit(0);
                            break;
                        }    
                }

            } while (opcao != "[red]Sair[/]");
        }
    }
}
