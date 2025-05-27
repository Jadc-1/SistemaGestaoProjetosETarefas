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
    public class ProjetoCadastroView
    {
        public static void AdicionarNovoProjeto()
        {
            while (true)
            {
                AnsiConsole.Clear();
                AnsiConsole.Write(new Rule("[gold1]Adicionar Novo Projeto[/]").RuleStyle("grey").Centered());
                Console.WriteLine();
                var nome = AnsiConsole.Ask<string>(("[cornflowerblue] Nome do Projeto: [/] "));
                var nomeFormatado = nome.Replace("[", "").Replace("]", "").Trim(); // Remove colchetes e espaços desnecessários
                Console.WriteLine();
                var descricao = AnsiConsole.Ask<string>(("[cornflowerblue] Descrição do Projeto: [/] "));
                var descricaoFormatada = descricao.Replace("[", "").Replace("]", "").Trim(); // Remove colchetes e espaços desnecessários
                Console.WriteLine();
                var dataInicio = DateTime.Now;
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
                Console.WriteLine();
                var listarGestores = GestorService.ListarGestores();
                Gestor? gestorEscolhido = null;
                if (listarGestores.Count > 0)
                {
                    var opcao = AnsiConsole.Prompt
                        (
                            new SelectionPrompt<string>()
                            .Title("[gold1] Deseja adicionar um Gestor?[/]")
                            .AddChoices(new[] { "[green] Sim[/]", "[red] Não[/]" })
                        );
                    if (opcao == "[green] Sim[/]")
                    {
                        var gestores = AnsiConsole.Prompt
                        (
                            new SelectionPrompt<string>()
                            .Title("[cornflowerblue] Selecione o gestor: [/]")
                            .AddChoices(listarGestores.Keys)
                        );
                        gestorEscolhido = listarGestores[gestores];
                        AnsiConsole.MarkupLine($" Você selecionou o gestor [cornflowerblue]{gestorEscolhido.Nome}[/]");
                       
                    }
                    Console.WriteLine();
                    var projeto = ConfirmarProjeto(nomeFormatado, descricaoFormatada, dataInicio, prioridadeEscolhida); // Chama o método para confirmar a adição do projeto
                    projeto.AtribuirGestor(gestorEscolhido!); // Atribui o gestor ao projeto
                    gestorEscolhido!.ProjetosGerenciados.Add(projeto); // Adiciona o projeto à lista de projetos do gestor
                    ProjetoService projetoService = new ProjetoService();
                    AnsiConsole.MarkupLine($"[green] Projeto {projeto.Nome} Adicionado com sucesso![/]"); Console.WriteLine("\n");
                    Thread.Sleep(1000);
                    ProjetoMenuView.MenuProjetos(); // Chama o método para exibir o menu de projetos
                }
                else
                {
                    Console.WriteLine();
                    var projeto = ConfirmarProjeto(nomeFormatado, descricaoFormatada, dataInicio, prioridadeEscolhida); // Chama o método para confirmar a adição do projeto
                    AnsiConsole.MarkupLine($"[green] Projeto {projeto.Nome} Adicionado com sucesso![/]"); Console.WriteLine("\n");
                    Thread.Sleep(1000);
                    ProjetoMenuView.MenuProjetos(); // Chama o método para exibir o menu de projetos
                }
            }
        }
        private static Projeto ConfirmarProjeto(string nomeFormatado, string descricaoFormatada, DateTime dataInicio, char prioridadeEscolhida)
        {
            while (true)
            {
                AnsiConsole.Write(new Rule().RuleStyle("grey").LeftJustified());
                var adicionar = AnsiConsole.Prompt
                    (
                        new SelectionPrompt<string>()
                        .Title("[gold1] Deseja realmente adicionar o projeto?[/]")
                        .AddChoices(new[]
                        {
                        "[green] Confirmar[/]", "[red] Cancelar[/]"
                        })
                    );

                if (adicionar == "[green] Confirmar[/]")
                {
                    Projeto projeto = new Projeto(nomeFormatado, descricaoFormatada, dataInicio, prioridadeEscolhida);
                    ProjetoService projetoService = new ProjetoService();
                    projetoService.AdicionarProjeto(projeto); // Chama o método para adicionar o projeto
                    return projeto;
                }
                else
                {
                    AnsiConsole.MarkupLine($"[red] Projeto não adicionado![/]"); Console.WriteLine("\n");
                    Thread.Sleep(1000);
                    ProjetoMenuView.MenuProjetos();
                }
                return null!; // Retorna nulo se o projeto não for adicionado
            }
        }

    }
}
