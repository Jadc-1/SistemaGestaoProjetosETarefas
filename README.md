# Sistema de Gestão de Projetos e Tarefas em console 

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

4. **Exclua os arquivos tarefas.txt e relatorio.txt**
      É preciso excluir os dois arquivos para que seja possível rodar o projeto. Logo abaixo terá um passo a passo de como criá-los novamente para ter acesso ao uso das APIs.
   

## COMO UTILIZAR AS FUNCIONALIDADES DAS APIs 

   1- Crie duas **APIKEY** no link abaixo e salve-as em um local para usar posteriomente (é preciso realizar o cadastro no site da OpenRouter):
       https://openrouter.ai/settings/keys

   2- Exclua os arquivos tarefas.txt e relatorio.txt

   3- Crie os dois arquivos novamente, e salve uma apiKey em cada arquivo.

   4- Pronto, agora será possível utilizar as funcionalidades das APIs

   5- As requisições para o modelo de API que estamos utilizando é pago, porém caso queria utilizar um modelo free basta apenas modificar o model nos arquivos TarefasAIService e RelatorioService: 


   ![modelIA](https://github.com/user-attachments/assets/c0ecdc2d-8036-4c22-b394-91b24ace864e)

   Modifique o model para: **mistralai/mistral-7b-instruct:free**
   
## ⚠️ Requisitos

- NET 7 ou superior instalado

- Conexão com a internet (para uso da IA)
