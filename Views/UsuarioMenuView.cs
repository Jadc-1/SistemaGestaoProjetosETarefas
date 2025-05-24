using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestaoProjetosETarefas.Views
{
    public class UsuarioMenuView
    {
        public static void MenuUsuario()
        {
            while (true)
            {
                AnsiConsole.Clear();
                AnsiConsole.Write(new Rule("[gold1] Gerenciar Usuários[/]").RuleStyle("grey").Centered());
                Console.WriteLine();
                var opcao = AnsiConsole.Prompt
                (
                    new SelectionPrompt<string>()
                    .Title("[gold1] Selecione uma opção: [/] ")
                    .AddChoices(new[]
                    {
                    "[cornflowerblue]1 -[/] Listar Funcionários",
                    "[cornflowerblue]2 -[/] Listar Gestores",
                    "[cornflowerblue]3 -[/] Adicionar novo usuário",
                    "[cornflowerblue]4 -[/] Editar usuário",
                    "[cornflowerblue]5 -[/] Desativar usuário",
                    "[cornflowerblue]6 -[/] Alterar departamento do usuário",
                    "[red]Voltar[/]"
                    })
                );

                switch (opcao)
                {
                    case "[cornflowerblue]1 -[/] Listar Funcionários": UsuarioListarView.ListarFuncionarios(); break;
                    case "[cornflowerblue]2 -[/] Listar Gestores": UsuarioListarView.ListarGestores(); break;
                    case "[cornflowerblue]3 -[/] Adicionar novo usuário": UsuarioCrudView.AdicionarNovoUsuario(); break;
                    case "[cornflowerblue]4 -[/] Editar usuário": UsuarioCrudView.EditarUsuario(); break;
                    case "[cornflowerblue]5 -[/] Desativar usuário": UsuarioCrudView.DesativarUsuario(); break;
                    case "[cornflowerblue]6 -[/] Alterar departamento do usuário": UsuarioCrudView.AlterarDepartamentoUsuario(); break;
                    case "[red]Voltar[/]": MenuView.MenuPrincipal().Wait(); break;
                }
            }
        }
    }
}
