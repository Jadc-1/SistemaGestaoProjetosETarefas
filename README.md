# Sistema de Gestão de Projetos e Tarefas em console (EM ANDAMENTO)

Este é um sistema em C# para gerenciamento de projetos e tarefas, com geração automática de tarefas usando inteligência artificial (IA), desenvolvido para um trabalho da faculdade de Análise e Desenvolvimento.

## Funcionalidades que serão implementadas:

- **Cadastro de Projetos**: Criação de novos projetos com título e descrição.
- **Cadastro de Tarefas**: Adição de tarefas dentro de projetos.
- **Controle de Status de Tarefas**: Marcação de tarefas como 'Pendentes', 'Em andamento' ou 'Concluídas'.
- **Exibição de Projetos e Tarefas**: Visualização dos projetos e suas respectivas tarefas.
- **Cadastro de Funcionário e Gestor**
- **Criar tarefas por meio de uma IA**: Enviar o nome do projeto e a IA criará as tarefas principais necessárias para o seu desenvolvimento
- **Gerar relatórios por meio de uma IA**: De acordo com os projetos, tarefas realizadas a IA desenvolverá um relatório do projeto, bem como dos funcionários envolvidos.

## Tecnologias

- **C# (.NET)**: Linguagem utilizada para o desenvolvimento do sistema.
- **Spectre.Console**: Interface de interação com o usuário no terminal/console.
- **API de IA**: OpenRouter
- **iTextSharp**: Gerar PDF

## Como Usar

1. Clone este repositório:
   ```bash
   git clone [https://github.com/Jadc-1/SistemaGestaoProjetosETarefas]
   cd SistemaGestaoProjetosETarefas
   
2. Restaure o pacote Nuget
   ```bash
   dotnet restore

3. Execute o projeto
   ```bash
   dotnet run

## ⚠️ Requisitos

- NET 7 ou superior instalado
-Conexão com a internet (para uso da IA)
