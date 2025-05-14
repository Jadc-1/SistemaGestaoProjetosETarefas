using SistemaGestaoProjetosETarefas.Service;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using SistemaGestaoProjetosETarefas.Domain;
using SistemaGestaoProjetosETarefas.Services;

namespace SistemaGestaoProjetosETarefas.Views
{
    public class ProjetoView
    {
        private static readonly Dictionary<string, Projeto> Projetos = ProjetoService.ListarProjetos();

        public static void MenuProjetos()
        {
            const string voltar = "[red] Voltar[/]";
            do
            {
                AnsiConsole.Clear();

                AnsiConsole.Write(new Rule("[gold1]Gerenciar Projetos[/]").RuleStyle("grey").Centered());
                Console.WriteLine();
                if (Projetos.Count > 0)
                {

                    var opcoes = AnsiConsole.Prompt
                    (
                        new SelectionPrompt<string>()
                        .Title("[gold1] Selecione um Projeto[/]")
                        .AddChoices(Projetos.Keys)
                        .AddChoices(voltar)
                    );
                    if (opcoes == voltar)
                    {
                        MenuView.MenuPrincipal(); // Se o usuário escolher voltar, sai do loop
                        break; // Se o usuário escolher voltar, sai do loop
                    }
                    else
                    {
                        var projetoEscolhido = Projetos[opcoes]; // A variável projeto recebe o valor do dicionário, onde a chave é o projeto escolhido
                        ExibirProjeto(projetoEscolhido); // Chama o método para exibir o projeto escolhido
                    }
                }
                else
                {
                    // Fazer interface, caso não tenha nenhum projeto cadastrado!
                }
            } while (true);

        }

        public static void ExibirProjeto(Projeto projeto)
        {
            InformacoesProjeto(projeto); // Chama o método para exibir as informações do projeto
            if (projeto.Tarefas?.Count > 0)
            {
                CriarTabelaTarefas(projeto); // Chama o método para criar a tabela de tarefas
                MenuTarefas(projeto);
            }
            else
            {
                AnsiConsole.MarkupLine("[red]Nenhuma tarefa cadastrada para este projeto.[/]");
                Console.ReadKey();
            }
            Console.WriteLine();
        }

        private static void InformacoesProjeto(Projeto projeto)
        {
            AnsiConsole.Clear();
            AnsiConsole.Write(new Rule($"[gold1]{projeto.Nome}[/]").RuleStyle("grey").Centered());
            Console.WriteLine();
            AnsiConsole.MarkupLine($"[cornflowerblue] Desc: [/] {projeto.Desc}");
            AnsiConsole.MarkupLine($"[cornflowerblue] Data de Início: [/] {projeto.DataInicio.ToString("dd/MM/yyyy")}");
            AnsiConsole.MarkupLine($"[cornflowerblue] Status: [/] {projeto.StatusProjeto!.Categoria}");
            AnsiConsole.MarkupLine($"[cornflowerblue] Prioridade: [/] {projeto.Prioridade}");
            var gestor = projeto.GestorDelegado != null ? projeto.GestorDelegado.Nome : "Nenhum Gestor Atribuído";
            AnsiConsole.MarkupLine($"[cornflowerblue] Gestor Delegado: [/] {gestor}");
            Console.WriteLine();
            AnsiConsole.Write(new Rule().RuleStyle("grey").LeftJustified());
            Console.WriteLine();
        }

        private static void CriarTabelaTarefas(Projeto projeto)
        {
            var table = new Table();
            table.Title("[gold1]Tarefas do projeto[/]");
            table.AddColumn(new TableColumn("[cadetblue]ID[/]").Centered()); // o TableColumn permite que seja possivel fazer configurações específicas nas colunas
            table.AddColumn(new TableColumn("[cadetblue]Nome[/]").Centered());
            table.AddColumn(new TableColumn("[cadetblue]Descrição[/]").LeftAligned());
            table.AddColumn(new TableColumn("[cadetblue]Funcionários[/]").Centered());
            table.AddColumn(new TableColumn("[cadetblue]Data de Início[/]").Centered());
            table.AddColumn(new TableColumn("[cadetblue]Status[/]").Centered());
            table.AddColumn(new TableColumn("[cadetblue]Prioridade[/]").Centered());


            foreach (Tarefa tarefa in projeto.Tarefas!)
            {
                table.AddRow
                (
                    tarefa.IdTarefa.ToString(),
                    tarefa.NomeTarefa!,
                    tarefa.DescricaoTarefa!,
                    tarefa.FuncionarioDelegado != null ? tarefa.FuncionarioDelegado.Nome! : "Nenhum Funcionario Atribuido",
                    tarefa.DataInicio.ToString("dd/MM/yyyy"),
                    tarefa.StatusTarefa!.Categoria!,
                    tarefa.Prioridade.ToString()
                );
            }
            table.BorderColor(Color.CadetBlue);
            table.Expand();
            table.Centered();
            AnsiConsole.Write(table);

        }

        private static void MenuTarefas(Projeto projeto)
        {
            Console.WriteLine("\n");
            if (projeto.StatusProjeto == Domain.Status.Cancelado || projeto.StatusProjeto == Domain.Status.Concluido)
            {
                AnsiConsole.MarkupLine("[red] Projeto cancelado ou concluído! Não é possível adicionar ou editar tarefas.[/]");
                Console.ReadKey();
                return;
            }
            else
            {
                var opcao = AnsiConsole.Prompt
                (
                  new SelectionPrompt<string>()
                  .Title(" [cadetblue]O que deseja fazer?[/]")
                  .AddChoices(new[]
                  {
                      "[cornflowerblue]1-[/] Adicionar Tarefa",
                      "[cornflowerblue]2-[/] Remover Tarefa",
                      "[cornflowerblue]3-[/] Editar Tarefa Existente",
                      "[cornflowerblue]4-[/] Alterar Status do Projeto",
                      "[cornflowerblue]5-[/] Alterar Prioridade",
                      "[cornflowerblue]6-[/] Atribuir Gestor",
                      "[cornflowerblue]7-[/] Remover Gestor",
                      "[cornflowerblue]8-[/] Finalizar Projeto",
                      "[cornflowerblue]9-[/] Cancelar Projeto",
                      "[red]Voltar[/] "
                  })
                );

                switch (opcao)
                {
                    case "[cornflowerblue]1-[/] Adicionar Tarefa": AdicionarNovaTarefa(projeto); break;
                    case "[cornflowerblue]2-[/] Remover Tarefa": RemoverTarefa(projeto); break;
                    case "[cornflowerblue]3-[/] Editar Tarefa Existente": EscolherTarefaExistente(projeto); break;
                    case "[cornflowerblue]4-[/] Alterar Status do Projeto": AlterarStatusProjeto(projeto); break;
                    case "[cornflowerblue]5-[/] Alterar Prioridade": AlterarPrioridadeProjeto(projeto); break;
                    case "[cornflowerblue]6-[/] Atribuir Gestor": AtribuirGestor(projeto); break;
                    case "[cornflowerblue]7-[/] Remover Gestor": RemoverGestor(projeto); break;
                    case "[cornflowerblue]8-[/] Finalizar Projeto": FinalizarProjeto(projeto); break;
                    case "[cornflowerblue]9-[/] Cancelar Projeto": CancelarProjeto(projeto); break;
                    case "[cornflowerblue]10-[/] [red]Voltar[/]": MenuProjetos(); break;
                }
            }
        }

        private static void AdicionarNovaTarefa(Projeto projeto)
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
                Console.WriteLine();
                var descricao = AnsiConsole.Ask<string>(($"[cornflowerblue] Descrição da Tarefa: [/] ")); //descrição poderá ser nula ou vazia
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
                    Tarefa tarefa = new Tarefa(nome, descricao, dataInicio, statusEscolhido, prioridadeEscolhida);
                    projeto.AdicionarTarefa(tarefa); // Chama o método para adicionar a tarefa ao projeto
                    AnsiConsole.MarkupLine($"[green] Tarefa {tarefa.NomeTarefa} Adicionada com sucesso![/]"); Console.WriteLine("\n");
                    Thread.Sleep(1000);
                    ExibirProjeto(projeto);
                    break;
                }
                else
                {
                    AnsiConsole.MarkupLine($"[red] Tarefa não adicionada![/]"); Console.WriteLine("\n");
                    Thread.Sleep(1000);
                    ExibirProjeto(projeto);
                    break;
                }
            }
        }

        private static void RemoverTarefa(Projeto projeto)
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
                if (escolha == voltar) ExibirProjeto(projeto); // Se o usuário escolher voltar, sai do método
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
                    ExibirProjeto(projeto);
                    break;
                }
                else
                {
                    var tarefa = projeto.Tarefas!.FirstOrDefault(t => t.NomeTarefa == escolha);
                    projeto.RemoverTarefa(tarefa!);
                    AnsiConsole.MarkupLine($"[green] Tarefa {tarefa!.NomeTarefa} Removida com sucesso![/]"); Console.WriteLine("\n");
                    Thread.Sleep(1000);
                    ExibirProjeto(projeto);
                    break;
                }

            }
        }

        private static void EscolherTarefaExistente(Projeto projeto)
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

                CriarTabelaTarefas(projeto);

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
                    ExibirProjeto(projeto);
                    break; // Se o usuário escolher voltar, sai do loop
                }

                var tarefa = projeto.Tarefas!.FirstOrDefault(t => t.NomeTarefa == opcao);

                EditarTarefaExistente(tarefa!, projeto); // Chama o método para editar a tarefa escolhida

            }
        }

        private static void EditarTarefaExistente(Tarefa tarefa, Projeto projeto)
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
                case "[cornflowerblue]1-[/] Nome":
                    {
                        var novoNome = AnsiConsole.Ask<string>($"[cornflowerblue] Novo nome da tarefa: [/] ");
                        tarefa!.NomeTarefa = novoNome;
                        AnsiConsole.MarkupLine($"[green] Nome da tarefa alterado com sucesso![/]"); Console.WriteLine("\n");
                        Thread.Sleep(1000);
                        break;
                    }
                case "[cornflowerblue]2-[/] Descrição":
                    {
                        var novaDescricao = AnsiConsole.Ask<string>("[cornflowerblue] Nova descrição da tarefa: [/]");
                        tarefa!.DescricaoTarefa = novaDescricao;
                        AnsiConsole.MarkupLine($"[green] Descrição da tarefa alterada com sucesso![/]!"); Console.WriteLine("\n");
                        break;
                    }
                case "[cornflowerblue]3-[/] Status":
                    {
                        var status = StatusService.ListarStatus();
                        var opcaoStatus = AnsiConsole.Prompt
                            (
                                new SelectionPrompt<string>()
                                .Title("[gold1] Selecione o novo status: [/]")
                                .AddChoices(status.Keys)
                            );
                        var statusEscolhido = status[opcaoStatus];
                        tarefa!.StatusTarefa = statusEscolhido;
                        AnsiConsole.MarkupLine($"[green] Status da tarefa alterado com sucesso![/]"); Console.WriteLine("\n");
                        Thread.Sleep(1000);
                        break;
                    }
                case "[cornflowerblue]4-[/] Prioridade":
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
                        break;
                    }
                case "[cornflowerblue]5-[/] Funcionário delegado":
                    {
                        var listaFunc = FuncionarioService.ListarFuncionarios();
                        if (listaFunc.Count != 0)
                        {
                            var funcionarios = AnsiConsole.Prompt
                            (
                                new SelectionPrompt<string>()
                                .Title("[gold1] Selecione o novo funcionário: [/]")
                                .AddChoices(listaFunc.Keys)
                            );

                            var funcEscolhido = listaFunc[funcionarios];
                            tarefa.FuncionarioDelegado = funcEscolhido;
                            AnsiConsole.MarkupLine($"[green] Funcionário delegado alterado com sucesso![/]"); Console.WriteLine("\n");
                            Thread.Sleep(1000);
                            break;
                        }
                        else
                        {
                            AnsiConsole.MarkupLine("[red] Nenhum funcionário cadastrado![/]");
                            Thread.Sleep(1000);
                            break;
                        }
                    }
                case "[cornflowerblue]6-[/] [red]Voltar[/]":
                    {
                        EscolherTarefaExistente(projeto);
                        break;
                    }
            }
        }

        private static void AlterarStatusProjeto(Projeto projeto)
        {
            while (true)
            {
                AnsiConsole.Clear();
                InformacoesProjeto(projeto);
                var status = StatusService.ListarStatus();
                var opcaoStatus = AnsiConsole.Prompt
                    (
                        new SelectionPrompt<string>()
                        .Title("[gold1] Selecione o novo status: [/]")
                        .AddChoices(status.Keys)
                        .AddChoices("[red]Voltar[/]")
                    );
                if (opcaoStatus == "[red]Voltar[/]")
                {
                    ExibirProjeto(projeto);
                    break; // Se o usuário escolher voltar, sai do método
                }
                var statusEscolhido = status[opcaoStatus];
                projeto.AlterarStatus(statusEscolhido);
                AnsiConsole.Markup($"[green] Status do projeto alterado com sucesso![/]"); Console.WriteLine("\n");
                Thread.Sleep(1000);
                ExibirProjeto(projeto);
                break;
            }
        }

        private static void AlterarPrioridadeProjeto(Projeto projeto)
        {
            while (true)
            {
                AnsiConsole.Clear();
                InformacoesProjeto(projeto);

                var opcaoPrioridade = AnsiConsole.Prompt
                    (
                        new SelectionPrompt<string>()
                        .Title("[gold1] Selecione a nova prioridade: [/]")
                        .AddChoices(new[]
                        {
                            "[cornflowerblue]1-[/] A",
                            "[cornflowerblue]2-[/] B",
                            "[cornflowerblue]3-[/] C",
                            "[cornflowerblue]4-[/] D",
                            "[red]Voltar[/]"
                        })
                    );
                if (opcaoPrioridade == "[red]Voltar[/]")
                {
                    ExibirProjeto(projeto);
                    break; // Se o usuário escolher voltar, sai do método
                }
                var prioridadeEscolhida = char.Parse(opcaoPrioridade.Substring(opcaoPrioridade.Length - 1, 1));
                projeto.Prioridade = prioridadeEscolhida;
                AnsiConsole.Markup($"[green] Prioridade do projeto alterada com sucesso![/]"); Console.WriteLine("\n");
                Thread.Sleep(1000);
                ExibirProjeto(projeto);
                break;
            }
        }

        private static void AtribuirGestor(Projeto projeto)
        {
            while (true)
            {
                AnsiConsole.Clear();
                InformacoesProjeto(projeto);
                var listaGestores = GestorService.ListarGestores();
                if (listaGestores.Count != 0)
                {
                    var gestores = AnsiConsole.Prompt
                    (
                        new SelectionPrompt<string>()
                        .Title("[gold1] Selecione o novo gestor: [/]")
                        .AddChoices(listaGestores.Keys)
                    );
                    var gestorEscolhido = listaGestores[gestores];
                    projeto.GestorDelegado = gestorEscolhido;
                    AnsiConsole.MarkupLine($"[green] Gestor delegado alterado com sucesso![/]"); Console.WriteLine("\n");
                    Thread.Sleep(1000);
                    ExibirProjeto(projeto);
                    break;
                }
                else
                {
                    AnsiConsole.MarkupLine("[red] Nenhum gestor cadastrado![/]");
                    Thread.Sleep(1700);
                    ExibirProjeto(projeto);
                    break;
                }
            }
        }

        private static void RemoverGestor(Projeto projeto)
        {
            while (true)
            {
                AnsiConsole.Clear();
                InformacoesProjeto(projeto);
                if (projeto.GestorDelegado == null)
                {
                    AnsiConsole.MarkupLine("[red] Nenhum gestor atribuído ao projeto![/]");
                    Thread.Sleep(1700);
                    ExibirProjeto(projeto);
                    break;
                }
                var gestor = AnsiConsole.Prompt
                    (
                        new SelectionPrompt<string>()
                        .Title("[gold1] Deseja remover o gestor? [/]")
                        .AddChoices(new[]
                        {
                            "[green] Confirmar[/]", "[red] Cancelar[/]"
                        })
                    );
                if (gestor == "[red] Cancelar[/]")
                {
                    ExibirProjeto(projeto);
                    break;
                }
                else
                {
                    projeto.GestorDelegado = null;
                    AnsiConsole.MarkupLine($"[green] Gestor removido com sucesso![/]"); Console.WriteLine("\n");
                    Thread.Sleep(1000);
                    ExibirProjeto(projeto);
                    break;
                }
            }
        }

        private static void FinalizarProjeto(Projeto projeto)
        {
            while (true)
            {
                AnsiConsole.Clear();
                InformacoesProjeto(projeto);
                var finalizar = AnsiConsole.Prompt
                    (
                        new SelectionPrompt<string>()
                        .Title("[gold1] Deseja finalizar o projeto? [/]")
                        .AddChoices(new[]
                        {
                            "[green] Confirmar[/]", "[red] Cancelar[/]"
                        })
                    );
                if (finalizar == "[red] Cancelar[/]")
                {
                    ExibirProjeto(projeto);
                    break;
                }
                else
                {
                    projeto.FinalizarProjeto();
                    AnsiConsole.MarkupLine($"[green] Projeto finalizado com sucesso![/]"); Console.WriteLine("\n");
                    Thread.Sleep(1000);
                    ExibirProjeto(projeto);
                    break;
                }
            }
        }
        
        private static void CancelarProjeto(Projeto projeto)
        {
            while (true)
            {
                AnsiConsole.Clear();
                InformacoesProjeto(projeto);
                var cancelar = AnsiConsole.Prompt
                    (
                        new SelectionPrompt<string>()
                        .Title("[gold1] Deseja cancelar o projeto? [/]")
                        .AddChoices(new[]
                        {
                            "[green] Confirmar[/]", "[red] Cancelar[/]"
                        })
                    );
                if (cancelar == "[red] Cancelar[/]")
                {
                    ExibirProjeto(projeto);
                    break;
                }
                else
                {
                    projeto.CancelarProjeto();
                    AnsiConsole.MarkupLine($"[green] Projeto cancelado com sucesso![/]"); Console.WriteLine("\n");
                    Thread.Sleep(1000);
                    ExibirProjeto(projeto);
                    break;
                }
            }
        }
    }
}