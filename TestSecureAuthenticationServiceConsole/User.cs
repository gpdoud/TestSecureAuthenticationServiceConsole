using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace TestSecureAuthenticationServiceConsole {
    
    public class User {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("username")]
        public string Username { get; set; }
        [JsonPropertyName("password")]
        public string Password { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("securityKey")]
        public string SecurityKey { get; set; } // Guid changable by user
        [JsonPropertyName("active")]
        public bool Active { get; set; } = true;
        [JsonPropertyName("created")]
        public DateTime Created { get; set; } = DateTime.Now;
        [JsonPropertyName("modified")]
        public DateTime? Modified { get; set; } = null;
        [JsonPropertyName("deleted")]
        public DateTime? Deleted { get; set; } = null;

        public string GetSecurityKey(string username, string password) {
            if(this.Username.ToLower().Equals(username) && this.Password.Equals(password)) {
                return this.SecurityKey;
            }
            return "Unauthorized!";
        }

        public string RegenSecurityKey() {
            this.SecurityKey = Guid.NewGuid().ToString();
            return this.SecurityKey;
        }

        public User() {
            this.RegenSecurityKey();
        }

        public override string ToString() {
            return $"{{ id: {Id}, username: {Username}, password: {Password}, name: {Name}, active: {Active}, " +
                $"securityKey: {SecurityKey}, created: {Created}, modified: {(Modified==null ? "null" : Modified.ToString())}, deleted: {(Deleted==null ? "null" : Deleted.ToString())} }}";
        }
    }
}
