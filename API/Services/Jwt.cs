using System;
using System.Collections.Generic;

namespace API.Services
{
    public class Jwt
    {
        // phonebook-api-jwt-key -> in byte array hex format
        private static byte[] key = { 0x70, 0x68, 0x6f, 0x6e, 0x65, 0x62, 0x6f, 0x6f, 0x6b, 0x2d, 0x61, 0x70, 0x69, 0x2d, 0x6a, 0x77, 0x74, 0x2d, 0x6b, 0x65, 0x79 };

        private static DateTime UnixRefDate { get { return new DateTime(1970, 1, 1); } }

        public static string Create(string username)
        {
            var expireDate = DateTime.UtcNow.AddMinutes(20);

            var payload = new Dictionary<string, object>()
            {
                { "sub", username },
                { "exp", expireDate.Subtract(UnixRefDate).TotalSeconds }
            };

            return Jose.JWT.Encode(payload, key, Jose.JwsAlgorithm.HS384);
        }

        public static Dictionary<string, object> Check(string token, out string username)
        {
            var json = Jose.JWT.Decode(token, key, Jose.JwsAlgorithm.HS384);

            var payload = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(json);

            username = payload["sub"].ToString();

            return payload;
        }
    }
}