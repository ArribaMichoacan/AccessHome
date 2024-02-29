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
using System.Collections.ObjectModel;

namespace AcessHome.Services.Firebase
{
    public class FireBaseSettings
    {
        //In order to connect with firebase you need to place here your link from your real time database.        
        public static FirebaseClient firebaseClient = new FirebaseClient(""); //place your link here
      
        // Document names in our firebase database
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

            if (await VerificarUsuario(userName))
            {
                await firebaseClient.Child(CollectionName).PostAsync(userData);
            }
            else
            {
                //el usuario ya esta en uso
            }
        }

        /// <summary>
        /// Registered the given user request
        /// </summary>
        /// <param name="UserName">User Name</param>
        /// <param name="UserPass">User password</param>
        /// <returns></returns>
        public async Task RegistrarUser(string UserName, string UserPass)
        {

            var UserData = new
            {
                UserName = UserName,
                UserPass = UserPass,
            };

            await firebaseClient.Child(CollectionRegistro).PostAsync(UserData);
        }

        /// <summary>
        /// Register the visit
        /// </summary>
        /// <returns></returns>
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


        /// <summary>
        /// Gets the given user key
        /// </summary>
        /// <param name="userName">UserName</param>
        /// <param name="userPass">UserPass</param>
        /// <returns></returns>
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

        /// <summary>
        /// Verify is there is an user with the same username
        /// </summary>
        /// <param name="userName">User name</param>
        /// <returns>true if not user registered false if user with the same name</returns>
        public async Task<bool> VerificarUsuario(string userName)
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

        /// <summary>
        /// Gets all the registered visits in firebase
        /// </summary>
        /// <param name="fecha">date param to order by</param>
        /// <returns>All the visits order by date</returns>
        public async Task<ObservableCollection<VisitaUser>> ObtenerVisitas(string fecha)
        {
            //consulta para obtener visitas segun la fecha
            var visitas = await firebaseClient
                .Child(CollectionVisitas)
                .OrderBy("visita")
                .EqualTo(fecha)
                .OnceAsync<VisitaUser>();
          
            var lista = new ObservableCollection<VisitaUser>();

            VisitaUser visitaUs = new VisitaUser();

            foreach (var visita in visitas)
            {
                var userid = visita.Object.userId;

                var username =
                    (
                    await firebaseClient.Child(CollectionName)
                    .Child(userid)
                    .OnceSingleAsync<User>()).UserName;

                var fecha2 = visita.Object.visita;

                visitaUs.userId = username;
                visitaUs.visita = fecha2;

                lista.Add(visitaUs);
            }

            return lista;
        }



        /// <summary>
        /// Obtains all the new users requests
        /// </summary>
        /// <returns></returns>
        public async Task<ObservableCollection<User>> ObtenerSolicitudesRegistro()
        {
            var usuarios = await firebaseClient
                .Child(CollectionRegistro)
                .OnceAsync<User>();

            ObservableCollection<User> lista = new ObservableCollection<User>();

            if (usuarios != null)
            {
                foreach (var users in usuarios)
                {
                    User user = new User();
                    user.UserKey = users.Key; //key del nodo, sirve para eliminarlo despues
                    user.UserName = users.Object.UserName;
                    user.UserPass = users.Object.UserPass;
                    lista.Add(user);
                }
            }

            return lista;
        }

        /// <summary>
        /// Deletes the given user
        /// </summary>
        /// <param name="user">object with user info</param>
        /// <returns></returns>
        public async Task EliminarRegistro(User user)
        {
            await firebaseClient.Child(CollectionRegistro).Child(user.UserKey).DeleteAsync();
        }

        /// <summary>
        /// Obtains all the users in the collection
        /// </summary>
        /// <returns>Observable list/collection with the user info from firebase</returns>
        public async Task<ObservableCollection<User>> ObtenerUsuarios()
        {
            var users = await firebaseClient.Child(CollectionName).OnceAsync<User>();

            ObservableCollection<User> lista = new ObservableCollection<User>();

            if (users != null)
            {
                foreach (var userData in users)
                {
                    User user = new User
                    {
                        UserKey = userData.Key,
                        UserName = userData.Object.UserName,
                        UserPass = userData.Object.UserPass,
                        Admin = userData.Object.Admin == "0" ? "Usuario normal" : "Administrador"
                    };

                    lista.Add(user);
                }
            }

            return lista;
        }
    }


    /// <summary>
    /// Since app purpose was meant only for testing, we didn't really need a real user session
    /// so in order to keep the user logged info we used a singleton to maintain the info through the app lifecycle
    /// </summary>
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
}