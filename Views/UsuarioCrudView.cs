﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spectre.Console;
using SistemaGestaoProjetosETarefas.Domain;
using SistemaGestaoProjetosETarefas.Services;
using SistemaGestaoProjetosETarefas.Service;
using System.Xml;

namespace SistemaGestaoProjetosETarefas.Views
{
    public class UsuarioCrudView
    {
        public static void AdicionarNovoUsuario()
        {
            while (true)
            {
                AnsiConsole.Clear();
                AnsiConsole.Write(new Rule("[gold1]Adicionar Novo Usuário[/]").RuleStyle("grey").Centered());
                Console.WriteLine();
                var opcao = AnsiConsole.Prompt
                    (
                        new SelectionPrompt<string>()
                        .Title("[gold1] Selecione o tipo de usuário:[/]")
                        .AddChoices(new[]
                        {
                            "[cornflowerblue]1 -[/] Funcionário",
                            "[cornflowerblue]2 -[/] Gestor",
                            "[red]Voltar[/]"
                        })
                    );
                switch (opcao)
                {
                    case "[cornflowerblue]1 -[/] Funcionário": AdicionarFuncionario(); break;
                    case "[cornflowerblue]2 -[/] Gestor": AdicionarGestor(); break;
                    case "[red]Voltar[/]": UsuarioMenuView.MenuUsuario(); break;
                }

            }
        }

        public static void AdicionarGestor()
        {
            while (true)
            {
                AnsiConsole.Clear();
                DepartamentoService departamentoService = new();
                var departamentos = departamentoService.ListarDepartamentos();
                AnsiConsole.Write(new Rule("[gold1]Adicionar Novo Gestor[/]").RuleStyle("grey").Centered());
                Console.WriteLine();
                var nome = AnsiConsole.Ask<string>(("[cornflowerblue] Nome do Gestor: [/] "));
                var nomeFormatado = nome.Replace("[", "").Replace("]", "").Trim(); // Remove colchetes e espaços desnecessários
                Console.WriteLine();
                var email = AnsiConsole.Ask<string>(("[cornflowerblue] E-mail do Gestor: [/] "));
                var emailFormatado = email.Replace("[", "").Replace("]", "").Trim(); // Remove colchetes e espaços desnecessários
                Console.WriteLine();
                var telefone = AnsiConsole.Ask<string>(("[cornflowerblue] Telefone do Gestor: [/] "));
                var telefoneFormatado = telefone.Replace("[", "").Replace("]", "").Trim(); // Remove colchetes e espaços desnecessários
                Console.WriteLine();
                AnsiConsole.Write(new Rule("[gold1]Endereço[/]").RuleStyle("grey").LeftJustified());
                Console.WriteLine();
                var estado = AnsiConsole.Ask<string>(("[cornflowerblue] Estado: [/] "));
                var estadoFormatado = estado.Replace("[", "").Replace("]", "").Trim(); // Remove colchetes e espaços desnecessários
                Console.WriteLine();
                var cidade = AnsiConsole.Ask<string>(("[cornflowerblue] Cidade: [/] "));
                var cidadeFormatada = cidade.Replace("[", "").Replace("]", "").Trim(); // Remove colchetes e espaços desnecessários
                Console.WriteLine();
                var rua = AnsiConsole.Ask<string>(("[cornflowerblue] Rua: [/] "));
                var ruaFormatada = rua.Replace("[", "").Replace("]", "").Trim(); // Remove colchetes e espaços desnecessários
                Console.WriteLine();
                var numero = AnsiConsole.Ask<string>(("[cornflowerblue] Número: [/] "));
                var numeroFormatado = numero.Replace("[", "").Replace("]", "").Trim(); // Remove colchetes e espaços desnecessários
                Console.WriteLine();
                AnsiConsole.Write(new Rule().RuleStyle("grey").Centered());
                Console.WriteLine();
                var endereco = new Endereco(ruaFormatada, numeroFormatado, cidadeFormatada, estadoFormatado);
                var dataAdmissao = DateTime.Now;
                var opcao = AnsiConsole.Prompt
                    (
                        new SelectionPrompt<string>()
                        .Title("[cornflowerblue] Deseja adicionar um Gestor? [/] ")
                        .AddChoices(new[] { "[green] Sim[/]", "[red] Não[/]" })
                    );
                if (opcao == "[green] Sim[/]")
                {
                    var gestor = new Gestor(nomeFormatado, emailFormatado, telefoneFormatado, dataAdmissao, endereco);
                    GestorService gestorService = new GestorService();
                    gestorService.AdicionarGestor(gestor);
                    Console.WriteLine();
                    AnsiConsole.Markup("[green] Gestor adicionado com sucesso![/]");
                    Thread.Sleep(1000);
                    UsuarioListarView.ListarGestores();
                }
                else
                {
                    Console.WriteLine();
                    AnsiConsole.Write(new Rule().RuleStyle("grey").Centered());
                    AnsiConsole.MarkupLine("[red] Gestor não adicionado![/]");
                    Console.WriteLine();
                    Console.WriteLine();
                    Thread.Sleep(1000);
                    UsuarioListarView.ListarGestores();
                }
            }
        }

        public static void AdicionarFuncionario()
        {
            while (true)
            {
                AnsiConsole.Clear();
                DepartamentoService departamentoService = new();
                var departamentos = departamentoService.ListarDepartamentos();
                AnsiConsole.Write(new Rule("[gold1]Adicionar Novo Funcionário[/]").RuleStyle("grey").Centered());
                Console.WriteLine();
                var nome = AnsiConsole.Ask<string>(("[cornflowerblue] Nome do Funcionário: [/] "));
                var nomeFormatado = nome.Replace("[", "").Replace("]", "").Trim(); // Remove colchetes e espaços desnecessários
                Console.WriteLine();
                var email = AnsiConsole.Ask<string>(("[cornflowerblue] E-mail do Funcionário: [/] "));
                var emailFormatado = email.Replace("[", "").Replace("]", "").Trim(); // Remove colchetes e espaços desnecessários
                Console.WriteLine();
                var telefone = AnsiConsole.Ask<string>(("[cornflowerblue] Telefone do Funcionário: [/] "));
                var telefoneFormatado = telefone.Replace("[", "").Replace("]", "").Trim(); // Remove colchetes e espaços desnecessários
                Console.WriteLine();
                AnsiConsole.Write(new Rule("[gold1]Endereço[/]").RuleStyle("grey").LeftJustified());
                Console.WriteLine();
                var estado = AnsiConsole.Ask<string>(("[cornflowerblue] Estado: [/] "));
                var estadoFormatado = estado.Replace("[", "").Replace("]", "").Trim(); // Remove colchetes e espaços desnecessários
                Console.WriteLine();
                var cidade = AnsiConsole.Ask<string>(("[cornflowerblue] Cidade: [/] "));
                var cidadeFormatada = cidade.Replace("[", "").Replace("]", "").Trim(); // Remove colchetes e espaços desnecessários
                Console.WriteLine();
                var rua = AnsiConsole.Ask<string>(("[cornflowerblue] Rua: [/] "));
                var ruaFormatada = rua.Replace("[", "").Replace("]", "").Trim(); // Remove colchetes e espaços desnecessários
                Console.WriteLine();
                var numero = AnsiConsole.Ask<string>(("[cornflowerblue] Número: [/] "));
                var numeroFormatado = numero.Replace("[", "").Replace("]", "").Trim(); // Remove colchetes e espaços desnecessários
                Console.WriteLine();
                AnsiConsole.Write(new Rule().RuleStyle("grey").Centered());
                var endereco = new Endereco(ruaFormatada, numeroFormatado, cidadeFormatada, estadoFormatado);
                var dataAdmissao = DateTime.Now;
                Console.WriteLine();
                Departamento departamentoEscolhido = null!;
                if (departamentos.Count > 0)
                {
                    var departamento = AnsiConsole.Prompt
                    (
                        new SelectionPrompt<string>()
                        .Title("[cornflowerblue] Selecione o departamento: [/] ")
                        .AddChoices(departamentos.Keys)
                    );
                    departamentoEscolhido = departamentos[departamento];
                }
                var opcao = AnsiConsole.Prompt
                    (
                        new SelectionPrompt<string>()
                        .Title("[cornflowerblue] Deseja adicionar um Funcionário? [/] ")
                        .AddChoices(new[] { "[green] Sim[/]", "[red] Não[/]" })
                    );

                if (opcao == "[green] Sim[/]")
                {
                    Console.WriteLine();

                    var funcionario = new Funcionario(nomeFormatado, emailFormatado, telefoneFormatado, dataAdmissao, endereco);
                    if (departamentoEscolhido != null)
                    {
                        departamentoEscolhido.AdicionarFuncionario(funcionario);
                        funcionario.Departamento = departamentoEscolhido;
                    }
                    FuncionarioService funcionarioService = new FuncionarioService();
                    funcionarioService.AdicionarFuncionario(funcionario);
                    AnsiConsole.Markup("[green] Funcionário adicionado com sucesso![/]");
                    Thread.Sleep(1000);
                    UsuarioListarView.ListarFuncionarios();
                }
                else
                {
                    Console.WriteLine();
                    AnsiConsole.Write(new Rule().RuleStyle("grey").Centered());
                    AnsiConsole.MarkupLine("[red] Funcionário não adicionado![/]");
                    Console.WriteLine();
                    Console.WriteLine();
                    Thread.Sleep(1000);
                    UsuarioListarView.ListarFuncionarios();
                }
            }
        }

        public static void EditarUsuario()
        {
            var usuarios = UsuarioService.ListarUsuarios();
            while (true)
            {
                AnsiConsole.Clear();
                AnsiConsole.Write(new Rule("[gold1]Editar Usuario[/]").RuleStyle("grey").Centered());
                Console.WriteLine();
                UsuarioListarView.TabelaUsuario();
                Console.WriteLine();
                AnsiConsole.Write(new Rule().RuleStyle("grey").Centered());
                Console.WriteLine();
                var opcao = AnsiConsole.Prompt
                    (
                        new SelectionPrompt<string>()
                        .Title("[gold1] Selecione uma opção: [/] ")
                        .AddChoices(usuarios.Keys)
                        .AddChoices(new[] { "[red] Voltar[/]" })
                    );
                if (opcao == "[red] Voltar[/]")
                {
                    UsuarioMenuView.MenuUsuario();
                    break;
                }
                var usuarioEscolhido = usuarios[opcao];
                var escolha = AnsiConsole.Prompt
                    (
                        new SelectionPrompt<string>()
                        .Title("[gold1] O que deseja editar? [/] ")
                        .AddChoices(new[]
                        {
                            "[cornflowerblue]1 -[/] Nome",
                            "[cornflowerblue]2 -[/] E-mail",
                            "[cornflowerblue]3 -[/] Telefone",
                            "[cornflowerblue]4 -[/] Endereço",
                            "[red]Voltar[/]"
                        })
                    );
                switch (escolha)
                {
                    case "[cornflowerblue]1 -[/] Nome":
                        {
                            var novoNome = AnsiConsole.Ask<string>(("[cornflowerblue] Novo Nome: [/] "));
                            var novoNomeFormatado = novoNome.Replace("[", "").Replace("]", "").Trim(); // Remove colchetes e espaços desnecessários
                            usuarioEscolhido.Nome = novoNomeFormatado;
                            Console.WriteLine();
                            AnsiConsole.Markup("[green] Nome editado com sucesso![/]");
                            Thread.Sleep(1000);
                            EditarUsuario(); break;

                        }
                    case "[cornflowerblue]2 -[/] E-mail":
                        {
                            var novoEmail = AnsiConsole.Ask<string>(("[cornflowerblue] Novo E-mail: [/] "));
                            var novoEmailFormatado = novoEmail.Replace("[", "").Replace("]", "").Trim(); // Remove colchetes e espaços desnecessários
                            usuarioEscolhido.Email = novoEmailFormatado;
                            Console.WriteLine();
                            AnsiConsole.Markup("[green] E-mail editado com sucesso![/]");
                            Thread.Sleep(1000);
                            EditarUsuario(); break;
                        }
                    case "[cornflowerblue]3 -[/] Telefone":
                        {
                            var novoTelefone = AnsiConsole.Ask<string>(("[cornflowerblue] Novo Telefone: [/] "));
                            var novoTelefoneFormatado = novoTelefone.Replace("[", "").Replace("]", "").Trim(); // Remove colchetes e espaços desnecessários
                            usuarioEscolhido.Telefone = novoTelefoneFormatado;
                            Console.WriteLine();
                            AnsiConsole.Markup("[green] Telefone editado com sucesso![/]");
                            Thread.Sleep(1000);
                            EditarUsuario(); break;
                        }
                    case "[cornflowerblue]4 -[/] Endereço":
                        {
                            AnsiConsole.Write(new Rule("[gold1]Endereço[/]").RuleStyle("grey").LeftJustified());
                            Console.WriteLine();
                            var novoEstado = AnsiConsole.Ask<string>(("[cornflowerblue] Novo Estado: [/] "));
                            var novoEstadoFormatado = novoEstado.Replace("[", "").Replace("]", "").Trim(); // Remove colchetes e espaços desnecessários
                            Console.WriteLine();
                            var novaCidade = AnsiConsole.Ask<string>(("[cornflowerblue] Nova Cidade: [/] "));
                            var novaCidadeFormatada = novaCidade.Replace("[", "").Replace("]", "").Trim(); // Remove colchetes e espaços desnecessários
                            Console.WriteLine();
                            var novaRua = AnsiConsole.Ask<string>(("[cornflowerblue] Nova Rua: [/] "));
                            var novaRuaFormatada = novaRua.Replace("[", "").Replace("]", "").Trim(); // Remove colchetes e espaços desnecessários;
                            Console.WriteLine();
                            var novoNumero = AnsiConsole.Ask<string>(("[cornflowerblue] Novo Número: [/] "));
                            var novoNumeroFormatado = novoNumero.Replace("[", "").Replace("]", "").Trim(); // Remove colchetes e espaços desnecessários
                            Console.WriteLine();
                            usuarioEscolhido.Endereco!.Estado = novoEstadoFormatado;
                            usuarioEscolhido.Endereco.Cidade = novaCidadeFormatada;
                            usuarioEscolhido.Endereco.Rua = novaRuaFormatada;
                            usuarioEscolhido.Endereco.Numero = novoNumeroFormatado;
                            Console.WriteLine();
                            AnsiConsole.Markup("[green] Endereço editado com sucesso![/]");
                            EditarUsuario(); break;
                        }
                }

            }
        }

        public static void DesativarUsuario()
        {
            var usuarios = UsuarioService.ListarUsuarios();
            while (true)
            {
                AnsiConsole.Clear();
                AnsiConsole.Write(new Rule("[gold1]Desativar Usuário[/]").RuleStyle("grey").Centered());
                Console.WriteLine();
                UsuarioListarView.TabelaUsuario();
                Console.WriteLine();
                AnsiConsole.Write(new Rule().RuleStyle("grey").Centered());
                Console.WriteLine();
                var opcao = AnsiConsole.Prompt
                    (
                        new SelectionPrompt<string>()
                        .Title("[gold1] Selecione uma opção: [/] ")
                        .AddChoices(usuarios.Keys)
                        .AddChoices(new[] { "[red] Voltar[/]" })
                    );
                if (opcao == "[red] Voltar[/]")
                {
                    UsuarioMenuView.MenuUsuario();
                    break;
                }
                var usuarioEscolhido = usuarios[opcao];
                usuarioEscolhido.Desativar(usuarioEscolhido);
                Console.WriteLine();
                AnsiConsole.Markup("[green] Usuário desativado com sucesso![/]");
                Thread.Sleep(1000);
                DesativarUsuario(); break;
            }
        }
        
        public static void AlterarDepartamentoUsuario()
        {
            var funcionarios = FuncionarioService.ListarFuncionarios();
            while (true)
            {
                AnsiConsole.Clear();
                var departamentoService = new DepartamentoService();
                var departamentos = departamentoService.ListarDepartamentos();
                AnsiConsole.Write(new Rule("[gold1]Alterar Departamento do Usuário[/]").RuleStyle("grey").Centered());
                Console.WriteLine();
                if (departamentos.Count == 0)
                {
                    AnsiConsole.MarkupLine("[red] Nenhum departamento encontrado![/]");
                    Console.WriteLine();
                    Console.WriteLine();
                    Thread.Sleep(1000);
                    UsuarioMenuView.MenuUsuario();
                    break;
                }
                UsuarioListarView.TabelaFuncionarios();
                Console.WriteLine();
                AnsiConsole.Write(new Rule().RuleStyle("grey").Centered());
                Console.WriteLine();
                var opcao = AnsiConsole.Prompt
                    (
                        new SelectionPrompt<string>()
                        .Title("[gold1] Selecione o funcionario: [/] ")
                        .AddChoices(funcionarios.Keys)
                        .AddChoices(new[] { "[red] Voltar[/]" })
                    );
                if (opcao == "[red] Voltar[/]")
                {
                    UsuarioMenuView.MenuUsuario();
                    break;
                }
                var funcionarioEscolhido = funcionarios[opcao];
                var departamentoEscolhido = AnsiConsole.Prompt
                    (
                        new SelectionPrompt<string>()
                        .Title("[gold1] Selecione o novo departamento: [/] ")
                        .AddChoices(departamentos.Keys)
                    );
                funcionarioEscolhido.Departamento = departamentos[departamentoEscolhido];
                Console.WriteLine();
                AnsiConsole.Markup("[green] Departamento alterado com sucesso![/]");
                Thread.Sleep(1000);
            }
        }
    }
}
