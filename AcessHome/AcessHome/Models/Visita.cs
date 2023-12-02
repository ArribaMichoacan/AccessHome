using SQLite;
using System;

namespace AcessHome.Models
{
    [Table("Visita")]
    public class Visita
    {
        [PrimaryKey,AutoIncrement,Column("idVisita")]
        public int idVisita { get; set; }
        [Indexed]
        public int usuarioId {  get; set; }
        public DateTime visita {  get; set; }

    }
}
