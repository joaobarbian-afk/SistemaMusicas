using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaMusicas.Data;
using SistemaMusicas.Models;

namespace SistemaMusicas.Controllers
{
    public class MusicasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MusicasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Musicas
        public async Task<IActionResult> Index()
        {
            return View(await _context.Musicas.ToListAsync());
        }

        // GET: Musicas/Hoje
        public async Task<IActionResult> Hoje()
        {
            var hoje = DateTime.Today;
            var musicasHoje = await _context.Musicas
                .Where(m => m.DataCadastro.Date == hoje)
                .ToListAsync();

            return View(musicasHoje);
        }

        // GET: Musicas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var musica = await _context.Musicas.FindAsync(id);
            if (musica == null) return NotFound();

            return View(musica);
        }

        // POST: Musicas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NomeAluno,NomeMusica,Autor,LinkYoutube,DataCadastro")] Musica musica)
        {
            if (id != musica.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(musica);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Musicas.Any(e => e.Id == musica.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(musica);
        }

        // GET: Musicas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Musicas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NomeAluno,NomeMusica,Autor,LinkYoutube,DataCadastro")] Musica musica)
        {
            if (ModelState.IsValid)
            {
                musica.DataCadastro = DateTime.Now;
                _context.Add(musica);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(musica);
        }

        // GET: Musicas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var musica = await _context.Musicas.FirstOrDefaultAsync(m => m.Id == id);
            if (musica == null) return NotFound();

            return View(musica);
        }

        // POST: Musicas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, string senhaAdmin)
        {
            string senhaCorreta = "123456";

            if (string.IsNullOrEmpty(senhaAdmin) || senhaAdmin != senhaCorreta)
            {
                TempData["ErroSenha"] = "Senha incorreta! A música não foi excluída.";
                return RedirectToAction(nameof(Delete), new { id });
            }

            var musica = await _context.Musicas.FindAsync(id);
            if (musica != null)
            {
                _context.Musicas.Remove(musica);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}