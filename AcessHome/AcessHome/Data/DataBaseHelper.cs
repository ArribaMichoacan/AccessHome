using AcessHome.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AcessHome.Data
{
    public class DataBaseHelper
    {
        static SQLiteAsyncConnection Database;

        public static readonly AsyncLazy<DataBaseHelper> instance = new AsyncLazy<DataBaseHelper>(async () =>
        {
            var instance = new DataBaseHelper();
            CreateTableResult usuarioResult = await Database.CreateTableAsync<Usuario>();
            CreateTableResult visitaResult = await Database.CreateTableAsync<Visita>();
            return instance;

        });



        public DataBaseHelper()
        {
            Database = new SQLiteAsyncConnection(Constants.DataBasePath, Constants.Flags);
        }


        public Task saveUsuarioInfo(Usuario usuarioInfo)
        {
            return Database.InsertAsync(usuarioInfo);
        }


        public Task SaveVisita(Usuario userInfo)
        {

            Visita visitaq = new Visita
            {
                usuarioId = userInfo.idUsuario,
                visita = DateTime.Now
            };

            return Database.InsertAsync(visitaq);

        }


        public async Task<List<Visita>> GetAllVisitasByDate()
        {
            var query = @"SELECT * FROM Visitas";

            var result = await Database.QueryAsync<Visita>(query);

            return result;
        }

        public async Task<List<Visita>> GetVisitasByUser(string nombreUsuario)
        {
            var query = @"SELECT idUsuario FROM Usuario WHERE nombreUsuario = ?";


            var usuario = await Database.Table<Usuario>().Where(u => u.nombreUsuario == nombreUsuario).FirstOrDefaultAsync();

            if (usuario != null)
            {
                // Si encontramos un usuario, ahora podemos obtener las visitas asociadas
                var visitas = await Database.Table<Visita>().Where(v => v.usuarioId == usuario.idUsuario).ToListAsync();

                return visitas;
            }
            else
            {
                return null;
            }

            
                       

        }


        public async Task<int> CheckCredentials(Usuario usuario)
        {
            var query = @"SELECT * FROM Usuario WHERE nombre nombreUsuario = ? AND passWord =?";

            var result = await Database.Table<Usuario>().Where(u => u.nombreUsuario == usuario.nombreUsuario && u.passWord == usuario.passWord).FirstOrDefaultAsync();

            if(result!= null)
            {
                return 1;
            }
            else { return 0; }


        }


        public async Task<int> GetUserId(Usuario usuario)
        {
            var query = @"SELECT * FROM Usuario WHERE nombre nombreUsuario = ? AND passWord =?";

            var result = await Database.Table<Usuario>().Where(u => u.nombreUsuario == usuario.nombreUsuario && u.passWord == usuario.passWord).FirstOrDefaultAsync();
            return result.idUsuario;

        }





    }
}
