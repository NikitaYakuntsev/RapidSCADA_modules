using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EntityService.Repository;
using UnitTestProject.Generators;
using Entity.Models;

namespace UnitTestProject
{
    [TestClass]
    public class SPRepTest
    {
        private SystemParameterRepository spr = SystemParameterRepository.GetInstance();
        [TestMethod]
        public void TestSPSave()
        {
            SystemParameter sp = EntityGenerator.GetSystemParameter();
            spr.Save(sp);

            SystemParameter act = spr.GetById(sp.Id);
            Assert.ReferenceEquals(act, sp);
        }

        [TestMethod]
        public void TestSPUpdate()
        {
            SystemParameter sp = EntityGenerator.GetSystemParameter();
            spr.Save(sp);

            string newValue = "newValue";
            sp.Value = newValue;
            spr.Update(sp.Id, sp);

            SystemParameter act = spr.GetByKey(sp.Key);
            Assert.ReferenceEquals(act, sp);
        }
    }
}
