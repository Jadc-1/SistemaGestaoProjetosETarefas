using SistemaGestaoProjetosETarefas.Service;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using SistemaGestaoProjetosETarefas.Domain;

namespace SistemaGestaoProjetosETarefas.Views
{
    public class ProjetoView
    {
       

        public static void MenuProjetos()
        {
            do
            {
                AnsiConsole.Clear();
                var projetos = ProjetoService.ListarProjetos(); // Chama o método para exibir todos os projetos cadastrados
                AnsiConsole.Write(new Rule("[gold1]Gerenciar Projetos[/]").RuleStyle("grey").Centered());
                Console.WriteLine();
                if (projetos.Count > 0)
                {
                    var voltar = "[darkred_1] Voltar[/]";
                    var opcoes = AnsiConsole.Prompt
                    (
                        new SelectionPrompt<string>()
                        .Title("[darkred_1] Selecione um Projeto[/]")
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
                MenuTarefas();
            }
            else
            {
                AnsiConsole.MarkupLine("[red]Nenhuma tarefa cadastrada para este projeto.[/]");
            }
            Console.WriteLine();
        }

        public static void CriarTabelaTarefas(Projeto projeto)
        {
            var table = new Table();
            table.Title("[gold1]Tarefas do projeto[/]");
            table.AddColumn(new TableColumn("[cadetblue]ID[/]").Centered()); // o TableColumn permite que seja possivel fazer configurações específicas nas colunas
            table.AddColumn(new TableColumn("[cadetblue]Nome[/]").Centered());
            table.AddColumn(new TableColumn("[cadetblue]Descrição[/]").Centered()); 
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

        public static void MenuTarefas()
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
                      "[cornflowerblue]9-[/] Voltar"
                  })
                );

           switch(opcao)
            {
                case "Adicionar Tarefa":break;
                case "Remover Tarefa": break;
                case "Alterar Status": break;
                case "Alterar Prioridade": break;
                case "Atribuir Gestor": break;
                case "Remover Gestor": break;
                case "Finalizar Projeto": break;
                case "Cancelar Projeto": break;
                case "Voltar": MenuProjetos(); break;
            }
        }
    }
}
