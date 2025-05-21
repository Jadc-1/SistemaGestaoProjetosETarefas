using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestaoProjetosETarefas.Views
{
    public class DepartamentoView
    {
        public void ExibirDepartamentos()
        {
            AnsiConsole.Write(new Rule("[gold1 bold]Gerenciar Departamentos[/]").RuleStyle("grey").Centered());
        }
    }
}
