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
    public class DeptCrudView
    {
        public static void AdicionarDepartamento()
        {
            while (true)
            {
                AnsiConsole.Clear();
                AnsiConsole.Write(new Rule("[gold1 bold]Adicionar Departamento[/]").RuleStyle("grey").Centered());
                Console.WriteLine();
                var nome = AnsiConsole.Ask<string>("[cornflowerblue] Digite o nome do departamento: [/]");
                var nomeFormatado = nome.Replace("[", "").Replace("]", "").Trim(); // Remove colchetes e espaços desnecessários
                var departamento = new Departamento(nomeFormatado);
                Console.WriteLine();
                var confirmar = AnsiConsole.Prompt
                    (
                        new SelectionPrompt<string>()
                        .Title($"[cadetblue] Você tem certeza que deseja adicionar o departamento {nome}? [/]")
                        .AddChoices(new[] { "[green]Sim[/]", "[red]Não[/]" })
                    );
                if (confirmar == "[red]Não[/]")
                {
                    AnsiConsole.MarkupLine("[red] Ação cancelada![/]");
                    Thread.Sleep(700);
                    DeptMenuView.MenuDepartamentos();
                }
                else
                {
                    DepartamentoService departamentoService = new DepartamentoService();
                    departamentoService.AdicionarDepartamento(departamento);
                }
                AnsiConsole.MarkupLine($"[green] Departamento {nome} adicionado com sucesso![/]");
                Thread.Sleep(500);
                DeptMenuView.MenuDepartamentos();
            }
        }

        public static void EditarDepartamento()
        {
            while (true)
            {
                AnsiConsole.Clear();
                AnsiConsole.Write(new Rule("[gold1 bold]Editar Departamento[/]").RuleStyle("grey").Centered());
                Console.WriteLine();
                DepartamentoService departamentoService = new DepartamentoService();
                var departamentos = departamentoService.ListarDepartamentos();
                var opcao = AnsiConsole.Prompt
                    (
                        new SelectionPrompt<string>()
                        .Title("[gold1] Selecione o departamento:[/]")
                        .AddChoices(departamentos.Keys)
                        .AddChoices(new[]
                        {
                            "[red]Voltar[/]"
                        })
                    );
                if (opcao == "[red]Voltar[/]")
                {
                    DeptMenuView.MenuDepartamentos();
                }
                else
                {
                    var departamentoSelecionado = departamentos[opcao];
                    var novoNome = AnsiConsole.Ask<string>("[cornflowerblue] Digite o novo nome do departamento:[/]");
                    var novoNomeFormatado = novoNome.Replace("[", "").Replace("]", "").Trim(); // Remove colchetes e espaços desnecessários
                    departamentoSelecionado.NomeDept = novoNomeFormatado;
                    Console.WriteLine();
                    AnsiConsole.MarkupLine($"[green] Departamento editado com sucesso![/]");
                    Thread.Sleep(500);
                    DeptMenuView.MenuDepartamentos();
                }
            }
        }

        public static void DesativarDepartamento()
        {
            while (true)
            {
                AnsiConsole.Clear();
                var departamentos = new DepartamentoService().ListarDepartamentos();
                AnsiConsole.Write(new Rule("[gold1]Desativar Departamento[/]").RuleStyle("grey").Centered());
                var opcao = AnsiConsole.Prompt
                    (
                        new SelectionPrompt<string>()
                        .Title("[gold1] Selecione o departamento:[/]")
                        .AddChoices(departamentos.Keys)
                        .AddChoices(new[]
                        {
                            "[red]Voltar[/]"
                        })
                    );
                if (opcao == "[red]Voltar[/]")
                {
                    DeptMenuView.MenuDepartamentos();
                }
                else
                {
                    var departamentoEscolhido = departamentos[opcao];
                    var confirmar = AnsiConsole.Prompt
                        (
                            new SelectionPrompt<string>()
                            .Title("[gold1] Você tem certeza que deseja desativar o departamento?[/]")
                            .AddChoices(new[]
                            {
                                "[green]Sim[/]",
                                "[red]Não[/]"
                            })
                        );
                    if (confirmar == "[green]Sim[/]")
                    {
                        departamentoEscolhido.Ativo = false;
                        AnsiConsole.MarkupLine($"[green] Departamento {departamentoEscolhido.NomeDept} desativado com sucesso![/]");
                        Thread.Sleep(500);
                        DeptMenuView.MenuDepartamentos();
                    }
                    else
                    {
                        AnsiConsole.MarkupLine("[red] Ação cancelada![/]");
                        Thread.Sleep(700);
                        DeptMenuView.MenuDepartamentos();
                    }
                }
            }
        }
    }
}
