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
    public class ProjetoMenuView
    {
        public static void MenuProjetos()
        {
            const string voltar = "[red] Voltar[/]";
            do
            {
                AnsiConsole.Clear();
                var projetos = ProjetoService.ListarProjetos();
                AnsiConsole.Write(new Rule("[gold1]Gerenciar Projetos[/]").RuleStyle("grey").Centered());
                Console.WriteLine();
                if (projetos.Count > 0)
                {

                    var opcoes = AnsiConsole.Prompt
                    (
                        new SelectionPrompt<string>()
                        .Title("[gold1] Selecione um Projeto[/]")
                        .AddChoices(projetos.Keys)
                        .AddChoices("[green] Adicionar Novo Projeto[/]")
                        .AddChoices(voltar)
                    );
                    if (opcoes == voltar)
                    {
                        MenuView.MenuPrincipal().Wait(); // Se o usuário escolher voltar, sai do loop
                        break; // Se o usuário escolher voltar, sai do loop
                    }
                    else if (opcoes == "[green] Adicionar Novo Projeto[/]")
                    {
                        ProjetoCadastroView.AdicionarNovoProjeto(); // Chama o método para adicionar um novo projeto
                    }
                    else
                    {
                        var projetoEscolhido = projetos[opcoes]; // A variável projeto recebe o valor do dicionário, onde a chave é o projeto escolhido
                        ExibirProjeto(projetoEscolhido); // Chama o método para exibir o projeto escolhido
                    }
                }
                else
                {
                    AnsiConsole.MarkupLine("[red] Nenhum projeto cadastrado![/]");
                    Console.WriteLine();
                    Console.WriteLine();
                    Thread.Sleep(500);
                    var opcao = AnsiConsole.Prompt
                    (
                        new SelectionPrompt<string>()
                        .Title("[cadetblue] O que deseja fazer?[/]")
                        .AddChoices(new[]
                        {
                            "[cornflowerblue]1-[/] Adicionar Projeto",
                            "[red]Voltar[/]"
                        })
                    );
                    switch (opcao)
                    {
                        case "[cornflowerblue]1-[/] Adicionar Projeto": ProjetoCadastroView.AdicionarNovoProjeto(); break;
                        case "[red]Voltar[/]": MenuView.MenuPrincipal().Wait(); break;
                    }
                }
            } while (true);

        }
        public static void MenuProjetoSemTarefas(Projeto projeto)
        {
            if (projeto.StatusProjeto == Domain.Status.Cancelado || projeto.StatusProjeto == Domain.Status.Concluido)
            {
                AnsiConsole.MarkupLine("[red] Projeto cancelado ou concluído! Não é possível adicionar ou editar tarefas.[/]");
                Console.ReadKey();
                MenuProjetos();
                return;
            }
            var opcao = AnsiConsole.Prompt
                (
                  new SelectionPrompt<string>()
                  .Title(" [cadetblue]O que deseja fazer?[/]")
                  .AddChoices(new[]
                  {
                      "[cornflowerblue]1-[/] Adicionar Tarefa",
                      "[cornflowerblue]2-[/] Alterar Prioridade",
                      "[cornflowerblue]3-[/] Atribuir Gestor",
                      "[cornflowerblue]4-[/] Cancelar Projeto",
                      "[red]Voltar[/] "
                  })
                );

            switch (opcao)
            {
                case "[cornflowerblue]1-[/] Adicionar Tarefa": TarefaCrudView.AdicionarNovaTarefa(projeto); break;
                case "[cornflowerblue]2-[/] Alterar Prioridade": ProjetoAdmView.AlterarPrioridadeProjeto(projeto); break;
                case "[cornflowerblue]3-[/] Atribuir Gestor": ProjetoAdmView.AtribuirGestor(projeto); break;
                case "[cornflowerblue]4-[/] Cancelar Projeto": ProjetoAdmView.CancelarProjeto(projeto); break;
                case "[red]Voltar[/]": MenuProjetos(); break;
            }
        }
        public static void InformacoesProjeto(Projeto projeto)
        {

            AnsiConsole.Clear();
            AnsiConsole.Write(new Rule($"[gold1]{projeto.Nome}[/]").RuleStyle("grey").LeftJustified());
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
        public static void ExibirProjeto(Projeto projeto)
        {
            InformacoesProjeto(projeto); // Chama o método para exibir as informações do projeto
            if (projeto.Tarefas?.Count > 0)
            {
                TarefaTabelaView.CriarTabelaTarefas(projeto); // Chama o método para criar a tabela de tarefas
                TarefaTabelaView.MenuTarefas(projeto);
            }
            else
            {
                AnsiConsole.MarkupLine("[red] Nenhuma tarefa cadastrada para este projeto.[/]");
                Console.WriteLine();
                Console.WriteLine();
                MenuProjetoSemTarefas(projeto);
            }
            Console.WriteLine();
        }
    }
}
