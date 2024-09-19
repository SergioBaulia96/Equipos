
using Equipos.Models;
using Equipos.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Equipos.Controllers;

public class JugadoresController : Controller
{
    private ApplicationDbContext _context;

    public JugadoresController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Jugadores()
    {
        var selectListItems = new List<SelectListItem>
        {
            new SelectListItem { Value = "0", Text = "[POSICION...]"}
        };

        var enumValues = Enum.GetValues(typeof(Posicion)).Cast<Posicion>();

        selectListItems.AddRange(enumValues.Select(p => new SelectListItem
        {
            Value = p.GetHashCode().ToString(),
            Text = p.ToString().ToUpper()
        }));

        ViewBag.Posicion = selectListItems.OrderBy(t => t.Text).ToList();

        var clubes = _context.Clubes.ToList();
        clubes.Add(new Club { ClubID = 0, NombreClub = "[CLUB...]" });
        ViewBag.ClubID = new SelectList(clubes.OrderBy(c => c.NombreClub), "ClubID", "NombreClub");

        return View();
    }


    public JsonResult ListaJugadores(int? JugadorID)
    {
        List<VistaJugadores> jugadoresMostar = new List<VistaJugadores>();
        
        var jugadores = _context.Jugadores.ToList();
            jugadores = jugadores.OrderBy(j => j.NombreJugador).ToList();

        if (JugadorID != null)
        {
            jugadores = jugadores.Where(t => t.JugadorID == JugadorID).ToList();
        }

        var clubes = _context.Clubes.ToList();

        foreach (var j in jugadores)
        {
            var club = clubes.Where(t => t.ClubID == j.ClubID).Single();

            var jugadorMostrar = new VistaJugadores
            {
                JugadorID = j.JugadorID,
                ClubID = j.ClubID,
                NombreJugador = j.NombreJugador,
                NombreClub = club.NombreClub,
                Posicion = Enum.GetName(typeof(Posicion), j.Posicion),
                Edad = j.Edad,
                Nacionalidad = j.Nacionalidad,
                Ingreso = j.Ingreso.ToString("dd/MM/yyyy")
            };
            jugadoresMostar.Add(jugadorMostrar);
        }

        return Json(jugadoresMostar);
    }

    public JsonResult TraerJugadoresAlModal(int? JugadorID)
    {
        var jugadoresPorId = _context.Jugadores.ToList();

        if (JugadorID != null)
        {
            jugadoresPorId = jugadoresPorId.Where(t => t.JugadorID == JugadorID).ToList();
        }
        return Json(jugadoresPorId.ToList());
    }

        public JsonResult Guardarjugador(
        int JugadorID,
        int ClubID,
        string NombreJugador,
        Posicion Posicion,
        int Edad,
        string Nacionalidad,
        DateTime Ingreso)
    {
        string resultado = "";

        NombreJugador = NombreJugador.ToUpper();
        Nacionalidad = Nacionalidad.ToUpper();
        if (JugadorID == 0)
        {
            if (ClubID > 0)
            {
                var jugador = new Jugador
                {
                    JugadorID = JugadorID,
                    ClubID = ClubID,
                    NombreJugador = NombreJugador,
                    Posicion = Posicion,
                    Edad = Edad,
                    Nacionalidad = Nacionalidad,
                    Ingreso = Ingreso
                };
                _context.Add(jugador);
                _context.SaveChanges();
            }
        }
        else
        {
            var editarJugador = _context.Jugadores.Where(e => e.JugadorID == JugadorID).SingleOrDefault();
            if (editarJugador != null)
            {
                editarJugador.JugadorID = JugadorID;
                editarJugador.ClubID = ClubID;
                editarJugador.NombreJugador = NombreJugador;
                editarJugador.Posicion = Posicion;
                editarJugador.Edad = Edad;
                editarJugador.Nacionalidad = Nacionalidad;
                editarJugador.Ingreso = Ingreso;
                _context.SaveChanges();
            }
        }
        return Json(resultado);
    }

        public JsonResult EliminarJugador(int JugadorID)
    {
        var jugador = _context.Jugadores.Find(JugadorID);
        _context.Remove(jugador);
        _context.SaveChanges();

        return Json(true);
    }

    public IActionResult InformeClubes()
    {
        return View();
    }

    public JsonResult ListadoInformes (int? id, DateTime? BuscarDesde, DateTime? BuscarHasta)
    {
        List<VistaJugadores> clubesJugadoresMostrar = new List<VistaJugadores>();

        var clubJugadores = _context.Jugadores.AsQueryable();

        if (id != null)
        {
            clubJugadores = clubJugadores.Where(e => e.JugadorID == id);
        }

        if (BuscarDesde != null && BuscarHasta != null)
        {
            clubJugadores = clubJugadores.Where(e => e.Ingreso >= BuscarDesde && e.Ingreso <= BuscarHasta);
        }

        clubJugadores = clubJugadores.OrderBy(e => e.Edad);

        var clubes = _context.Clubes.ToList();

        foreach (var clubJugador in clubJugadores)
        {
            var club = clubes.Single(t => t.ClubID == clubJugador.ClubID);

            var clubJugadoresMostrar = new VistaJugadores
            {
                JugadorID = clubJugador.JugadorID,
                ClubID = clubJugador.ClubID,
                NombreJugador = clubJugador.NombreJugador,
                NombreClub = club.NombreClub,
                Edad = clubJugador.Edad,
                Nacionalidad = clubJugador.Nacionalidad,
                Posicion = Enum.GetName(typeof(Posicion),clubJugador.Posicion),
                Ingreso = clubJugador.Ingreso.ToString("dd/MM/yyyy")
            };
            clubesJugadoresMostrar.Add(clubJugadoresMostrar);
        }

        return Json(clubesJugadoresMostrar);
    }
}