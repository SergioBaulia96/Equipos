

using System.ComponentModel.DataAnnotations;

namespace Equipos.Models
{


    public class Club
    {
        [Key]

        public int ClubID {get; set;}
        public string? NombreClub {get; set;}
        public string? Pais {get; set;}

        public virtual ICollection<Jugador> Jugadores {get; set;}
    }

}