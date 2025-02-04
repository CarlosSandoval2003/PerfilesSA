using Microsoft.AspNetCore.Mvc;
using PerfilesSA2.Models;
using System.Linq;
using PerfilesSA2.Data;
using Microsoft.EntityFrameworkCore;

namespace PerfilesSA2.Controllers
{
    public class EmpleadosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmpleadosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Acción para mostrar la lista de empleados
        public IActionResult Index()
        {
            var empleados = _context.Empleados.ToList();
            return View(empleados);
        }

        // Acción para crear un nuevo empleado (GET)
        public IActionResult Create()
        {
            ViewBag.Departamentos = _context.Departamentos.Where(d => d.Estado == true).ToList();
            return View();
        }


        // Acción para crear un nuevo empleado (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Empleado empleado)
        {
            if (ModelState.IsValid)
            {
                empleado.Estado = true;
                _context.Empleados.Add(empleado);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Departamentos = _context.Departamentos.Where(d => d.Estado == true).ToList();
            return View(empleado);
        }

        // Acción para editar un empleado (GET)
        public IActionResult Edit(int id)
        {
            var empleado = _context.Empleados.Find(id);
            if (empleado == null)
            {
                return NotFound();
            }
            ViewBag.Departamentos = _context.Departamentos.Where(d => d.Estado == true).ToList();
            return View(empleado);
        }

        // Acción para editar un empleado (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Empleado empleado)
        {
            if (id != empleado.EmpleadoID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var empleadoOriginal = _context.Empleados.AsNoTracking().FirstOrDefault(d => d.EmpleadoID == id);
                if (empleadoOriginal != null)
                {
                    empleado.Estado = empleadoOriginal.Estado; 
                }
                _context.Update(empleado);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Departamentos = _context.Departamentos.Where(d => d.Estado == true).ToList();
            return View(empleado);
        }

        public IActionResult Reporte(DateTime? fechaInicio, DateTime? fechaFin, int? departamentoId)
        {
            var departamentos = _context.Departamentos.ToList();
            ViewBag.Departamentos = departamentos;

            var empleados = from e in _context.Empleados
                            join d in _context.Departamentos on e.DepartamentoID equals d.DepartamentoID
                            where (!fechaInicio.HasValue || e.FechaIngreso >= fechaInicio) &&
                                  (!fechaFin.HasValue || e.FechaIngreso <= fechaFin) &&
                                  (!departamentoId.HasValue || e.DepartamentoID == departamentoId)
                            select new ReporteEmpleado
                            {
                                Nombre = e.Nombres,
                                DPI = e.DPI,
                                FechaNacimiento = e.FechaNacimiento,
                                FechaIngreso = e.FechaIngreso,
                                Genero = e.Genero,
                                NombreDepartamento = d.Nombre,
                                Estado = e.Estado 
                            };

            var reporte = empleados.ToList();

            // Agrupar por departamento
            var resultadoAgrupado = reporte.GroupBy(emp => emp.NombreDepartamento)
                                           .Select(g => new
                                           {
                                               Departamento = g.Key,
                                               Empleados = g.ToList()
                                           });

            return View(resultadoAgrupado);
        }


    }
}
