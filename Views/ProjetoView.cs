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


        public static void ExibirProjeto(Projeto projeto)
        {
            AnsiConsole.Clear();
            AnsiConsole.Write(new Rule($"[cornflowerblue]Tarefas do projeto: [/][gold1]{projeto.Nome}[/]").RuleStyle("grey").Centered());
            AnsiConsole.MarkupLine($"[cornflowerblue] Desc: [/] {projeto.Desc}");
            AnsiConsole.MarkupLine($"[cornflowerblue] Data de Início: [/] {projeto.DataInicio.ToString("dd/MM/yyyy")}");
            AnsiConsole.MarkupLine($"[cornflowerblue] Status: [/] {projeto.StatusProjeto}");


        }
    }
}
