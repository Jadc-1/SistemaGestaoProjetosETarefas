using SistemaGestaoProjetosETarefas.Domain;
using SistemaGestaoProjetosETarefas.Services;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestaoProjetosETarefas.Views
{
    public class ProjetoAdministrativoView
    {
        public static void AtribuirGestor(Projeto projeto)
        {
            while (true)
            {
                AnsiConsole.Clear();
                ProjetoMenuView.InformacoesProjeto(projeto);
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
                    ProjetoMenuView.ExibirProjeto(projeto);
                    break;
                }
                else
                {
                    AnsiConsole.MarkupLine("[red] Nenhum gestor cadastrado![/]");
                    Thread.Sleep(1700);
                    ProjetoMenuView.ExibirProjeto(projeto);
                    break;
                }
            }
        }

        public static void RemoverGestor(Projeto projeto)
        {
            while (true)
            {
                AnsiConsole.Clear();
                ProjetoMenuView.InformacoesProjeto(projeto);
                if (projeto.GestorDelegado == null)
                {
                    AnsiConsole.MarkupLine("[red] Nenhum gestor atribuído ao projeto![/]");
                    Thread.Sleep(1700);
                    ProjetoMenuView.ExibirProjeto(projeto);
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
                    ProjetoMenuView.ExibirProjeto(projeto);
                    break;
                }
                else
                {
                    projeto.GestorDelegado = null;
                    AnsiConsole.MarkupLine($"[green] Gestor removido com sucesso![/]"); Console.WriteLine("\n");
                    Thread.Sleep(1000);
                    ProjetoMenuView.ExibirProjeto(projeto);
                    break;
                }
            }
        }

        public static void FinalizarProjeto(Projeto projeto)
        {
            while (true)
            {
                AnsiConsole.Clear();
                ProjetoMenuView.InformacoesProjeto(projeto);
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
                    ProjetoMenuView.ExibirProjeto(projeto);
                    break;
                }
                else
                {
                    projeto.FinalizarProjeto();
                    AnsiConsole.MarkupLine($"[green] Projeto finalizado com sucesso![/]"); Console.WriteLine("\n");
                    Thread.Sleep(1000);
                    ProjetoMenuView.ExibirProjeto(projeto);
                    break;
                }
            }
        }

        public static void CancelarProjeto(Projeto projeto)
        {
            while (true)
            {
                AnsiConsole.Clear();
                ProjetoMenuView.InformacoesProjeto(projeto);
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
                    ProjetoMenuView.ExibirProjeto(projeto);
                    break;
                }
                else
                {
                    projeto.CancelarProjeto();
                    AnsiConsole.MarkupLine($"[green] Projeto cancelado com sucesso![/]"); Console.WriteLine("\n");
                    Thread.Sleep(1000);
                    ProjetoMenuView.ExibirProjeto(projeto);
                    break;
                }
            }
        }

        public static void AlterarPrioridadeProjeto(Projeto projeto)
        {
            while (true)
            {
                AnsiConsole.Clear();
                ProjetoMenuView.InformacoesProjeto(projeto);

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
                    ProjetoMenuView.ExibirProjeto(projeto);
                    break; // Se o usuário escolher voltar, sai do método
                }
                var prioridadeEscolhida = char.Parse(opcaoPrioridade.Substring(opcaoPrioridade.Length - 1, 1));
                projeto.Prioridade = prioridadeEscolhida;
                AnsiConsole.Markup($"[green] Prioridade do projeto alterada com sucesso![/]"); Console.WriteLine("\n");
                Thread.Sleep(1000);
                ProjetoMenuView.ExibirProjeto(projeto);
                break;
            }
        }
    }
}
