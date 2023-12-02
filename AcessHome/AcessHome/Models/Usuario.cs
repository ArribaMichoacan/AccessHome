using SQLite;

namespace AcessHome.Models
{
    [Table("Usuarios")]
    public class Usuario
    {
        [PrimaryKey,AutoIncrement, Column("idUsuario")]
        public int idUsuario { get; set; }

        [MaxLength(20), NotNull,Unique, Column("nombreUsuario")]
        public string nombreUsuario { get; set; }

        [MaxLength(15), NotNull, Column("passWord")]
        public string passWord { get; set; }
    }
}