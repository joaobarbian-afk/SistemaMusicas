using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            var musicas = await _context.Musicas
                .AsNoTracking()
                .OrderByDescending(m => m.DataCadastro)
                .ToListAsync();

            return View(musicas);
        }

        // GET: Musicas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var musica = await _context.Musicas
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);

            if (musica == null)
                return NotFound();

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
        public async Task<IActionResult> Create(Musica musica)
        {
            if (!ModelState.IsValid)
                return View(musica);

            musica.DataCadastro = DateTime.Now;

            _context.Musicas.Add(musica);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: Musicas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var musica = await _context.Musicas.FindAsync(id);

            if (musica == null)
                return NotFound();

            return View(musica);
        }

        // POST: Musicas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Musica musica)
        {
            if (id != musica.Id)
                return NotFound();

            if (!ModelState.IsValid)
                return View(musica);

            try
            {
                var musicaBanco = await _context.Musicas.FindAsync(id);

                if (musicaBanco == null)
                    return NotFound();

                musicaBanco.NomeAluno = musica.NomeAluno;
                musicaBanco.NomeMusica = musica.NomeMusica;
                musicaBanco.Autor = musica.Autor;
                musicaBanco.LinkYoutube = musica.LinkYoutube;

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Musicas.AnyAsync(m => m.Id == id))
                    return NotFound();

                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Musicas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var musica = await _context.Musicas
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);

            if (musica == null)
                return NotFound();

            return View(musica);
        }

        // POST: Musicas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var musica = await _context.Musicas.FindAsync(id);

            if (musica != null)
            {
                _context.Musicas.Remove(musica);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Musicas/Hoje
        public async Task<IActionResult> Hoje()
        {
            var hoje = DateTime.Today;
            var amanha = hoje.AddDays(1);

            var musicas = await _context.Musicas
                .AsNoTracking()
                .Where(m => m.DataCadastro >= hoje && m.DataCadastro < amanha)
                .OrderByDescending(m => m.DataCadastro)
                .ToListAsync();

            return View(musicas);
        }
    }
}