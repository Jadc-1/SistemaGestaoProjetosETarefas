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
    public class DepartamentoListagemView
    {
        public static void ListarDepartamentos()
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
                        case "[red]Voltar[/]": DepartamentoMenuView.MenuDepartamentos(); break;
                    }
                }
                else
                {
                    AnsiConsole.Markup("[grey] Pressiona qualquer tecla para continuar..[/]");
                    Console.ReadKey();
                    DepartamentoMenuView.MenuDepartamentos();
                }
            }
        }

        public static void TabelaDepartamentos()
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

        public static void ListarFuncionarioDept()
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
                    DepartamentoMenuView.MenuDepartamentos();
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

        public static void TabelaFuncionarioDept(Departamento departamento)
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

    }
}
