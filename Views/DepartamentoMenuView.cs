using SistemaGestaoProjetosETarefas.Service;
using SistemaGestaoProjetosETarefas.Domain;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SistemaGestaoProjetosETarefas.Views
{
    public class DepartamentoMenuView
    {
        public static void MenuDepartamentos()
        {
            AnsiConsole.Clear();
            AnsiConsole.Write(new Rule("[gold1 bold]Gerenciar Departamentos[/]").RuleStyle("grey").Centered());
            Console.WriteLine();
            var opcao = AnsiConsole.Prompt
                (
                    new SelectionPrompt<string>()
                    .Title("[gold1] Selecione uma opção: [/]")
                    .AddChoices(new[]
                    {
                        "[cornflowerblue]1- [/]Listar Departamentos",
                        "[cornflowerblue]2- [/]Adicionar Departamento",
                        "[cornflowerblue]3- [/]Editar Departamento",
                        "[cornflowerblue]4- [/]Desativar Departamento",
                        "[red]Voltar[/]"
                    })
                );
            switch (opcao)
            {
                case "[cornflowerblue]1- [/]Listar Departamentos": DepartamentoListagemView.ListarDepartamentos(); break;
                case "[cornflowerblue]2- [/]Adicionar Departamento": DepartamentoCrudView.AdicionarDepartamento(); break;
                case "[cornflowerblue]3- [/]Editar Departamento": DepartamentoCrudView.EditarDepartamento(); break;
                case "[cornflowerblue]4- [/]Desativar Departamento": DepartamentoCrudView.DesativarDepartamento(); break;
                case "[red]Voltar[/]": MenuPrincipalView.MenuPrincipal(); break;
            }

            Console.ReadKey();
        }

       
      
    }
}
