using SistemaGestaoProjetosETarefas.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestaoProjetosETarefas.Services
{
    public class UsuarioService
    {
        public static Dictionary<string, Usuario> ListarUsuarios()
        {
            var funcionarios = FuncionarioService.ListarFuncionarios().Values;
            var gestores = GestorService.ListarGestores().Values;
            var dicionarioUsuario = new Dictionary<string, Usuario>();
            foreach (var gestor in gestores)
            {
                var chave = $"[cornflowerblue] Gestor {gestor.IdGestor}:[/] {gestor.Nome}";
                dicionarioUsuario.Add(chave, gestor);
            }
            foreach (var funcionario in funcionarios)
            {
                var chave = $"[cornflowerblue] Funcionário {funcionario.IdFuncionario}:[/] {funcionario.Nome}";
                dicionarioUsuario.Add(chave, funcionario);
            }
            return dicionarioUsuario;
        }
    }
}
