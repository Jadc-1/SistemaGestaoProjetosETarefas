using SistemaGestaoProjetosETarefas.Domain;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestaoProjetosETarefas.Views
{
    public class TarefaTabelaView
    {
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
                      "[cornflowerblue]4-[/] Alterar Prioridade",
                      "[cornflowerblue]5-[/] Atribuir Gestor",
                      "[cornflowerblue]6-[/] Remover Gestor",
                      "[cornflowerblue]7-[/] Finalizar Projeto",
                      "[cornflowerblue]8-[/] Cancelar Projeto",
                      "[red]Voltar[/] "
                  })
                );

                switch (opcao)
                {
                    case "[cornflowerblue]1-[/] Adicionar Tarefa": TarefaCrudView.AdicionarNovaTarefa(projeto); break;
                    case "[cornflowerblue]2-[/] Remover Tarefa": TarefaCrudView.RemoverTarefa(projeto); break;
                    case "[cornflowerblue]3-[/] Editar Tarefa Existente": TarefaCrudView.EscolherTarefaExistente(projeto); break;
                    case "[cornflowerblue]4-[/] Alterar Prioridade": ProjetoAdmView.AlterarPrioridadeProjeto(projeto); break;
                    case "[cornflowerblue]5-[/] Atribuir Gestor": ProjetoAdmView.AtribuirGestor(projeto); break;
                    case "[cornflowerblue]6-[/] Remover Gestor": ProjetoAdmView.RemoverGestor(projeto); break;
                    case "[cornflowerblue]7-[/] Finalizar Projeto": ProjetoAdmView.FinalizarProjeto(projeto); break;
                    case "[cornflowerblue]8-[/] Cancelar Projeto": ProjetoAdmView.CancelarProjeto(projeto); break;
                    case "[red]Voltar[/]": ProjetoMenuView.MenuProjetos(); break;
                }
            }
        }

    }
}
