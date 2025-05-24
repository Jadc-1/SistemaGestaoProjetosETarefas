using SistemaGestaoProjetosETarefas.Domain;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SistemaGestaoProjetosETarefas.Services
{
    public class RelatorioService
    {
        public static async Task<string?> GerarRelatorioIAAsync(Projeto projetoSelecionado)
        {
            var httpClient = new HttpClient();
            var apiKey = File.ReadAllText("relatorio.txt").Trim();
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", apiKey); //Autoriza utilizar a API, por meio da APIKey
            httpClient.DefaultRequestHeaders.Add("HTTP-Referer", "http://localhost"); 
            httpClient.DefaultRequestHeaders.Add("X-Title", "ProjetoGestaoTarefas");

            var prompt = GerarPromptDoProjeto(projetoSelecionado);

            var requestBody = new
            {
                model = "gpt-3.5-turbo",
                messages = new[]
                {
                    new { role = "system", content = "Você é um assistente que gera relatórios técnicos de projetos" }, // Mensagem padrão para o assistente
                    new { role = "user", content = prompt }
                },
            };

            var json = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json"); // Cria o conteúdo da requisição em JSON
            var response = await httpClient.PostAsync("https://openrouter.ai/api/v1/chat/completions", content); // Envia a requisição para a API
            var responseJson = await response.Content.ReadAsStringAsync(); // Lê a resposta da API

            if (response.IsSuccessStatusCode)
            {
                var responseData = JsonSerializer.Deserialize<JsonElement>(responseJson);
                var resposta = responseData.GetProperty("choices")[0].GetProperty("message").GetProperty("content").GetString(); // Pega a resposta da API
                return resposta;
            }
            else
            {
                var erro = $"Erro: {response.StatusCode} - {response.ReasonPhrase}"; // Exibe o erro caso ocorra
                return erro;
            }
        }

        public static string GerarPromptDoProjeto(Projeto projeto)
        {
            var sb = new StringBuilder();
            sb.AppendLine("Você é um assistente que gera relatórios técnicos e dissertativos sobre a execução de projetos em Português.");
            sb.AppendLine($"Gere um relatório completo, profissional e bem estruturado sobre o projeto \"{projeto.Nome}\". Descrição do projeto: {projeto.Desc}");
            sb.AppendLine("O relatório deve destacar o progresso, os responsáveis, possíveis atrasos, sugestões de melhoria e impacto do desempenho da equipe.");
            sb.AppendLine("O objetivo é apresentar esse relatório para uma reunião de avaliação de desempenho da equipe de projetos.");
            sb.AppendLine($"O projeto está no status: {projeto.StatusProjeto!.Categoria} e a prioridade do projeto é: {projeto.Prioridade}.");
            sb.AppendLine("Não traga nenhuma informação em formato de tabelas, pois quebra na hora de criar um PDF com o assunto");
            sb.AppendLine("Não invente nenhuma informação sobre o projeto, se está faltando algo, apenas diga que não tem essa informação ou que possa melhorar, mesmo que seja sem funcionário");
            sb.AppendLine("Abaixo estão as tarefas e o Gestor:");
            sb.AppendLine("Se ja concluiu não inicie a introdução novamente, apenas termine o relatorio!");
            if (projeto.GestorDelegado != null)
                sb.AppendLine($"Gestor: {projeto.GestorDelegado.Nome}");

            foreach (var tarefa in projeto.Tarefas!)
                sb.AppendLine($"Tarefa: {tarefa.NomeTarefa} - Descrição da tarefa: {tarefa.DescricaoTarefa} - Responsável pela tarefa: {tarefa.FuncionarioDelegado} - Status da tarefa: {tarefa.StatusTarefa!.Categoria!} - Prioridade da Tarefa: {tarefa.Prioridade}");
            sb.AppendLine($"Data de início: {projeto.DataInicio.ToString("dd/MM/yyyy")}");
            if (projeto.DataTermino != default)
                sb.AppendLine($"Data de término: {projeto.DataTermino.ToString("dd/MM/yyyy")}");

            return sb.ToString();
        }
    }
}
