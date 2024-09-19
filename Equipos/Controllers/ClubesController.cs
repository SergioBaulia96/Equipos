
using Equipos.Data;
using Equipos.Models;
using Microsoft.AspNetCore.Mvc;

namespace Equipos.Controllers;

public class ClubesController : Controller
{
    private ApplicationDbContext _context;

    public ClubesController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Clubes()
    {
        return View();
    }

    public JsonResult ListadoClubes(int? clubID)
    {
        var listadoClubes = _context.Clubes.ToList();
            listadoClubes = _context.Clubes.OrderBy(l => l.NombreClub).ToList();

        if(clubID != null)
        {
            listadoClubes = _context.Clubes.Where(l => l.ClubID == clubID).ToList();
        }
        return Json(listadoClubes);
    }

        public JsonResult GuardarClub (int clubID, string nombreClub, string pais)
    {
        string resultado = "";

        nombreClub = nombreClub.ToUpper();
        pais = pais.ToUpper();

        if(clubID == 0)
        {
            var nuevoClub = new Club
            {
                NombreClub = nombreClub,
                Pais = pais
            };
            _context.Add(nuevoClub);
            _context.SaveChanges();
            resultado = "Club Guardado";
        }
        else
        {
            var editarClub = _context.Clubes.Where(e => e.ClubID == clubID).SingleOrDefault();
            
            if(editarClub != null)
            {
                editarClub.NombreClub = nombreClub;
                editarClub.Pais = pais;
                _context.SaveChanges();
                resultado = "Club Editado";
            }
        }
        return Json(resultado);
    }

    public JsonResult EliminarClub(int clubID)
    {
        var eliminarClub = _context.Clubes.Find(clubID);
        _context.Remove(eliminarClub);
        _context.SaveChanges();

        return Json(eliminarClub);
    }
}
