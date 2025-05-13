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
       

        public static void MenuProjetos()
        {
            const string voltar = "[red] Voltar[/]";
            do
            {
                AnsiConsole.Clear();
                var projetos = ProjetoService.ListarProjetos(); // Chama o método para exibir todos os projetos cadastrados
                AnsiConsole.Write(new Rule("[gold1]Gerenciar Projetos[/]").RuleStyle("grey").Centered());
                Console.WriteLine();
                if (projetos.Count > 0)
                {
                    
                    var opcoes = AnsiConsole.Prompt
                    (
                        new SelectionPrompt<string>()
                        .Title("[gold1] Selecione um Projeto[/]")
                        .AddChoices(projetos.Keys)
                        .AddChoices(voltar)
                    );
                    if (opcoes == voltar)
                    {
                        break; // Se o usuário escolher voltar, sai do loop
                    }
                    else
                    {
                        var projetoEscolhido = projetos[opcoes]; // A variável projeto recebe o valor do dicionário, onde a chave é o projeto escolhido
                        ExibirProjeto(projetoEscolhido); // Chama o método para exibir o projeto escolhido
                    }
                    

                }
                else
                {

                }
            } while (true);
            
        }

        public static void ExibirProjeto(Projeto projeto)
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

        public static void CriarTabelaTarefas(Projeto projeto)
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

        public static void MenuTarefas(Projeto projeto)
        {
            Console.WriteLine("\n");
            var opcao = AnsiConsole.Prompt
                (
                  new SelectionPrompt<string>()
                  .Title(" [cadetblue]O que deseja fazer?[/]")
                  .AddChoices(new[]
                  {
                      "[cornflowerblue]1-[/] Adicionar Tarefa",
                      "[cornflowerblue]2-[/] Remover Tarefa",
                      "[cornflowerblue]3-[/] Alterar Status",
                      "[cornflowerblue]4-[/] Alterar Prioridade",
                      "[cornflowerblue]5-[/] Atribuir Gestor",
                      "[cornflowerblue]6-[/] Remover Gestor",
                      "[cornflowerblue]7-[/] Finalizar Projeto",
                      "[cornflowerblue]8-[/] Cancelar Projeto",
                      "[cornflowerblue]9-[/] [red]Voltar[/]"
                  })
                );

           switch(opcao)
            {
                case "[cornflowerblue]1-[/] Adicionar Tarefa":AdicionarNovaTarefa(projeto); break;
                case "[cornflowerblue]2-[/] Remover Tarefa": break;
                case "[cornflowerblue]3-[/] Alterar Status": break;
                case "[cornflowerblue]4-[/] Alterar Prioridade": break;
                case "[cornflowerblue]5-[/] Atribuir Gestor": break;
                case "[cornflowerblue]6-[/] Remover Gestor": break;
                case "[cornflowerblue]7-[/] Finalizar Projeto": break;
                case "[cornflowerblue]8-[/] Cancelar Projeto": break;
                case "[cornflowerblue]9-[/] [red]Voltar[/]": MenuProjetos(); break;
            }
        }

        private static void AdicionarNovaTarefa(Projeto projeto)
        {
            while(true)
            {
                AnsiConsole.Clear();
                var titulo = new Panel(new Text($"Adicionar nova tarefa ao projeto {projeto.Nome}", new Style(Color.Gold1)).Centered()).Border(BoxBorder.Heavy);
                titulo.Expand();
                AnsiConsole.Write(titulo);
                Console.WriteLine();
                AnsiConsole.Write(new Rule().RuleStyle("grey").LeftJustified());
                Console.WriteLine();

                var nome = AnsiConsole.Ask<string>(($"[cornflowerblue] Nome da Tarefa: [/] "));
                if (string.IsNullOrEmpty(nome)) AnsiConsole.WriteLine("[red]Nome inválido![/]"); //Não permitir a pessoa cadastrar uma tarefa sem nome
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
                AnsiConsole.MarkupLine($" Você selecionou a opção [cornflowerblue]{prioridadeEscolhida}[/]");
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
                    break;
                }
                else
                {
                    AnsiConsole.MarkupLine($"[red] Tarefa não adicionada![/]"); Console.WriteLine("\n");
                    Thread.Sleep(1000);
                    break;
                }
            }
        }
    }
}
