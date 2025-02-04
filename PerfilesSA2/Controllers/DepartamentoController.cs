using Microsoft.AspNetCore.Mvc;
using PerfilesSA2.Models;
using System.Linq;
using PerfilesSA2.Data;
using Microsoft.EntityFrameworkCore;

namespace PerfilesSA2.Controllers
{
    public class DepartamentosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DepartamentosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Acción para mostrar la lista de departamentos
        public IActionResult Index()
        {
            var departamentos = _context.Departamentos.ToList();
            return View(departamentos);
        }

        // Acción para crear un nuevo departamento (GET)
        public IActionResult Create()
        {
            return View();
        }

        // Acción para crear un nuevo departamento (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Departamento departamento)
        {
            if (ModelState.IsValid)
            {
                departamento.Estado = true; // Habilitado por defecto
                _context.Departamentos.Add(departamento);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(departamento);
        }

        // Acción para editar un departamento (GET)
        public IActionResult Edit(int id)
        {
            var departamento = _context.Departamentos.Find(id);
            if (departamento == null)
            {
                return NotFound();
            }
            return View(departamento);
        }

        // Acción para editar un departamento (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Departamento departamento)
        {
            if (id != departamento.DepartamentoID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                // Obtener el departamento original para conservar su estado
                var departamentoOriginal = _context.Departamentos.AsNoTracking().FirstOrDefault(d => d.DepartamentoID == id);
                if (departamentoOriginal != null)
                {
                    departamento.Estado = departamentoOriginal.Estado; // Conservar el estado original
                }

                _context.Update(departamento);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(departamento);
        }



        // Acción para habilitar/deshabilitar un departamento
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Toggle(int id)
        {
            var departamento = _context.Departamentos.Find(id);
            if (departamento != null)
            {
                departamento.Estado = !departamento.Estado; // Cambiar el estado
                _context.Update(departamento);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
