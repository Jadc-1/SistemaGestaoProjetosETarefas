using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaGestaoProjetosETarefas.Domain;
using SistemaGestaoProjetosETarefas.Services;
using System.ComponentModel;

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
                if (tarefas.Count == 0)
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

        public static void TabelaProjetos(List<Projeto> projetos)
        {
            var table = new Table();
            table.AddColumn("[cornflowerblue]ID[/]");
            table.AddColumn("[cornflowerblue]Nome[/]");
            table.AddColumn("[cornflowerblue]Descrição[/]");
            table.AddColumn("[cornflowerblue]Data de Início[/]");
            table.AddColumn("[cornflowerblue]Status[/]");
            table.AddColumn("[cornflowerblue]Prioridade[/]");
            foreach (var projeto in projetos)
            {
                table.AddRow
                    (
                        projeto.CodigoDoProjeto.ToString(),
                        projeto.Nome!,
                        projeto.Desc!,
                        projeto.DataInicio.ToString("dd/MM/yyyy"),
                        projeto.StatusProjeto!.Categoria!,
                        projeto.Prioridade.ToString()
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
            table.AddColumn("[cornflowerblue]Departamento[/]");
            table.AddColumn("[cornflowerblue]Status[/]");
            foreach (var funcionario in funcionarios)
            {
                var departamento = funcionario.Value.Departamento != null ? funcionario.Value.Departamento.NomeDept!.ToString() : "Nenhum departamento atribuído";
                var ativo = funcionario.Value.Ativo.ToString() == "True" ? "[green]Ativo[/]" : "[red]Inativo[/]";
                table.AddRow
                    (
                        funcionario.Value.IdFuncionario.ToString(),
                        funcionario.Value.Nome!,
                        funcionario.Value.Email!,
                        funcionario.Value.Telefone!,
                        funcionario.Value.DataCadastro.ToString("dd/MM/yyyy"),
                        funcionario.Value.Endereco!.ToString()!,
                        departamento,
                        ativo
                    );
            }
            AnsiConsole.Write(table);
        }

        public static void ListarGestores()
        {
            while (true)
            {
                AnsiConsole.Clear();
                AnsiConsole.Write(new Rule("[gold1]Gestores[/]").RuleStyle("grey").LeftJustified());
                if (GestorService.ListarGestores().Count == 0)
                {
                    Console.WriteLine();
                    AnsiConsole.MarkupLine("[red] Nenhum gestor encontrado.[/]");
                    Console.WriteLine();
                    AnsiConsole.Write(new Rule().RuleStyle("grey").Centered());
                    Thread.Sleep(1000);
                    UsuarioMenuView.MenuUsuario();
                }
                Console.WriteLine();
                TabelaGestores();
                Console.WriteLine();
                AnsiConsole.Write(new Rule().RuleStyle("grey").Centered());
                var opcao = AnsiConsole.Prompt
                (
                    new SelectionPrompt<string>()
                    .Title("[gold1] Selecione uma opção: [/]")
                    .AddChoices(new[]
                    {
                    "[cornflowerblue]1 -[/] Listar Projetos de um Gestor",
                    "[red]Voltar[/]"
                    })
                );
                if (opcao == "[cornflowerblue]1 -[/] Listar Projetos de um Gestor")
                {
                    var gestores = GestorService.ListarGestores();
                    var gestorEscolhido = AnsiConsole.Prompt
                    (
                        new SelectionPrompt<string>()
                        .Title("[gold1] Selecione um Gestor: [/]")
                        .AddChoices(gestores.Keys)
                    );
                    var gestor = gestores[gestorEscolhido];
                    var projetos = GestorService.ListarProjetosGestor(gestor);
                    ListarProjetosGestor(projetos);
                }
                else if (opcao == "[red]Voltar[/]")
                {
                    UsuarioMenuView.MenuUsuario();
                }
            }
        }

        public static void TabelaGestores()
        {
            var table = new Table();
            var gestores = GestorService.ListarGestores();
            table.AddColumn("[cornflowerblue]ID[/]");
            table.AddColumn("[cornflowerblue]Nome[/]");
            table.AddColumn("[cornflowerblue]Email[/]");
            table.AddColumn("[cornflowerblue]Telefone[/]");
            table.AddColumn("[cornflowerblue]Data de Admissão[/]");
            table.AddColumn("[cornflowerblue]Endereço[/]");
            table.AddColumn("[cornflowerblue]Status[/]");
            foreach (var gestor in gestores)
            {
                var ativo = gestor.Value.Ativo.ToString() == "True" ? "[green]Ativo[/]" : "[red]Inativo[/]";
                table.AddRow
                    (
                        gestor.Value.IdGestor.ToString(),
                        gestor.Value.Nome!,
                        gestor.Value.Email!,
                        gestor.Value.Telefone!,
                        gestor.Value.DataCadastro.ToString("dd/MM/yyyy"),
                        gestor.Value.Endereco!.ToString()!,
                        ativo
                    );
            }
            AnsiConsole.Write(table);
        }

        public static void ListarProjetosGestor(List<Projeto> projeto)
        {
            while (true)
            {
                AnsiConsole.Clear();
                AnsiConsole.Write(new Rule("[gold1]Projetos do Gestor[/]").RuleStyle("grey").LeftJustified());
                Console.WriteLine();
                if (projeto.Count == 0)
                {
                    AnsiConsole.MarkupLine("[red] Nenhuma tarefa encontrada![/]");
                    Console.WriteLine();
                    AnsiConsole.Write(new Rule().RuleStyle("grey").Centered());
                    Thread.Sleep(1000);
                    ListarGestores();
                }
                else
                {
                    TabelaProjetos(projeto);
                }
                Console.WriteLine();
                AnsiConsole.Write(new Rule().RuleStyle("grey").Centered());
                Console.WriteLine();
                AnsiConsole.Markup("[grey] Pressione qualquer tecla para continuar..[/]");
                Console.ReadKey();
                ListarGestores();
            }
        }

        public static void TabelaUsuario()
        {
            var usuarios = UsuarioService.ListarUsuarios();
            var table = new Table();
            table.AddColumn("[cornflowerblue]ID[/]");
            table.AddColumn("[cornflowerblue]Nome[/]");
            table.AddColumn("[cornflowerblue]Email[/]");
            table.AddColumn("[cornflowerblue]Telefone[/]");
            table.AddColumn("[cornflowerblue]Data de Admissão[/]");
            table.AddColumn("[cornflowerblue]Endereço[/]");
            table.AddColumn("[cornflowerblue]Status[/]");
            table.AddColumn("[cornflowerblue]Tipo[/]");
            foreach (var usuario in usuarios)
            {
                var ativo = usuario.Value.Ativo.ToString() == "True" ? "[green]Ativo[/]" : "[red]Inativo[/]";
                var tipo = usuario.Value is Funcionario ? "[cornflowerblue]Funcionário[/]" : "[cornflowerblue]Gestor[/]";
                table.AddRow
                    (
                        usuario.Value.IdDoUsuario.ToString(),
                        usuario.Value.Nome!,
                        usuario.Value.Email!,
                        usuario.Value.Telefone!,
                        usuario.Value.DataCadastro.ToString("dd/MM/yyyy"),
                        usuario.Value.Endereco!.ToString()!,
                        ativo,
                        tipo
                    );
            }
            AnsiConsole.Write(table);
        }
    }
}
