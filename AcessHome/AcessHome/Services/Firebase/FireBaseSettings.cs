using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Firebase.Database;
using Firebase.Database.Query;
using Newtonsoft.Json;
using AcessHome.Models.Firebase;

namespace AcessHome.Services.Firebase
{
    public class FireBaseSettings
    {
        public static FirebaseClient firebaseClient = new FirebaseClient("https://xamarinapp-18d47-default-rtdb.firebaseio.com/");

        readonly string CollectionName = "Usuarios";

        public async Task RegistrarUsuario(string userName, string userPass) 
        {
            try
            {
                var userData = new
                {
                    UserName = userName,
                    UserPass = userPass,
                    Admin = "0"
                };

                if(await VerificarUsuario(userName,userPass))
                {
                    await firebaseClient.Child(CollectionName).PostAsync(userData);
                }
                else
                {
                    //el usuario ya esta en uso
                }


            }catch(Exception ex)
            {
                //error durante la insercion
            }
        }


        public async Task RegistrarVisita()
        {

            //string userKey = await GetUserKey("samir", "samir123");

            
            
        }

        public async Task<User> GetUserKey(string userName, string userPass)
        {
            try
            {
                var user = (await firebaseClient.Child(CollectionName)
                    .OrderBy("UserName")
                    .EqualTo(userName)
                    .OnceAsync<User>())
                    .FirstOrDefault(u => u.Object.UserPass == userPass);                

                if (user != null)
                {
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


            }catch(Exception ex) 
            {
                return null;
            }

        }
           
        public async Task<bool> VerificarUsuario(string userName,  string userPass)
        {

            var query = firebaseClient.Child(CollectionName)
                .OrderBy("UserName")
                .EqualTo(userName)
                .LimitToFirst(1);

            string jsonString = await query.OnceAsJsonAsync();

            if(jsonString == "null")
            {
                return true;
            }
            return false;


        }
      



    }
}
