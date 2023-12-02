using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace AcessHome.Models
{
    [Table("Administradores")]
    public class Administradores
    {
        [PrimaryKey,NotNull,AutoIncrement, Column("idAdministrador")]
        public int idAdministrador {  get; set; }
        [Indexed]
        public int usuarioId { get; set; }

    }
}
