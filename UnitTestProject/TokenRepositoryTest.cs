using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntityService.Repository;
using Entity;
using UnitTestProject.Generators;

namespace UnitTestProject
{
    [TestClass]
    public class TokenRepositoryTest
    {
        private TokenRepository tokRep = TokenRepository.GetInstance();

        [TestMethod]
        public void TestTokenSave()
        {
            Token t = EntityGenerator.GetToken();
            tokRep.Save(t);

            Token act = tokRep.GetById(t.Id);
            Assert.ReferenceEquals(act, t);
        }

        [TestMethod]
        public void TestTokenUpdate()
        {
            Token t = EntityGenerator.GetToken();
            tokRep.Save(t);

            string newName = "newN";
            long expDate = 123;
            long crDate = 1234;
            t.CreationDate = crDate;
            t.ExpirationDate = expDate;
            t.Name = newName;
            tokRep.Update(t.Id, t);

            Token act = tokRep.GetById(t.Id);
            Assert.ReferenceEquals(act, t);
        }
    }
}
