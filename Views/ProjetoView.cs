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
            var projetos = ProjetoService.ListarProjetos(); // Chama o método para exibir todos os projetos cadastrados
            AnsiConsole.Clear();
            AnsiConsole.Write(new Rule("[gold1]Gerenciar Projetos[/]").RuleStyle("grey").Centered());
            Console.WriteLine();
            if (projetos.Count > 0)
            {
                var opcoes = AnsiConsole.Prompt
                (
                    new SelectionPrompt<string>()
                    .Title("[darkred_1] Selecione um Projeto[/]")
                    .AddChoices(projetos.Keys)
                );
                var projetoEscolhido = projetos[opcoes]; // A variável projeto recebe o valor do dicionário, onde a chave é o projeto escolhido
                ExibirProjeto(projetoEscolhido); // Chama o método para exibir o projeto escolhido

                Console.ReadKey();
            }
            else
            {

            }
        }

        public static void ExibirProjeto(Projeto projeto)
        {
            AnsiConsole.Clear();
            AnsiConsole.Write(new Rule($"[cornflowerblue]Tarefas do projeto: [/][gold1]{projeto.Nome}[/]").RuleStyle("grey").Centered());
            Console.WriteLine();
            AnsiConsole.MarkupLine($"[cornflowerblue] Desc: [/] {projeto.Desc}");
            AnsiConsole.MarkupLine($"[cornflowerblue] Data de Início: [/] {projeto.DataInicio.ToString("dd/MM/yyyy")}");
            AnsiConsole.MarkupLine($"[cornflowerblue] Status: [/] {projeto.StatusProjeto}");
            Console.WriteLine();
            AnsiConsole.Write(new Rule().RuleStyle("grey").LeftJustified());
            Console.WriteLine();
            if (projeto.Tarefas.Count > 0)
            {
                CriarTabelaTarefas(projeto); // Chama o método para criar a tabela de tarefas
            }
            else
            {
                AnsiConsole.MarkupLine("[red]Nenhuma tarefa cadastrada para este projeto.[/]");
            }


        }

        public static void CriarTabelaTarefas(Projeto projeto)
        {
            var table = new Table();
            table.Title("[gold1]Tarefas do projeto[/]").LeftAligned();
            table.AddColumn(new TableColumn("[cadetblue]ID[/]").Centered()); // o TableColumn permite que seja possivel fazer configurações específicas nas colunas
            table.AddColumn(new TableColumn("[cadetblue]Nome[/]").Centered());
            table.AddColumn(new TableColumn("[cadetblue]Descrição[/]").Centered()); 
            table.AddColumn(new TableColumn("[cadetblue]Data de Início[/]").Centered());
            table.AddColumn(new TableColumn("[cadetblue]Status[/]").Centered());
            table.AddColumn(new TableColumn("[cadetblue]Prioridade[/]").Centered()); 
            table.AddColumn(new TableColumn("[cadetblue]Funcionários[/]").Centered());

            foreach (Tarefa tarefa in projeto.Tarefas)
            {
                table.AddRow
                (
                    tarefa.IdTarefa.ToString(),
                    tarefa.NomeTarefa!,
                    tarefa.DescricaoTarefa!,
                    tarefa.DataInicio.ToString("dd/MM/yyyy"),
                    tarefa.StatusTarefa.ToString(),
                    tarefa.Prioridade.ToString(),
                    tarefa.FuncionarioDelegado != null ? tarefa.FuncionarioDelegado.Nome! : "Nenhum Funcionario Atribuido"
                );
            }
            table.BorderColor(Color.CadetBlue);
            table.Expand();
            table.Centered();
            
            AnsiConsole.Write(table);
        }
    }
}
