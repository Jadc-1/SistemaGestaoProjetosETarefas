using System;
using SistemaGestaoProjetosETarefas.Services;
using SistemaGestaoProjetosETarefas.Views;
using Spectre.Console;

namespace SistemaGestaoProjetosETarefas
{
    class Program
    {
        static void Main(string[] args)
        {
            NomeSistema(); // Chama o método para exibir o nome do sistema
            MenuView.MenuPrincipal().Wait();
        }
        public static void NomeSistema()
        {
            AnsiConsole.Write
                (
                    new FigletText("Sistema de").Centered().Color(Color.Red) // Adiciona o título do sistema
                );
            AnsiConsole.Write
                (
                    new FigletText("Gestão de Projetos e Tarefas").Centered().Color(Color.Gold1) // Adiciona o título do sistema
                );
            Thread.Sleep(2000); // Aguarda 1 segundo
            AnsiConsole.Clear();
        }
    }
}
