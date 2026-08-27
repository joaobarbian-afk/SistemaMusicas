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

            // GET: Musicas/Details/5
            public async Task<IActionResult> Details(int? id)
            {
                if (id == null) return NotFound();

                var musica = await _context.Musicas.FirstOrDefaultAsync(m => m.Id == id);
                if (musica == null) return NotFound();

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
                // DEFINE A SENHA AQUI:
                string senhaCorreta = "123456";

                if (senhaAdmin != senhaCorreta)
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
}