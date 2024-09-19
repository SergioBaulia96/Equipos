using System.ComponentModel.DataAnnotations;

namespace Equipos.Models
{

    public class Jugador
    {
        [Key]
        public int JugadorID { get; set; }
        public int ClubID { get; set;}
        public string? NombreJugador { get; set; }
        public int Edad {get; set;}
        public string? Nacionalidad { get; set; }
        public Posicion Posicion { get; set; }
        public DateTime Ingreso { get; set;}

        public virtual Club Clubes {get; set;}
    }




    public enum Posicion
    {
        Arquero = 1,
        Defensor = 2,
        Mediocampista = 3,
        Delantero = 4
    }


    public class VistaJugadores
    {
        public int JugadorID { get; set;}
        public int ClubID { get; set;}
        public string? NombreJugador { get; set; }
        public string? NombreClub {get; set;}
        public int Edad {get; set;}
        public string? Nacionalidad { get; set; }
        public string? Posicion { get; set; }
        public string? Ingreso { get; set;}
    }


}