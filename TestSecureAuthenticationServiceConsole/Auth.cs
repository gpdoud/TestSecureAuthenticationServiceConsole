using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TestSecureAuthenticationServiceConsole {
    
    class Auth {

        static readonly string baseUrl = "http://localhost:7015/api/auth";
        static readonly HttpClient http = new HttpClient();

        public static async Task<string> RegenSecurityKey(string username, string password) {
            var res = await http.PutAsync($"{baseUrl}/genseckey/{username}/{password}", null);
            return await res.Content.ReadAsStringAsync();
        }

        public static async Task<string> GetNewToken(string seckey) {
            var res = await http.GetAsync($"{baseUrl}/gentoken/{seckey}");
            return await res.Content.ReadAsStringAsync();
        }

        public static async Task<string> AuthToken(string token) {
            var res = await http.GetAsync($"{baseUrl}/valtoken/{token}");
            return await res.Content.ReadAsStringAsync();
        }

    }
}