using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaGestaoProjetosETarefas.Domain;
using SistemaGestaoProjetosETarefas.Service;
using SistemaGestaoProjetosETarefas.Services;
using Spectre.Console;
using Spectre.Console.Extensions;

namespace SistemaGestaoProjetosETarefas.Views
{
    public class RelatorioView
    {
        public static async Task MenuRelatorio() //Transforma em Assincrona para poder utilizar, e Task para esperar o retorno no futuro antes de finalizar o projeto
        {
            while (true)
            {
                AnsiConsole.Clear();
                var projetos = ProjetoService.ListarProjetos();
                AnsiConsole.Write(new Rule("[gold1]Gerar relatório inteligente em PDF[/]").RuleStyle("grey").Centered());
                if (projetos.Count == 0)
                {
                    Console.WriteLine();
                    AnsiConsole.MarkupLine("[red] Nenhum projeto cadastrado![/]");
                    Console.WriteLine();
                    Console.WriteLine();
                    Thread.Sleep(500);
                    var escolher = AnsiConsole.Prompt
                    (
                        new SelectionPrompt<string>()
                        .Title("[cadetblue] O que deseja fazer?[/]")
                        .AddChoices(new[]
                        {
                            "[cornflowerblue]1-[/] Adicionar Projeto",
                            "[red]Voltar[/]"
                        })
                    );
                    switch (escolher)
                    {
                        case "[cornflowerblue]1-[/] Adicionar Projeto": ProjetoCadastroView.AdicionarNovoProjeto(); break; 
                        case "[red]Voltar[/]": await MenuView.MenuPrincipal(); break;
                    }
                }
                var opcao = AnsiConsole.Prompt
                    (
                        new SelectionPrompt<string>()
                        .Title("[gold1] Selecione um projeto: [/] ")
                        .AddChoices(projetos.Keys)
                        .AddChoices(new[]
                        {
                        "[red] Voltar[/]"
                        })
                    );
                if (opcao == "[red] Voltar[/]")
                {
                    await MenuView.MenuPrincipal();
                }
                else
                {
                    AnsiConsole.Clear();
                    AnsiConsole.Write(new Rule("[gold1]Gerar relatório inteligente em PDF[/]").RuleStyle("grey").Centered());
                    var projetoSelecionado = projetos[opcao];

                    if (projetoSelecionado.Tarefas == null || projetoSelecionado.Tarefas.Count == 0)
                    {
                        AnsiConsole.Markup("[red]Nenhuma tarefa foi atribuída! Não é possível realizar um relatório.[/]");
                        Console.ReadKey();
                        await MenuRelatorio();
                    }
                    var conteudo = await AnsiConsole.Status()
                    .Spinner(Spinner.Known.Dots)
                    .StartAsync(" Gerando relatório com IA...", async ctx =>
                    {
                        return await RelatorioService.GerarRelatorioIAAsync(projetoSelecionado);
                    });
                    Console.WriteLine();
                    var nomeArquivo = AnsiConsole.Ask<string>("[green] Digite o nome desejado para o arquivo: [/]");
                    var nomeArquivoFormatado = nomeArquivo.Replace(" ", "_").Replace("[", "").Replace("]", "").Replace("(", "").Replace(")", "").ToLower(); // Remove espaços e caracteres especiais
                    GerarPDFService.GerarPDF(conteudo!, $"{nomeArquivoFormatado}.pdf", projetoSelecionado.Nome!);
                    Console.WriteLine();
                    AnsiConsole.Write(new Rule().RuleStyle("grey"));
                    Console.WriteLine();
                    AnsiConsole.MarkupLine("[grey] Pressione ENTER para continuar...[/]");
                    Console.ReadKey();
                    return; // volta ao menu anterior, sem continuar o while
                }
            }
        }
    }
}
