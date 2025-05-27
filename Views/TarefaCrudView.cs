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
    public class TarefaCrudView
    {

        public static async Task MenuAdicionarTarefa(Projeto projeto)
        {
            AnsiConsole.Clear();
            AnsiConsole.Write(new Rule($"[gold1]Adicicio Tarefas - Projeto {projeto.Nome}[/]"));
            Console.WriteLine();
            var opcao = AnsiConsole.Prompt
                (
                    new SelectionPrompt<string>()
                    .Title("[gold1] Como você deseja adicionar tarefas? [/] ")
                    .AddChoices(new[]
                    {
                        "[cornflowerblue]1-[/] Adicionar tarefa manualmente",
                        "[cornflowerblue]2-[/] Adicionar tarefas com IA",
                        "[red]Voltar[/]"
                    })
                );
            switch (opcao)
            {
                case "[cornflowerblue]1-[/] Adicionar tarefa manualmente": AdicionarNovaTarefa(projeto); break;
                case "[cornflowerblue]2-[/] Adicionar tarefas com IA": await AdicionarTarefasComIA(projeto); break;
                case "[red]Voltar[/]": ProjetoMenuView.ExibirProjeto(projeto); break;
            }

        }

        public static async Task AdicionarTarefasComIA(Projeto projeto)
        {
            while (true)
            {
                Console.Clear();
                AnsiConsole.Write(new Rule($"[gold1]Adicionar tarefas com IA - Projeto {projeto.Nome}[/]"));
                Console.WriteLine();
                var tarefasGeradas = await AnsiConsole.Status()
                    .Spinner(Spinner.Known.Dots)
                    .StartAsync(" Adicionando tarefas com IA...", async ctx =>
                    {
                        ctx.SpinnerStyle("green");
                        return await TarefasIAService.CriarTarefasIAAsync(projeto);
                    });

                foreach (var tarefa in tarefasGeradas)
                {
                    tarefa.StatusTarefa = Domain.Status.Pendente;
                    tarefa.DataInicio = DateTime.Now;
                    projeto.AdicionarTarefa(tarefa);
                }
                AnsiConsole.MarkupLine($"[green] Tarefas adicionadas com sucesso![/]"); Console.WriteLine("\n");
                AnsiConsole.Markup("[grey] Pressione qualquer tecla para continuar...[/]");
                Console.ReadKey();
                break;
            }

        }

        public static void AdicionarNovaTarefa(Projeto projeto)
        {
            while (true)
            {
                AnsiConsole.Clear();
                var titulo = new Panel(new Text($"Adicionar tarefas - Projeto: {projeto.Nome}", new Style(Color.Gold1)).Centered()).Border(BoxBorder.Heavy);
                titulo.Expand();
                AnsiConsole.Write(titulo);
                Console.WriteLine();
                AnsiConsole.Write(new Rule().RuleStyle("grey").LeftJustified());
                Console.WriteLine();

                var nome = AnsiConsole.Ask<string>(($"[cornflowerblue] Nome da Tarefa: [/] "));
                var nomeFormatado = nome.Replace("[", "").Replace("]", "").Trim(); // Remove colchetes e espaços desnecessários
                Console.WriteLine();
                var descricao = AnsiConsole.Ask<string>(($"[cornflowerblue] Descrição da Tarefa: [/] ")); //descrição poderá ser nula ou vazia
                var descricaoFormatada = descricao.Replace("[", "").Replace("]", "").Trim(); // Remove colchetes e espaços desnecessários
                Console.WriteLine();
                var dataInicio = DateTime.Now;

                var listastatus = StatusService.ListarStatus(); // Chama o método para exibir todos os status cadastrados

                var status = AnsiConsole.Prompt
                    (
                        new SelectionPrompt<string>()
                        .Title("[cornflowerblue] Status da Tarefa: [/] ")
                        .AddChoices(listastatus.Keys)

                    );

                var statusEscolhido = listastatus[status]; // A variável status recebe o valor do dicionário, onde a chave é o status escolhido
                AnsiConsole.MarkupLine($" Você selecionou o status [cornflowerblue]{statusEscolhido.Categoria}[/]");
                Console.WriteLine();
                var prioridade = AnsiConsole.Prompt
                    (
                        new SelectionPrompt<string>()
                        .Title("[cornflowerblue] Prioridade da Tarefa: [/] ")
                        .AddChoices(new[]
                        {
                        "[cornflowerblue]1-[/] A",
                        "[cornflowerblue]2-[/] B",
                        "[cornflowerblue]3-[/] C",
                        "[cornflowerblue]4-[/] D"
                        })
                    );
                var prioridadeEscolhida = char.Parse(prioridade.Substring(prioridade.Length - 1, 1)); // Pego apenas a letra da prioridade escolhida
                AnsiConsole.MarkupLine($" Você selecionou a prioridade [cornflowerblue]{prioridadeEscolhida}[/]");
                Console.WriteLine("\n");
                AnsiConsole.Write(new Rule().RuleStyle("grey").LeftJustified());
                var adicionar = AnsiConsole.Prompt
                    (
                        new SelectionPrompt<string>()
                        .Title("[gold1] Adicionar tarefa?[/]")
                        .AddChoices(new[]
                        {
                        "[green] Confirmar[/]", "[red] Cancelar[/]"
                        })
                    );
                if (adicionar == "[green] Confirmar[/]")
                {
                    Tarefa tarefa = new Tarefa(nomeFormatado, descricaoFormatada, dataInicio, statusEscolhido, prioridadeEscolhida);
                    projeto.AdicionarTarefa(tarefa); // Chama o método para adicionar a tarefa ao projeto
                    AnsiConsole.MarkupLine($"[green] Tarefa {tarefa.NomeTarefa} Adicionada com sucesso![/]"); Console.WriteLine("\n");
                    Thread.Sleep(1000);
                    ProjetoMenuView.ExibirProjeto(projeto);
                    break;
                }
                else
                {
                    AnsiConsole.MarkupLine($"[red] Tarefa não adicionada![/]"); Console.WriteLine("\n");
                    Thread.Sleep(1000);
                    ProjetoMenuView.ExibirProjeto(projeto);
                    break;
                }
            }
        }

        public static void RemoverTarefa(Projeto projeto)
        {
            while (true)
            {
                var tarefas = ProjetoService.ListarTarefasDoProjeto(projeto);
                AnsiConsole.Clear();
                var titulo = new Panel(new Text($"Remover tarefas - Projeto: {projeto.Nome}", new Style(Color.Gold1)).Centered()).Border(BoxBorder.Heavy);
                titulo.Expand();
                AnsiConsole.Write(titulo);
                Console.WriteLine();
                AnsiConsole.Write(new Rule().RuleStyle("grey").LeftJustified());
                Console.WriteLine();
                if (tarefas.Count == 0)
                {
                    AnsiConsole.MarkupLine("[red]Nenhuma tarefa cadastrada para este projeto.[/]");
                    Thread.Sleep(1000);
                    return;
                }

                var voltar = "[red]Voltar[/]";
                var escolha = AnsiConsole.Prompt
                    (
                        new SelectionPrompt<string>()
                        .Title("[gold3] Selecione a tarefa a ser excluída: [/]")
                        .AddChoices(tarefas)
                        .AddChoices(voltar)
                    );
                if (escolha == voltar) ProjetoMenuView.ExibirProjeto(projeto); // Se o usuário escolher voltar, sai do método
                var confirmacao = AnsiConsole.Prompt
                    (
                        new SelectionPrompt<string>()
                        .Title($"[gold1] Deseja realmente remover a tarefa {escolha}?[/]")
                        .AddChoices(new[]
                        {
                        "[green] Confirmar[/]", "[red] Cancelar[/]"
                        })
                    );
                if (confirmacao == "[red] Cancelar[/]")
                {
                    AnsiConsole.MarkupLine($"[red] Operação de remoção cancelada![/]"); Console.WriteLine("\n");
                    Thread.Sleep(1000);
                    ProjetoMenuView.ExibirProjeto(projeto);
                    break;
                }
                else
                {
                    var tarefa = projeto.Tarefas!.FirstOrDefault(t => t.NomeTarefa == escolha);
                    projeto.RemoverTarefa(tarefa!);
                    AnsiConsole.MarkupLine($"[green] Tarefa {tarefa!.NomeTarefa} Removida com sucesso![/]"); Console.WriteLine("\n");
                    Thread.Sleep(1000);
                    ProjetoMenuView.ExibirProjeto(projeto);
                    break;
                }

            }
        }

        public static void EscolherTarefaExistente(Projeto projeto)
        {
            while (true)
            {
                AnsiConsole.Clear();
                var titulo = new Panel(new Text($"Editar tarefas - Projeto: {projeto.Nome}", new Style(Color.Gold1)).Centered()).Border(BoxBorder.Heavy);
                titulo.Expand();
                AnsiConsole.Write(titulo);
                Console.WriteLine();
                AnsiConsole.Write(new Rule().RuleStyle("grey").LeftJustified());
                Console.WriteLine();

                TarefaTabelaView.CriarTabelaTarefas(projeto);

                AnsiConsole.Write(new Rule().RuleStyle("grey").LeftJustified());

                var tarefas = ProjetoService.ListarTarefasDoProjeto(projeto);
                var opcao = AnsiConsole.Prompt
                    (
                        new SelectionPrompt<string>()
                        .Title("[gold1] Selecione a tarefa a ser editada: [/]")
                        .AddChoices(tarefas)
                        .AddChoices("[red]Voltar[/]")
                    );

                if (opcao == "[red]Voltar[/]")
                {
                    ProjetoMenuView.ExibirProjeto(projeto);
                    break; // Se o usuário escolher voltar, sai do loop
                }

                var tarefa = projeto.Tarefas!.FirstOrDefault(t => t.NomeTarefa == opcao);

                EditarTarefaExistente(tarefa!, projeto); // Chama o método para editar a tarefa escolhida

            }
        }

        public static void EditarTarefaExistente(Tarefa tarefa, Projeto projeto)
        {
            var editarEscolhido = AnsiConsole.Prompt
                   (
                       new SelectionPrompt<string>()
                       .Title("[gold1] O que deseja editar? [/]")
                       .AddChoices(new[]
                       {
                            "[cornflowerblue]1-[/] Nome",
                            "[cornflowerblue]2-[/] Descrição",
                            "[cornflowerblue]3-[/] Status",
                            "[cornflowerblue]4-[/] Prioridade",
                            "[cornflowerblue]5-[/] Funcionário delegado",
                            "[cornflowerblue]6-[/] [red]Voltar[/]"
                       })
                   );

            switch (editarEscolhido)
            {
                case "[cornflowerblue]1-[/] Nome": EditarNome(tarefa); break;
                case "[cornflowerblue]2-[/] Descrição": EditarDesc(tarefa); break;
                case "[cornflowerblue]3-[/] Status": EditarStatus(tarefa); break;
                case "[cornflowerblue]4-[/] Prioridade": EditarPrioridade(tarefa); break;
                case "[cornflowerblue]5-[/] Funcionário delegado":
                    {
                        var listaFunc = FuncionarioService.ListarFuncionarios();
                        if (listaFunc.Count != 0)
                        {
                            EditarFuncionario(tarefa, listaFunc);
                            break;
                        }
                        else
                        {
                            AnsiConsole.MarkupLine("[red] Nenhum funcionário cadastrado![/]");
                            Thread.Sleep(1000);
                            break;
                        }
                    }
                case "[cornflowerblue]6-[/] [red]Voltar[/]": EscolherTarefaExistente(projeto); break;
            }

        }

        private static void EditarNome(Tarefa tarefa)
        {
            var novoNome = AnsiConsole.Ask<string>($"[cornflowerblue] Novo nome da tarefa: [/] ");
            var novoNomeFormatado = novoNome.Replace("]", "").Replace("[", "").Trim();
            tarefa!.NomeTarefa = novoNomeFormatado;
            AnsiConsole.MarkupLine($"[green] Nome da tarefa alterado com sucesso![/]"); Console.WriteLine("\n");
            Thread.Sleep(1000);
        }

        private static void EditarDesc(Tarefa tarefa)
        {
            var novaDescricao = AnsiConsole.Ask<string>("[cornflowerblue] Nova descrição da tarefa: [/]");
            var novaDescricaoFormatada = novaDescricao.Replace("]", "").Replace("[", "").Trim();
            tarefa!.DescricaoTarefa = novaDescricaoFormatada;
            AnsiConsole.MarkupLine($"[green] Descrição da tarefa alterada com sucesso![/]!"); Console.WriteLine("\n");
            Thread.Sleep(1000);
        }

        private static void EditarStatus(Tarefa tarefa)
        {
            var status = StatusService.ListarStatus();
            var opcaoStatus = AnsiConsole.Prompt
                (
                    new SelectionPrompt<string>()
                    .Title("[gold1] Selecione o novo status: [/]")
                    .AddChoices(status.Keys)
                );
            var statusEscolhido = status[opcaoStatus];

            if (statusEscolhido.Categoria == "Concluido")
                tarefa.FinalizarTarefa();
            else if (statusEscolhido.Categoria == "Cancelado")
                tarefa.CancelarTarefa();

            tarefa!.StatusTarefa = statusEscolhido;
            AnsiConsole.MarkupLine($"[green] Status da tarefa alterado com sucesso![/]"); Console.WriteLine("\n");
            Thread.Sleep(1000);
        }

        private static void EditarPrioridade(Tarefa tarefa)
        {
            var prioridade = AnsiConsole.Prompt
                            (
                                new SelectionPrompt<string>()
                                .Title("[gold1] Selecione a nova prioridade: [/]")
                                .AddChoices(new[]
                                {
                                        "[cornflowerblue]1-[/] A",
                                        "[cornflowerblue]2-[/] B",
                                        "[cornflowerblue]3-[/] C",
                                        "[cornflowerblue]4-[/] D"
                                })
                            );
            var prioridadeEscolhida = char.Parse(prioridade.Substring(prioridade.Length - 1, 1));
            tarefa!.Prioridade = prioridadeEscolhida;
        }

        private static void EditarFuncionario(Tarefa tarefa, Dictionary<string, Funcionario> listaFunc)
        {
            var funcionarios = AnsiConsole.Prompt
                           (
                               new SelectionPrompt<string>()
                               .Title("[gold1] Selecione o novo funcionário: [/]")
                               .AddChoices(listaFunc.Keys)
                           );

            var funcEscolhido = listaFunc[funcionarios];
            funcEscolhido.Tarefas!.Add(tarefa);
            tarefa.FuncionarioDelegado = funcEscolhido;
            AnsiConsole.MarkupLine($"[green] Funcionário delegado alterado com sucesso![/]"); Console.WriteLine("\n");
            Thread.Sleep(1000);
        }
    }
}
