using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaGestaoProjetosETarefas.Domain;
using SistemaGestaoProjetosETarefas.Services;

namespace SistemaGestaoProjetosETarefas.Views
{
    public class UsuarioListarView
    {
        public static void ListarFuncionarios()
        {
            while (true)
            {
                AnsiConsole.Clear();
                AnsiConsole.Write(new Rule("[gold1]Funcionários[/]").RuleStyle("grey").LeftJustified());
                Console.WriteLine();
                TabelaFuncionarios();
                Console.WriteLine();
                AnsiConsole.Write(new Rule().RuleStyle("grey").Centered());

                var opcao = AnsiConsole.Prompt
                (
                    new SelectionPrompt<string>()
                    .Title("[gold1] Selecione uma opção: [/]")
                    .AddChoices(new[]
                    {
                    "[cornflowerblue]1 -[/] Listar Tarefas de um Funcionário",
                    "[red]Voltar[/]"
                    })
                );
                if (opcao == "[cornflowerblue]1 -[/] Listar Tarefas de um Funcionário")
                {
                    var funcionarios = FuncionarioService.ListarFuncionarios();
                    var funcionarioEscolhido = AnsiConsole.Prompt
                    (
                        new SelectionPrompt<string>()
                        .Title("[gold1] Selecione um Funcionário: [/]")
                        .AddChoices(funcionarios.Keys)
                    );
                    var funcionario = funcionarios[funcionarioEscolhido];
                    var tarefas = FuncionarioService.ListarTarefasFuncionario(funcionario);
                    ListarTarefasFuncionario(tarefas);
                }
                else if (opcao == "[red]Voltar[/]")
                {
                    UsuarioMenuView.MenuUsuario();
                }
            }

        }

        private static void ListarTarefasFuncionario(List<Tarefa> tarefas)
        {
            while (true)
            {
                AnsiConsole.Clear();
                AnsiConsole.Write(new Rule("[gold1]Tarefas do Funcionário[/]").RuleStyle("grey").LeftJustified());
                Console.WriteLine();
                if(tarefas.Count == 0)
                {
                    AnsiConsole.MarkupLine("[red] Nenhuma tarefa encontrada![/]");
                    Console.WriteLine();
                    AnsiConsole.Write(new Rule().RuleStyle("grey").Centered());
                    Thread.Sleep(1000);
                    ListarFuncionarios();
                }
                else
                {
                    TabelaTarefas(tarefas);
                }
                Console.WriteLine();
                AnsiConsole.Write(new Rule().RuleStyle("grey").Centered());
                Console.WriteLine();
                AnsiConsole.Markup("[grey] Pressione qualquer tecla para continuar..[/]");
                Console.ReadKey();
                ListarFuncionarios();
            }
        }

        public static void TabelaTarefas(List<Tarefa> tarefas)
        {
            var table = new Table();
            table.AddColumn("[cornflowerblue]ID[/]");
            table.AddColumn("[cornflowerblue]Nome[/]");
            table.AddColumn("[cornflowerblue]Descrição[/]");
            table.AddColumn("[cornflowerblue]Data de Início[/]");
            table.AddColumn("[cornflowerblue]Data de Término[/]");
            table.AddColumn("[cornflowerblue]Status[/]");
            table.AddColumn("[cornflowerblue]Prioridade[/]");

            foreach (var tarefa in tarefas)
            {
                table.AddRow
                    (
                        tarefa.IdTarefa.ToString(),
                        tarefa.NomeTarefa!,
                        tarefa.DescricaoTarefa!,
                        tarefa.DataInicio.ToString("dd/MM/yyyy"),
                        tarefa.StatusTarefa!.Categoria!,
                        tarefa.Prioridade.ToString()
                    );
            }
           
            AnsiConsole.Write(table);
        }


        public static void TabelaFuncionarios()
        {
            var table = new Table();
            var funcionarios = FuncionarioService.ListarFuncionarios();
            if (funcionarios.Count == 0)
            {
                AnsiConsole.Markup("[red] Nenhum funcionário encontrado.[/]");
                Console.WriteLine();
                Console.WriteLine();
                AnsiConsole.Write(new Rule().RuleStyle("grey").Centered());
                Thread.Sleep(1000);
                UsuarioMenuView.MenuUsuario();
            }
            table.AddColumn("[cornflowerblue]ID[/]");
            table.AddColumn("[cornflowerblue]Nome[/]");
            table.AddColumn("[cornflowerblue]Email[/]");
            table.AddColumn("[cornflowerblue]Telefone[/]");
            table.AddColumn("[cornflowerblue]Data de Admissão[/]");
            table.AddColumn("[cornflowerblue]Endereço[/]");
            table.AddColumn("[cornflowerblue]Status[/]");
            foreach (var funcionario in funcionarios)
            {
                var ativo = funcionario.Value.Ativo.ToString() == "True" ? "[green]Ativo[/]" : "[red]Inativo[/]";
                table.AddRow
                    (
                        funcionario.Value.IdFuncionario.ToString(),
                        funcionario.Value.Nome!,
                        funcionario.Value.Email!,
                        funcionario.Value.Telefone!,
                        funcionario.Value.DataCadastro.ToString("dd/MM/yyyy"),
                        funcionario.Value.Endereco!.ToString(),
                        ativo
                    );
            }
            AnsiConsole.Write(table);
        }
    }
}
