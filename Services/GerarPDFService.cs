using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spectre.Console;
using iTextSharp;
using iTextSharp.text;
using iTextSharpParagraph = iTextSharp.text.Paragraph;
using iTextSharp.text.pdf;


namespace SistemaGestaoProjetosETarefas.Services
{
    public class GerarPDFService
    {
        public static void GerarPDF(string conteudo, string nomeArquivo, string projetoNome)
        {
            try
            {
                string caminhoAtual = Directory.GetCurrentDirectory();
                string caminhoPdf = Path.Combine(caminhoAtual, nomeArquivo); // Define o caminho do arquivo PDF

                FileStream arquivoPDF = new FileStream(caminhoPdf, FileMode.Create); // Cria o arquivo PDF no caminho especificado
                Document doc = new Document(PageSize.A4); // Cria um novo documento PDF com tamanho A4  
                PdfWriter escritorPDF = PdfWriter.GetInstance(doc, arquivoPDF); // Cria um escritor PDF para o documento

                doc.Open(); // Abre o documento para escrita

                Font fonteTitulo = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16); // Fonte em negrito, tamanho 16
                iTextSharpParagraph titulo = new iTextSharpParagraph($"Relatório Técnico: {projetoNome} - Relatório de Avaliação de Desempenho da Equipe", fonteTitulo);
                titulo.Alignment = Element.ALIGN_CENTER; // Alinha o título ao centro do documento
                titulo.SpacingAfter = 15;
                doc.Add(titulo); // Adiciona o título ao documento


                Font fonteNormal = FontFactory.GetFont(FontFactory.HELVETICA, 12); // Fonte normal, tamanho 12
                iTextSharpParagraph paragrafo = new iTextSharpParagraph(conteudo, fonteNormal); // Cria um novo parágrafo para o conteúdo do PDF
                paragrafo.Add(conteudo); // Adiciona o conteúdo ao parágrafo

                
                doc.Add(paragrafo); // Adiciona o parágrafo ao documento
                doc.Close(); // Fecha o documento

                Console.WriteLine();
                AnsiConsole.Markup($"[green] Arquivo PDF gerado com sucesso[/]");
                Console.WriteLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro {ex.Message}");
            }
        }
    }
}
