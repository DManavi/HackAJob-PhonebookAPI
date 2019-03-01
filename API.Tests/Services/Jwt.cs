using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace API.Tests.Services
{
    [TestClass]
    public class Jwt
    {
        [TestMethod]
        public void Generate_JWT_TOKEN()
        {
            var token = API.Services.Jwt.Create("my-username");

            Assert.IsNotNull(token);
            Assert.IsTrue(token.Length > 0);
        }

        [TestMethod]
        public void Validate_JWT_TOKEN()
        {
            var token = API.Services.Jwt.Create("my-username");

            var username = string.Empty;

            var payload = API.Services.Jwt.Check(token, out username);

            Assert.AreEqual(username, payload["sub"]);
            Assert.AreEqual(username, "my-username");
        }
    }
}
