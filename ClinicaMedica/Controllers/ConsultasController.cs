using ClinicaMedica.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using ClosedXML.Excel;
using System.IO;

public class ConsultasController : Controller
{
    private readonly ClinicaMedicaContext _context;

    public ConsultasController(ClinicaMedicaContext context)
    {
        _context = context;
    }

    // GET: CONSULTAS
    public async Task<IActionResult> Index()    
    {
        var consultas = await _context.Consulta
                                  .Include(c => c.Paciente)
                                  .ToListAsync();

        return View(consultas);
    }

    // GET: CONSULTAS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var consulta = await _context.Consulta
            .Include(c => c.Paciente)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (consulta == null)
        {
            return NotFound();
        }

        return View(consulta);
    }

    // GET: CONSULTAS/Create
    public IActionResult Create()
    {
        ViewData["PacienteId"] = new SelectList(_context.Paciente, "Id", "Nome");
        return View();
    }

    // POST: CONSULTAS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,PacienteId,Especialidade,NomeMedico,DataHora,Observacoes")] Consulta consulta)
    {
        if (ModelState.IsValid)
        {
            _context.Add(consulta);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(consulta);
    }

    // GET: CONSULTAS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var consulta = await _context.Consulta.FindAsync(id);
        if (consulta == null)
        {
            return NotFound();
        }
        return View(consulta);
    }

    // POST: CONSULTAS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,PacienteId,Paciente,NomeMedico,DataHora,Observacoes")] Consulta consulta)
    {
        if (id != consulta.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(consulta);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ConsultaExists(consulta.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(consulta);
    }

    // GET: CONSULTAS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var consulta = await _context.Consulta
            .FirstOrDefaultAsync(m => m.Id == id);
        if (consulta == null)
        {
            return NotFound();
        }

        return View(consulta);
    }

    // POST: CONSULTAS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var consulta = await _context.Consulta.FindAsync(id);
        if (consulta != null)
        {
            _context.Consulta.Remove(consulta);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool ConsultaExists(int? id)
    {
        return _context.Consulta.Any(e => e.Id == id);
    }
    //GET: CONSULTAS/GeneratePDF/5
    public async Task<IActionResult> GerarProntuario(int? id)
    {
        if(id == null) return NotFound();

        var consulta = await _context.Consulta
            .Include(c => c.Paciente)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (consulta == null) return NotFound();

        QuestPDF.Settings.License = LicenseType.Community;

        // Criando o documento PDF em memória
        var pdf = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(2, Unit.Centimetre);
                page.PageColor(Colors.White);
                page.DefaultTextStyle(x => x.FontSize(12).FontColor(Colors.Grey.Darken3));

                // CABEÇALHO DO PDF
                page.Header()
                    .Row(row =>
                    {
                        row.RelativeItem().Column(column =>
                        {
                            column.Item().Text("CLÍNICA MÉDICA GOMES").FontSize(20).Bold().FontColor(Colors.Blue.Medium);
                            column.Item().Text("Prontuário de Atendimento e Resumo da Consulta").FontSize(10).Italic();
                        });
                    });

                // CONTEÚDO DO PRONTUÁRIO
                page.Content()
                    .PaddingTop(1, Unit.Centimetre)
                    .Column(x =>
                    {
                        x.Spacing(10);

                        // Seção Paciente
                        x.Item().Text("Dados do Paciente").FontSize(14).Bold().FontColor(Colors.Blue.Darken2);
                        x.Item().LineHorizontal(1).LineColor(Colors.Grey.Lighten2);
                        x.Item().Text($"Nome: {consulta.Paciente?.Nome}");
                        x.Item().Text($"Especialidade Requisitada: {consulta.Especialidade}");

                        x.Item().PaddingTop(15);

                        // Seção Atendimento
                        x.Item().Text("Detalhes Médicos").FontSize(14).Bold().FontColor(Colors.Blue.Darken2);
                        x.Item().LineHorizontal(1).LineColor(Colors.Grey.Lighten2);
                        x.Item().Text($"Médico Responsável: Dr(a). {consulta.NomeMedico}");
                        x.Item().Text($"Data/Horário: {consulta.DataHora:dd/MM/yyyy HH:mm}");

                        x.Item().PaddingTop(15);

                        // Seção Observações
                        x.Item().Text("Observações Clínicas e Sintomas").FontSize(14).Bold().FontColor(Colors.Blue.Darken2);
                        x.Item().LineHorizontal(1).LineColor(Colors.Grey.Lighten2);
                        x.Item().Text(consulta.Observacoes ?? "Nenhuma observação registrada.").Italic();
                    });

                // RODAPÉ DO PDF
                page.Footer()
                    .AlignCenter()
                    .Text(x =>
                    {
                        x.CurrentPageNumber();
                        x.Span(" / ");
                        x.TotalPages();
                    });
            });
        });

        // Converte o PDF gerado em Array de Bytes para download
        var pdfBytes = pdf.GeneratePdf();
        string nomeArquivo = $"Prontuario_{consulta.Paciente?.Nome?.Replace(" ", "_")}_{consulta.Id}.pdf";

        return File(pdfBytes, "application/pdf", nomeArquivo);
    }
    // GET: Consultas/ExportarExcel
    public async Task<IActionResult> ExportarExcel()
    {
        // 1. Busca todas as consultas trazendo o Nome do Paciente grudado (JOIN)
        var consultas = await _context.Consulta
                                      .Include(c => c.Paciente)
                                      .ToListAsync();

        // 2. Cria a estrutura da planilha Excel
        using (var workbook = new XLWorkbook())
        {
            var worksheet = workbook.Worksheets.Add("Consultas");

            // 3. Cria o Cabeçalho da tabela (Linha 1)
            worksheet.Cell(1, 1).Value = "Paciente";
            worksheet.Cell(1, 2).Value = "Especialidade Médica";
            worksheet.Cell(1, 3).Value = "Médico";
            worksheet.Cell(1, 4).Value = "Data e Horário";
            worksheet.Cell(1, 5).Value = "Observações/Sintomas";

            // Estilização do Cabeçalho (Negrito com fundo azul escuro e texto branco)
            var cabecalho = worksheet.Range("A1:E1");
            cabecalho.Style.Font.Bold = true;
            cabecalho.Style.Fill.BackgroundColor = XLColor.FromHtml("#1F4E78");
            cabecalho.Style.Font.FontColor = XLColor.White;
            cabecalho.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            // 4. Alimenta as linhas de dados (começando na Linha 2)
            int linhaAtual = 2;
            foreach (var item in consultas)
            {
                worksheet.Cell(linhaAtual, 1).Value = item.Paciente?.Nome ?? "Não informado";
                worksheet.Cell(linhaAtual, 2).Value = item.Especialidade;
                worksheet.Cell(linhaAtual, 3).Value = item.NomeMedico;
                worksheet.Cell(linhaAtual, 4).Value = item.DataHora.ToString("dd/MM/yyyy HH:mm");
                worksheet.Cell(linhaAtual, 5).Value = item.Observacoes ?? "";

                // Alinhamento centralizado para a coluna de Data
                worksheet.Cell(linhaAtual, 4).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                linhaAtual++;
            }

            // 5. Ajusta automaticamente a largura das colunas de acordo com o tamanho do texto
            worksheet.Columns().AdjustToContents();

            // 6. Converte para Array de Bytes e manda para o navegador fazer o download
            using (var stream = new MemoryStream())
            {
                workbook.SaveAs(stream);
                var content = stream.ToArray();

                string nomeArquivo = $"Relatorio_Consultas_{DateTime.Now:ddMMyyyy_HHmm}.xlsx";

                return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", nomeArquivo);
            }
        }
    }

}
