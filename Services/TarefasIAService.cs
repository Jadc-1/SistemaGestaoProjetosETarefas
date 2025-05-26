using SistemaGestaoProjetosETarefas.Domain;
using Spectre.Console;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SistemaGestaoProjetosETarefas.Services
{
    
    public class RespostaTarefas
    {
        public List<Tarefa>? Tarefas { get; set; }
    }

    public class TarefasIAService
    {
        public static async Task<List<Tarefa>> CriarTarefasIAAsync(Projeto projeto)
        {
            var httpClient = new HttpClient();
            var apiKey = File.ReadAllText("tarefa.txt").Trim();
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", apiKey); //Autoriza utilizar a API, por meio da APIKey
            httpClient.DefaultRequestHeaders.Add("HTTP-Referer", "http://localhost");
            httpClient.DefaultRequestHeaders.Add("X-Title", "ProjetoGestaoTarefas");

            var prompt = GerarPromptDoProjeto(projeto);

            var requestBody = new
            {
                model = "gpt-3.5-turbo",
                messages = new[]
                {
                    new { role = "system", content = "Você é um assistente que gera tarefas para um projeto" }, // Mensagem padrão para o assistente
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
                var resultado = JsonSerializer.Deserialize<List<Tarefa>>(resposta!); // Deserializa a resposta em uma lista de tarefas
                return resultado!;
            }
            else
            {
                throw new Exception($"Erro: {response.StatusCode} - {response.ReasonPhrase}");
            }
        }

        public static string GerarPromptDoProjeto(Projeto projeto)
        {
            var sb = new StringBuilder();
            sb.AppendLine("Você é um assistente que gera tarefas para projetos em Português.");
            sb.AppendLine($"Gere uma lista de tarefas para o projeto \"{projeto.Nome}\". Descrição do projeto: {projeto.Desc}");
            sb.AppendLine("Traga apenas isso no json, não traga mais nada, nem mesmo o nome do projeto.");
            sb.AppendLine("Apenas traga as tarefas, não traga o gestor, nem o status do projeto.");
            sb.AppendLine("Me retorne apenas uma lista de objetos JSON com as propriedades NomeTarefa, DescricaoTarefa e Prioridade, sem nenhum campo externo como tarefas. Apenas a lista direta.");
            sb.AppendLine("Lembre-se, não mude a chave, tem que ser o que eu definir aqui");
            sb.AppendLine("Siga esse exemplo: ");
            sb.AppendLine(
                " {\r\n    \"NomeTarefa\": \"Criar diagrama de fluxo\",\r\n    \"DescricaoTarefa\": \"Desenvolver um diagrama de fluxo detalhado para o projeto\",\r\n    \"Prioridade\": \"A(apenas A, B, C, D)\"\r\n  },"
                );
            return sb.ToString();
        }
    }
}

