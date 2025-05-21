using SistemaGestaoProjetosETarefas.Service;
using SistemaGestaoProjetosETarefas.Domain;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SistemaGestaoProjetosETarefas.Views
{
    public class DepartamentoView
    {
        public static void MenuDepartamentos()
        {
            AnsiConsole.Clear();
            AnsiConsole.Write(new Rule("[gold1 bold]Gerenciar Departamentos[/]").RuleStyle("grey").Centered());
            Console.WriteLine();
            var opcao = AnsiConsole.Prompt
                (
                    new SelectionPrompt<string>()
                    .Title("[gold1] Selecione uma opção: [/]")
                    .AddChoices(new[]
                    {
                        "[cornflowerblue]1- [/]Listar Departamentos",
                        "[cornflowerblue]2- [/]Adicionar Departamento",
                        "[cornflowerblue]3- [/]Editar Departamento",
                        "[cornflowerblue]4- [/]Desativar Departamento",
                        "[red]Voltar[/]"
                    })
                );
            switch (opcao)
            {
                case "[cornflowerblue]1- [/]Listar Departamentos": ListarDepartamentos(); break;
                case "[cornflowerblue]2- [/]Adicionar Departamento": AdicionarDepartamento(); break;
                case "[cornflowerblue]3- [/]Editar Departamento": EditarDepartamento(); break;
                case "[cornflowerblue]4- [/]Desativar Departamento": DesativarDepartamento(); break;
                case "[red]Voltar[/]": MenuView.MenuPrincipal(); break;
            }

            Console.ReadKey();
        }

        private static void ListarDepartamentos()
        {
            while (true)
            {
                AnsiConsole.Clear();
                DepartamentoService departamentoService = new DepartamentoService();
                var departamentos = departamentoService.ListarDepartamentos();
                AnsiConsole.Write(new Rule("[gold1 bold]Listar Departamentos[/]").RuleStyle("grey").Centered());
                Console.WriteLine();
                TabelaDepartamentos();
                Console.WriteLine();
                AnsiConsole.Write(new Rule().RuleStyle("grey").Centered());
                Console.WriteLine();
                if (departamentos.Count > 0)
                {
                    var opcao = AnsiConsole.Prompt
                    (
                        new SelectionPrompt<string>()
                        .Title("[gold1] Selecione uma opção: [/]")
                        .AddChoices(new[]
                        {
                            "[cornflowerblue]1- Listar Funcionários do Departamento[/]",
                            "[red]Voltar[/]"
                        })
                    );
                    switch (opcao)
                    {
                        case "[cornflowerblue]1- Listar Funcionários do Departamento[/]": ListarFuncionarioDept(); break;
                        case "[red]Voltar[/]": MenuDepartamentos(); break;
                    }
                }
                else
                {
                    AnsiConsole.Markup("[grey] Pressiona qualquer tecla para continuar..[/]");
                    Console.ReadKey();
                    MenuDepartamentos();
                }
            }
        }

        private static void TabelaDepartamentos()
        {
            DepartamentoService departamentoService = new DepartamentoService();
            var departamentos = departamentoService.ListarDepartamentos();
            if (departamentos.Count == 0)
            {
                AnsiConsole.MarkupLine("[red] Nenhum departamento encontrado.[/]");
                Console.WriteLine();
                Console.WriteLine();
            }
            else
            {
                var table = new Table();
                table.AddColumn("[cadetblue]ID[/]");
                table.AddColumn("[cadetblue]Nome[/]");
                table.AddColumn("[cadetblue]Ativo[/]");
                table.AddColumn("[cadetblue]Funcionários[/]");
                foreach (var departamento in departamentos)
                {
                    table.AddRow(
                        departamento.Value.IdDept.ToString(), // Pego o value do dicionario que é um objeto do tipo Departamento e seleciono o ID
                        departamento.Value.NomeDept!,
                        departamento.Value.Ativo.ToString(),
                        departamento.Value.Funcionarios!.Count.ToString()
                        );
                }
                table.BorderColor(Color.CadetBlue);


                AnsiConsole.Write(table);
            }

        }

        private static void ListarFuncionarioDept()
        {
            while (true)
            {
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
                    MenuDepartamentos();
                }
                else
                {
                    var departamentoSelecionado = departamentos[opcao];
                    if (departamentoSelecionado.Funcionarios!.Count > 0)
                    {
                        TabelaFuncionarioDept(departamentoSelecionado);
                        ListarDepartamentos();
                    }
                    else
                    {
                        AnsiConsole.Clear();
                        AnsiConsole.Write(new Rule($"[gold1 bold]Funcionários do departamento {departamentoSelecionado.NomeDept}[/]").RuleStyle("grey").Centered());
                        Console.WriteLine();
                        AnsiConsole.MarkupLine("[red] Nenhum funcionário encontrado!.[/]");
                        Console.WriteLine();
                        AnsiConsole.Write(new Rule().RuleStyle("grey").Centered());
                        Thread.Sleep(1500); Console.WriteLine();
                        break;
                    }
                }
            }
        }

        private static void TabelaFuncionarioDept(Departamento departamento)
        {

            while (true)
            {
                AnsiConsole.Clear();
                AnsiConsole.Write(new Rule($"[gold1 bold]Departamento {departamento.NomeDept}[/]").RuleStyle("grey").Centered());
                Console.WriteLine();
                var table = new Table();
                table.AddColumn("[cadetblue]ID[/]");
                table.AddColumn("[cadetblue]Nome[/]");
                table.AddColumn("[cadetblue]Email[/]");
                table.AddColumn("[cadetblue]Telefone[/]");
                table.AddColumn("[cadetblue]Data de Admissão[/]");
                foreach (Funcionario funcionario in departamento.Funcionarios!)
                {
                    table.AddRow
                        (
                            funcionario.IdFuncionario.ToString(),
                            funcionario.Nome!,
                            funcionario.Email!,
                            funcionario.Telefone!,
                            funcionario.DataCadastro.ToString("dd/MM/yyyy")
                        );
                }
                AnsiConsole.Write(table);
                Console.WriteLine();
                AnsiConsole.Write(new Rule().RuleStyle("grey").Centered());
                Console.WriteLine();
                AnsiConsole.Markup("[grey] Pressione qualquer tecla para voltar... [/]");
                Console.ReadKey();
                break;
            }


        }

        private static void AdicionarDepartamento()
        {
            while (true)
            {
                AnsiConsole.Clear();
                AnsiConsole.Write(new Rule("[gold1 bold]Adicionar Departamento[/]").RuleStyle("grey").Centered());
                Console.WriteLine();
                var nome = AnsiConsole.Ask<string>("[cornflowerblue] Digite o nome do departamento: [/]");
                var departamento = new Departamento(nome);
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
                    MenuDepartamentos();
                }
                else
                {
                    DepartamentoService departamentoService = new DepartamentoService();
                    departamentoService.AdicionarDepartamento(departamento);
                }
                AnsiConsole.MarkupLine($"[green] Departamento {nome} adicionado com sucesso![/]");
                Thread.Sleep(500);
                MenuDepartamentos();
            }
        }

        private static void EditarDepartamento()
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
                    MenuDepartamentos();
                }
                else
                {
                    var departamentoSelecionado = departamentos[opcao];
                    var novoNome = AnsiConsole.Ask<string>("[cornflowerblue] Digite o novo nome do departamento:[/]");
                    departamentoSelecionado.NomeDept = novoNome;
                    Console.WriteLine();
                    AnsiConsole.MarkupLine($"[green] Departamento editado com sucesso![/]");
                    Thread.Sleep(500);
                    MenuDepartamentos();
                }
            }
        }

        private static void DesativarDepartamento()
        {
            while(true)
            {
                AnsiConsole.Clear();
                var departamentos = new DepartamentoService().ListarDepartamentos();
                AnsiConsole.Write(new Rule("Desativar Departamento").RuleStyle("grey").Centered());
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
                if(opcao == "[red]Voltar[/]")
                {
                    MenuDepartamentos();
                }
                else
                {
                    var departamentoEscolhido = departamentos[opcao];
                    var confirmar = AnsiConsole.Prompt
                        (
                            new SelectionPrompt<string>()
                            .Title("Você tem certeza que deseja desativar o departamento?")
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
                        MenuDepartamentos();
                    }
                    else
                    {
                        AnsiConsole.MarkupLine("[red] Ação cancelada![/]");
                        Thread.Sleep(700);
                        MenuDepartamentos();
                    }
                }
            }
        }
    }
}
