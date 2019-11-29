using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace TestSecureAuthenticationServiceConsole {

    class Program {

        static readonly HttpClient http = new HttpClient();
        static readonly string baseUrl = "http://localhost:7015/api";
    
        static async Task Main(string[] args) {
            {
                // Insert()
                //var newUser = new User {
                //    Id = 0, Username = "us", Password = "us", Name = "User", Active = true
                //};
                //var res = await Insert(newUser);
                //Console.WriteLine($"Insert: {res}");
            }
            {
                // Update()
                var user = await GetPk(3);
                user.RegenSecurityKey();
                var res = await Update(user);
                Console.WriteLine($"Update: {res}");
            }
            {
                // GetAll()
                var users = await GetAll();
                Console.WriteLine("users: [");
                foreach(var u in users)
                    Console.WriteLine($"  {u}");
                Console.WriteLine("]");
            }
            {
                // GetPk()
                //var user = await GetPk(3);
                //Console.WriteLine($"user: {user}");
            }
        }
        static async Task<IEnumerable<User>> GetAll() {
            var res = await http.GetStringAsync($"{baseUrl}/users");
            var options = new JsonSerializerOptions {
                WriteIndented = true
            };
            var users = (IEnumerable<User>)JsonSerializer.Deserialize<IEnumerable<User>>(res, options);
            return users;
        }
        static async Task<User> GetPk(int id) {
            var res = await http.GetStringAsync($"{baseUrl}/users/{id}");
            var options = new JsonSerializerOptions {
                WriteIndented = true
            };
            var user = (User)JsonSerializer.Deserialize<User>(res, options);
            return user;
        }
        static async Task<HttpResponseMessage> Insert(User usr) {
            var userJson = JsonSerializer.Serialize<User>(usr);
            var content = new StringContent(userJson, System.Text.Encoding.Default, "application/json");
            var res = await http.PostAsync($"{baseUrl}/users", content);
            return res;
        }
        static async Task<HttpResponseMessage> Update(User usr) {
            var userJson = JsonSerializer.Serialize<User>(usr);
            var content = new StringContent(userJson, System.Text.Encoding.Default, "application/json");
            var res = await http.PutAsync($"{baseUrl}/users", content);
            return res;
        }
    }
}
