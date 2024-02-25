using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Firebase.Database;
using Firebase.Database.Query;
using Newtonsoft.Json;
using AcessHome.Models.Firebase;
using AcessHome.Models;

namespace AcessHome.Services.Firebase
{
    public class FireBaseSettings
    {
        public static FirebaseClient firebaseClient = new FirebaseClient("https://xamarinapp-18d47-default-rtdb.firebaseio.com/");

        private readonly string CollectionName = "Usuarios";
        private readonly string CollectionVisitas = "Visitas";
        private readonly string CollectionRegistro = "Registro";

        public async Task RegistrarUsuario(string userName, string userPass)
        {
            var userData = new
            {
                UserName = userName,
                UserPass = userPass,
                Admin = "0"
            };

            if (await VerificarUsuario(userName, userPass))
            {
                await firebaseClient.Child(CollectionName).PostAsync(userData);
            }
            else
            {
                //el usuario ya esta en uso
            }
        }

        public async Task RegistrarUser(string UserName, string UserPass)
        {
            var UserData = new
            {
                UserName = UserName,
                UserPass = UserPass,
            };

            await firebaseClient.Child(CollectionRegistro).PostAsync(UserData);
        }

        public async Task RegistrarVisita()
        {
            string UserKey = UsuarioSingleton.Instancia.UserKey;

            DateTime date = DateTime.Now;

            string fecha = date.ToString("yyyy-MM-dd");

            var visitaData = new
            {
                userId = UserKey,
                visita = fecha
            };

            await firebaseClient.Child(CollectionVisitas).PostAsync(visitaData);
        }

        public async Task<User> GetUserKey(string userName, string userPass)
        {
            var user = (await firebaseClient.Child(CollectionName)
                .OrderBy("UserName")
                .EqualTo(userName)
                .OnceAsync<User>())
                .FirstOrDefault(u => u.Object.UserPass == userPass);

            if (user != null)
            {
                UsuarioSingleton.Instancia.UserName = user.Object.UserName;
                UsuarioSingleton.Instancia.UserPass = user.Object.UserPass;

                UsuarioSingleton.Instancia.UserKey = user.Key;

                User user2 = new User();

                user2.UserName = user.Object.UserName;
                user2.UserPass = user.Object.UserPass;
                user2.Admin = user.Object.Admin;
                user2.UserKey = user.Key;

                return user2;
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> VerificarUsuario(string userName, string userPass)
        {
            var query = firebaseClient.Child(CollectionName)
                .OrderBy("UserName")
                .EqualTo(userName)
                .LimitToFirst(1);

            string jsonString = await query.OnceAsJsonAsync();

            if (jsonString == "null")
            {
                return true; // no hay ningun usuario registrado con ese nombre
            }
            return false; // hay usuario registrado con ese nombre
        }

        public async Task<List<(string, string)>> ObtenerVisitas(string fecha)
        {
            //consulta para obtener visitas segun la fecha
            var visitas = await firebaseClient
                .Child(CollectionVisitas)
                .OrderBy("visita")
                .EqualTo(fecha)
                .OnceAsync<VisitaUser>();

            //crear lista vacia

            var lista = new List<(string, string)>();

            foreach (var visita in visitas)
            {
                var userid = visita.Object.userId;

                var username =
                    (
                    await firebaseClient.Child(CollectionName)
                    .Child(userid)
                    .OnceSingleAsync<User>()).UserName;

                var fecha2 = visita.Object.visita;

                lista.Add((username, fecha2));
            }

            return lista;
        }
    }

    public class UsuarioSingleton
    {
        private static UsuarioSingleton instancia = null;

        private UsuarioSingleton()
        { }

        public static UsuarioSingleton Instancia
        {
            get
            {
                if (instancia == null)
                {
                    instancia = new UsuarioSingleton();
                }
                return instancia;
            }
        }

        public string UserName { get; set; }
        public string UserPass { get; set; }
        public string adminCheck { get; set; }
        public string UserKey { get; set; }
    }

    public class VisitaUser
    {
        public string userId { get; set; }

        public string visita { get; set; }
    }
}