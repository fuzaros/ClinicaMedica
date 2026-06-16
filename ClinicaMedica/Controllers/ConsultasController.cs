
using ClinicaMedica.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

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
}
